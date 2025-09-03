using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public Button ball1Button;
    public Button ball2Button;
    public Button ball3Button;

    public Button resetBallButton;


    private void Start()
{
    UpdateButtonStates();
    resetBallButton.onClick.AddListener(ResetBallSelection);
}


public void ResetBallSelection()
{
    PlayerPrefs.DeleteKey("SelectedBall");
    PlayerPrefs.Save();
    UpdateButtonStates();
// FindObjectOfType<BallPurchaseManager>()?.UpdateButtonText();
    Debug.Log("🔁 Ball selection reset. Purchase retained.");
}


    public void SelectBall(int index)
    {
        int current = PlayerPrefs.GetInt("SelectedBall", -1);

        if (current == index)
        {
            // Unselect
            PlayerPrefs.DeleteKey("SelectedBall");
        }
        else
        {
            // Select
            PlayerPrefs.SetInt("SelectedBall", index);
        }

        PlayerPrefs.Save();
        UpdateButtonStates();
    }

    public void UpdateButtonStates()
    {
        int selected = PlayerPrefs.GetInt("SelectedBall", -1);
    bool thirdBallUnlocked = PlayerPrefs.GetInt("ThirdBallUnlocked", 0) == 1;

        UpdateButton(ball1Button, selected == 0);
        UpdateButton(ball2Button, selected == 1);
if (thirdBallUnlocked)
    {
        UpdateButton(ball3Button, selected == 2);
        ball3Button.GetComponentInChildren<Text>().text = selected == 2 ? "Used" : "Use";
    }
    else
    {
        ball3Button.interactable = true;
        ball3Button.GetComponentInChildren<Text>().text = "Buy";
    }
    }

    void UpdateButton(Button button, bool isSelected)
    {
        Text buttonText = button.GetComponentInChildren<Text>();
        ColorBlock colors = button.colors;

        if (isSelected)
        {
            buttonText.text = "Used";
            colors.normalColor = Color.green;
        }
        else
        {
            buttonText.text = "Use";
            colors.normalColor = Color.white;
        }

        button.colors = colors;
    }

public void OnThirdBallButtonClick()
{
    if (PlayerPrefs.GetInt("ThirdBallUnlocked", 0) == 1)
    {
        // Ball is unlocked, select/deselect it
        SelectBall(2);
    }
    else
    {
        // Ball is locked, try to purchase it
        FindObjectOfType<BallPurchase>()?.BuyProduct(0);
    }
}

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

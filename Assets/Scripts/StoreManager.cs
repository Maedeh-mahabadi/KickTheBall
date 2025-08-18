using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public Button ball1Button;
    public Button ball2Button;
    public Button ball3Button;

    private void Start()
    {
        UpdateButtonStates();
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

    void UpdateButtonStates()
    {
        int selected = PlayerPrefs.GetInt("SelectedBall", -1);

        UpdateButton(ball1Button, selected == 0);
        UpdateButton(ball2Button, selected == 1);
if (PlayerPrefs.GetInt("ThirdBallUnlocked", 0) == 1)
{
    UpdateButton(ball3Button, selected == 2);
}
else
{
    ball3Button.interactable = false;
    ball3Button.GetComponentInChildren<Text>().text = "Locked";
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

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

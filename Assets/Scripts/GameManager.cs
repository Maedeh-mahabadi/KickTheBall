using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
// using Bazaar.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
// using Bazaar.Poolakey;
// using Bazaar.Poolakey.Data;


public class GameManager : MonoBehaviour
{
    public Text feedbackText;

    public int lives = 3;
    public int score = 0;
    public Text livesText;
    public Text scoreText;

    public GameObject gameOverPanel;
    public Text finalScoreText;
    public Button inGameMenuButton;

    // [SerializeField] private PurchaseManager purchaseManager;

    private bool doubleScoreActive = false;
    public int savedScore;

    public AudioClip gameMusic;


    public Button buyLivesButton;

 void Start()
{
    UpdateUI();
    SoundManager.Instance.PlayMusic(gameMusic);
    gameOverPanel.SetActive(false);


}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            finalScoreText.text = "Score: " + score;
            gameOverPanel.SetActive(true);
        }
    }

    public void AddScore(int amount)
    {
        Debug.Log("AddScore called. DoubleScoreActive = " + doubleScoreActive);
        score += doubleScoreActive ? amount * 2 : amount;
        UpdateUI();
    }

    public int GetScore()
    {
        return score;
    }

    public void BallHitGround()
    {
        lives--;
        UpdateUI();

        if (lives <= 0)
        {
            TriggerGameOver();
        }
    }

    void UpdateUI()
    {
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
    }

    void TriggerGameOver()
    {
        savedScore = score; // 👈 Save score before Game Over
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        Debug.Log("Score at Game Over: " + score);
        finalScoreText.text = "Score: " + score;
        inGameMenuButton.interactable = false;
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ResumeGameIfPossible()
    {
        if (Time.timeScale == 0f && lives > 0)
        {
            Time.timeScale = 1f;
            gameOverPanel.SetActive(false);
            inGameMenuButton.interactable = true;
            UpdateUI();
            Debug.Log("Game resumed with score: " + score);
        }
    }


    public void AddLife(int amount)
    {
        lives += amount;
        UpdateUI();

        if (Time.timeScale == 0f && lives > 0)
        {
            Time.timeScale = 1f;
            gameOverPanel.SetActive(false);
        }
    }

    public void ActivateDoubleScore(float duration)
    {
        Debug.Log("Activating double score for " + duration + " seconds");
        StartCoroutine(DoubleScoreCoroutine(duration));
    }

    IEnumerator DoubleScoreCoroutine(float duration)
    {
        doubleScoreActive = true;
        Debug.Log("Double score ON");
        yield return new WaitForSecondsRealtime(duration);
        doubleScoreActive = false;
        Debug.Log("Double score OFF");
    }

    public void SlowDownTimer(float factor, float duration)
    {
        StartCoroutine(SlowTimerEffect(factor, duration));
    }

    IEnumerator SlowTimerEffect(float factor, float duration)
    {
        Time.timeScale = factor;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }


    void ShowFeedback(string message)
    {
        feedbackText.text = message;
        feedbackText.gameObject.SetActive(true);
        StartCoroutine(HideFeedbackAfterSeconds(3f));
    }

    IEnumerator HideFeedbackAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        feedbackText.gameObject.SetActive(false);
    }

}

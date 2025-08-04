using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public int lives = 3;
    public int score = 0;
    public Text livesText;
    public Text scoreText;

    public GameObject gameOverPanel;         // 👈 Assign in Inspector
    public Text finalScoreText;              // 👈 Assign in Inspector (inside GameOverPanel)
    public Button inGameMenuButton;

    private bool doubleScoreActive = false;

    void Start()
    {
        UpdateUI();
        gameOverPanel.SetActive(false);      // Make sure it's hidden at start
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
        Time.timeScale = 0f;                         // Pause game
        gameOverPanel.SetActive(true);               // Show Game Over UI
        Debug.Log("Score at Game Over: " + score);
        finalScoreText.text = "Score: " + score;
        inGameMenuButton.interactable = false;

    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;                         // Reset time scale before switching scenes
        SceneManager.LoadScene("MainMenu");
    }

    public void AddLife(int amount)
    {
        lives += amount;
        UpdateUI();

        // If lives were 0 and game was paused, resume
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
    yield return new WaitForSecondsRealtime(duration); // ✅ Not affected by timeScale
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
}

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
    // public GameObject gameOverPanel;

    void Start()
    {
        UpdateUI();
        // gameOverPanel.SetActive(false);
    }

    public void AddScore(int amount)
    {
        score += amount;
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
            // gameOverPanel.SetActive(true);
        }
    }

    void UpdateUI()
    {
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void AddLife(int amount)
{
    lives += amount;
    UpdateUI();
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

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

    private bool doubleScoreActive = false;

    void Start()
    {
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        if (doubleScoreActive)
            score += amount * 2;
        else
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

    public void ActivateDoubleScore(float duration)
    {
        StartCoroutine(DoubleScoreCoroutine(duration));
    }

    IEnumerator DoubleScoreCoroutine(float duration)
    {
        doubleScoreActive = true;
        yield return new WaitForSeconds(duration);
        doubleScoreActive = false;
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

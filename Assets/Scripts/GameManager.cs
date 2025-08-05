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

    public GameObject gameOverPanel;
    public Text finalScoreText;
    public Button inGameMenuButton;

    [SerializeField] private PurchaseManager purchaseManager;
    [SerializeField] private string productId = "three_lives_pack"; // 👈 Your Bazaar product ID

    private bool doubleScoreActive = false;
    private int savedScore;

    public AudioClip gameMusic;


    async void Start()
    {
        UpdateUI();
        SoundManager.Instance.PlayMusic(gameMusic);

        gameOverPanel.SetActive(false);
        bool connected = await purchaseManager.init();
        if (!connected)
        {
            Debug.LogError("Poolakey connection failed.");
        }
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

    // 👇 NEW METHOD: Buy lives using Poolakey
    public async void OnBuyLivesClicked()
    {
        if (purchaseManager == null)
        {
            Debug.LogError("PurchaseManager not assigned.");
            return;
        }

        var purchaseResult = await purchaseManager.Purchase(productId);
        if (purchaseResult.status != Bazaar.Data.Status.Success)
        {
            Debug.LogWarning("Purchase failed: " + purchaseResult.message);
            return;
        }

        var token = purchaseResult.data.purchaseToken;
        var consumeResult = await purchaseManager.Consume(token);
        if (consumeResult.status != Bazaar.Data.Status.Success)
        {
            Debug.LogWarning("Consume failed: " + consumeResult.message);
            return;
        }

        score = savedScore;
        AddLife(3); // 👈 Grant 3 lives
        Debug.Log("Purchase successful and consumed. 3 lives added.");
    }
}

using UnityEngine;

public class Potion : MonoBehaviour
{
    public enum PotionType { Health, Score }
    public PotionType type;
    
        public GameManager gameManager; // Assign in Inspector

    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Ball"))
    {
        if (gameManager == null)
        {
            Debug.LogError("Potion: GameManager is not assigned!");
            return;
        }

        gameManager.AddLife(1);
        gameManager.ActivateDoubleScore(7f); // 7 seconds
        Destroy(gameObject); // Remove potion after pickup
    }
}

    

    void ApplyEffect()
    {
        switch (type)
        {
            case PotionType.Health:
                Debug.Log("Health increased!");
                // Call your health manager here
                break;
            case PotionType.Score:
                Debug.Log("Score increased!");
                // Add to score here
                break;
        }
    }
}

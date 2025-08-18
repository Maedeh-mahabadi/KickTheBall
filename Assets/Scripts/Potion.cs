using UnityEngine;
using Bazaar.Data;
using System.Collections.Generic;
using System.Threading.Tasks;


public class Potion : MonoBehaviour
{
    public enum PotionType { AddLife, DoubleScore }
    public PotionType type;

    public GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            if (gameManager == null) return;

            switch (type)
            {
                case PotionType.AddLife:
                    gameManager.AddLife(1);
                    break;
                case PotionType.DoubleScore:
                    Debug.Log("Potion triggered: Double Score");
                    gameManager.ActivateDoubleScore(6f);
                    break;
            }

            Destroy(gameObject);
        }
    }
}

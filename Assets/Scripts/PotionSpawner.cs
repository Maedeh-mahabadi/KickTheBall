using UnityEngine;

public class PotionSpawner : MonoBehaviour
{
    public GameObject potionPrefab;
    public Transform[] spawnPoints;

    void Start()
    {
        SpawnPotion();
    }

    void SpawnPotion()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(potionPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
    }
}

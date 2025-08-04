using UnityEngine;

public class PotionSpawner : MonoBehaviour
{
    public GameObject[] potionPrefabs;     // Assign your potion prefabs here
    public Transform[] spawnPoints;        // Assign your 2 spawn points here

    public GameManager gameManager; // Assign this in the Inspector

    public float spawnInterval = 10f;    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnPotion();
            timer = 0f;
        }
    }

    void SpawnPotion()
{
    if (potionPrefabs.Length == 0 || spawnPoints.Length == 0) return;

    int potionIndex = Random.Range(0, potionPrefabs.Length);
    int spawnIndex = Random.Range(0, spawnPoints.Length);

    GameObject potion = Instantiate(potionPrefabs[potionIndex], spawnPoints[spawnIndex].position, Quaternion.identity);

    Potion potionScript = potion.GetComponent<Potion>();
    if (potionScript != null)
    {
        potionScript.gameManager = gameManager;
    }
}

}

using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject zombiePrefab;  // Reference to the zombie prefab
    public Transform[] spawnPoints;  // Array of spawn points
    public float spawnInterval = 3f;  // Time interval between spawns
    public int maxZombies = 10;  // Maximum number of zombies on screen
    public float detectionRadius = 10f;  // Detection radius for zombies
    public AudioClip[] idleSounds;  // Array of idle sounds for zombies
    public float idleSoundTime = 5f;  // Time interval for idle sounds

    private int currentZombieCount = 0;
    private GameManager gameManager;

    void Start()
    {
        // Find and store reference to GameManager
        gameManager = FindObjectOfType<GameManager>();

        // Start spawning enemies
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (currentZombieCount < maxZombies)
            {
                SpawnZombie();
            }
        }
    }

    void SpawnZombie()
    {
        // Choose a random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject zombieGO = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);

        // Initialize zombie properties
        zombieGO.GetComponent<zombieController>().flipModel = zombieGO.transform; // Adjust as needed
        zombieGO.GetComponent<zombieController>().runSpeed = Random.Range(2f, 5f);  // Set random speed
        zombieGO.GetComponent<zombieController>().idleSounds = idleSounds;
        zombieGO.GetComponent<zombieController>().idleSoundTime = idleSoundTime;

        // Increment zombie count
        currentZombieCount++;

        // Optionally, disable the collider after some time to let zombies pass through each other
        Destroy(zombieGO, 20f);  // Example: Destroy zombie after 20 seconds
    }

    // Method to decrement zombie count when a zombie is destroyed
    public void OnZombieDestroyed()
    {
        currentZombieCount--;
    }
}

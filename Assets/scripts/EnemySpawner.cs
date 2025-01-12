using System.Collections;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject enemyPrefab;      // The enemy prefab to spawn
    public Transform[] spawnPoints;    // Array of spawn points
    public float spawnInterval = 1f;   // Time interval between spawns
    public int enemiesPerWave = 10;    // Number of enemies per wave
    public int totalEnemies = 300;     // Total number of enemies to spawn
    public TextMeshProUGUI texttotalenemyleft;
    public TextMeshProUGUI texttotalwaveleft;

    private int currentWave = 0;       // Current wave count
    private int spawnedEnemies = 0;   // Total enemies spawned
    private int activeEnemies = 0;    // Current active enemies in the scene



    public Transform Map;

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        while (spawnedEnemies < totalEnemies)
        {
            // Start spawning enemies for the current wave
            for (int i = 0; i < enemiesPerWave; i++)
            {
                if (spawnedEnemies >= totalEnemies) break;

                SpawnEnemy();
                yield return new WaitForSeconds(spawnInterval);
            }

            // Wait for all enemies to be destroyed before spawning the next wave
            while (activeEnemies > 0)
            {
                yield return null;
            }

            // Increment wave count
            currentWave++;
           
        }

        Debug.Log("All waves completed!");
        Map.GetComponent<Animator>().SetTrigger("mapopen");

    }

    private void SpawnEnemy()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        spawnedEnemies++;
        activeEnemies++;

        // Subscribe to the OnEnemyDestroyed event
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.OnEnemyDestroyed += HandleEnemyDestroyed;
        }
       
    }

    private void HandleEnemyDestroyed()
    {
        activeEnemies--;
        

    }
    private void Update()
    {
        texttotalenemyleft.text = "Enemies left: " + activeEnemies;
        texttotalwaveleft.text = "Wave: " + currentWave + "/30";
    }
}

using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public EnemySpawner enemySpawner; // Reference to the enemy spawner
    public float timeBetweenWaves = 10f; // Time between waves in seconds
    public int totalWaves = 25; // Total number of waves

    public WaveConfiguration[] waves;

    private int currentWave = 0;
    private bool isWaveActive = false;

    private void Start()
    {
        StartCoroutine(StartWaveSequence());
    }

    private IEnumerator StartWaveSequence()
    {
        yield return new WaitForSeconds(3f); // Initial delay before starting waves

        while (currentWave < totalWaves)
        {
            isWaveActive = true;
            //Debug.Log("Wave " + (currentWave + 1) + " started!");

            // Spawn enemies for the current wave
            StartCoroutine(SpawnWaveEnemies(currentWave));

            // Wait for enemies to be defeated before starting the next wave
            yield return new WaitWhile(() => isWaveActive);

            //Debug.Log("Wave " + (currentWave + 1) + " cleared!");

            yield return new WaitForSeconds(timeBetweenWaves);
            currentWave++;
        }

        //Debug.Log("All waves cleared!");
    }

    private IEnumerator SpawnWaveEnemies(int waveIndex)
    {
        if (waveIndex < waves.Length)
        {
            WaveConfiguration waveConfig = waves[waveIndex];
            for (int i = 0; i < waveConfig.enemyCount; i++)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                enemySpawner.SpawnEnemy(waveConfig.enemyTypePrefab, spawnPosition);

                // Wait for the specified time before spawning the next enemy
                yield return new WaitForSeconds(waveConfig.timeBetweenSpawns);
            }
        }
    }


    public void OnWaveCleared()
    {
        //Debug.Log("Wave clear method called.");
        isWaveActive = false;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(Clamp.Instance.minX, Clamp.Instance.maxX);
        float randomZ = Random.Range(Clamp.Instance.minZ, Clamp.Instance.maxZ);

        return new Vector3(randomX, 0.125f, randomZ);
    }
}

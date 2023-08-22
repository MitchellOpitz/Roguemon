using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public EnemySpawner enemySpawner; // Reference to the enemy spawner
    public float timeBetweenWaves = 10f; // Time between waves in seconds
    public UpgradesUI upgradesUI;
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
            // Pause the game before starting the wave
            Time.timeScale = 0;

            // Show the upgrade menu
            upgradesUI.ShowUpgradeMenu();

            // Wait until the upgrade menu is closed (player has made their selection)
            yield return new WaitWhile(() => upgradesUI.isUpgradeMenuOpen);

            // Resume the game
            Time.timeScale = 1;

            isWaveActive = true;
            StartCoroutine(SpawnWaveEnemies(currentWave));

            // ... rest of your wave spawning logic ...

            // Wait for enemies to be defeated before starting the next wave
            yield return new WaitWhile(() => isWaveActive);

            // ... rest of your wave clearing logic ...

            yield return new WaitForSeconds(timeBetweenWaves);
            currentWave++;
        }

        Debug.Log("All waves cleared!");
        SceneManagement.instance.LoadSceneByName("Title");
    }


    private IEnumerator SpawnWaveEnemies(int waveIndex)
    {
        if (waveIndex < waves.Length)
        {
            WaveConfiguration waveConfig = waves[waveIndex];
            Vector3 waveSpawnPosition = GetRandomSpawnPosition();
            for (int i = 0; i < waveConfig.enemyCount; i++)
            {
                Vector3 spawnPosition = GetSpawnOffset(waveSpawnPosition);
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

    private Vector3 GetSpawnOffset(Vector3 waveSpawnPosition)
    {
        float offsetX = Random.Range(-1f, 1f);
        float offsetZ = Random.Range(-1f, 1f);

        // Apply the offset to the wave spawn position
        Vector3 spawnOffset = new Vector3(offsetX, 0f, offsetZ);
        Vector3 spawnPosition = waveSpawnPosition + spawnOffset;

        // Clamp the spawn position within the Clamp boundaries
        spawnPosition.x = Mathf.Clamp(spawnPosition.x, Clamp.Instance.minX, Clamp.Instance.maxX);
        spawnPosition.z = Mathf.Clamp(spawnPosition.z, Clamp.Instance.minZ, Clamp.Instance.maxZ);

        return spawnPosition;
    }

}

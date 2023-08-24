using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public WaveManager waveManager;

    private int enemiesRemaining;

    // Spawns an enemy at the given position using the provided enemyPrefab
    public void SpawnEnemy(GameObject enemyPrefab, Vector3 spawnPosition)
    {
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity); // Instantiate the enemy
        enemiesRemaining++; // Increment the count of remaining enemies
    }

    // Called when an enemy is defeated
    public void EnemyDefeated()
    {
        enemiesRemaining--; // Decrement the count of remaining enemies

        if (enemiesRemaining <= 0)
        {
            // All enemies in the wave have been defeated
            waveManager.OnWaveCleared(); // Notify the WaveManager that the wave is cleared
        }
    }
}

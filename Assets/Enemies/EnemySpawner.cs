using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public WaveManager waveManager;
    public GameObject enemyTypeAPrefab;
    public GameObject enemyTypeBPrefab;

    private int enemiesRemaining;
    // ...

    public void SpawnEnemy(GameObject enemyPrefab, Vector3 spawnPosition)
    {
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemiesRemaining++;
        Debug.Log("Enemies remaining: " + enemiesRemaining);
    }

    public void EnemyDefeated()
    {
        enemiesRemaining--;
        Debug.Log("Enemies remaining: " + enemiesRemaining);

        if (enemiesRemaining <= 0)
        {
            // All enemies in the wave have been defeated
            waveManager.OnWaveCleared();
        }
    }
}

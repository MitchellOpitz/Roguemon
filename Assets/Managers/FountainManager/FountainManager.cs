using UnityEngine;
using System.Collections;

public class FountainManager : MonoBehaviour
{
    public GameObject healingFountainPrefab;
    public float spawnInterval;
    private Coroutine fountainSpawnCoroutine;

    public void ActivateHealingFountains(float duration)
    {
        // Debug.Log("Activating healing fountains.  Duration: " + duration);
        if (fountainSpawnCoroutine == null)
        {
            fountainSpawnCoroutine = StartCoroutine(SpawnFountainsWithInterval(duration, spawnInterval));
        } else
        {
            StopSpawningFountains();
            fountainSpawnCoroutine = StartCoroutine(SpawnFountainsWithInterval(duration, spawnInterval));
        }
    }

    public void StopSpawningFountains()
    {
        // Debug.Log("Stopping healing fountains.");
        if (fountainSpawnCoroutine != null)
        {
            StopCoroutine(fountainSpawnCoroutine);
            fountainSpawnCoroutine = null;
        }
        
        // Find all HealingFountain instances and destroy them
        HealingFountain[] fountains = FindObjectsOfType<HealingFountain>();
        foreach (HealingFountain fountain in fountains)
        {
            Destroy(fountain.gameObject);
        }
    }

    private IEnumerator SpawnFountainsWithInterval(float duration, float spawnInterval)
    {
        while (true)
        {
            Vector3 randomSpawnPosition = GetRandomSpawnPosition();
            GameObject fountain = Instantiate(healingFountainPrefab, randomSpawnPosition, Quaternion.Euler(90f, 0f, 0f));
            Destroy(fountain, duration);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(Clamp.Instance.minX, Clamp.Instance.maxX);
        float randomZ = Random.Range(Clamp.Instance.minZ, Clamp.Instance.maxZ);
        return new Vector3(randomX, 0f, randomZ);
    }
}

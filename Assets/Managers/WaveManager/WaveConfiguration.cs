using UnityEngine;

[System.Serializable]
public class WaveConfiguration
{
    public GameObject enemyTypePrefab;
    public int enemyCount;
    public float timeBetweenSpawns = 1f; // Time between spawning each enemy in the wave
    // You can add more properties as needed, such as spawn positions, delays, etc.
}

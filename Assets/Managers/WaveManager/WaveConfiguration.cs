using UnityEngine;

// Serializable class to define wave configuration
[System.Serializable]
public class WaveConfiguration
{
    public GameObject enemyTypePrefab;      // The enemy prefab for this wave
    public int enemyCount;                  // Number of enemies in the wave
    public float timeBetweenSpawns = 1f;    // Time between spawning each enemy in the wave
    // Additional properties can be added for more specific wave setups, like spawn positions, delays, etc.
}

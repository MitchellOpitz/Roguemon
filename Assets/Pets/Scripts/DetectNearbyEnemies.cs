using UnityEngine;

public class DetectNearbyEnemies : MonoBehaviour
{
    public LayerMask enemyLayer; // Layer mask to specify which objects are considered enemies
    public float detectionRadius = 5f; // Radius to detect nearby enemies
    private float slowMultiplier;
    private float damageMultiplier;
    private float pushbackChance;

    private void Start()
    {
        ResetSlowMultiplier();
        ResetDamageMultiplier();
        ResetPushbackChance();
    }

    private void Update()
    {
        // Detect nearby enemies using Physics.OverlapSphere
        Collider[] nearbyEnemies = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);

        // Apply or remove the slow effect based on nearby enemies
        foreach (Collider enemyCollider in nearbyEnemies)
        {
            Enemy enemy = enemyCollider.GetComponent<Enemy>();

            // Ice synergy
            if (enemy != null && !enemy.isSlowed)
            {
                enemy.UpdateMoveSpeed(slowMultiplier);
            }

            // Psychic synergy
            if (enemy != null)
            {
                enemy.UpdateDamageMultiplier(damageMultiplier);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Display the detection radius in the Unity Editor
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    public void ResetSlowMultiplier()
    {
        slowMultiplier = 0;
        Debug.Log("Update slow multiplier.  New value: " + slowMultiplier);
    }

    public void UpdateSlowMultiplier(float multiplier)
    {
        slowMultiplier = multiplier;
        Debug.Log("Update slow multiplier.  New value: " + slowMultiplier);
    }

    public void ResetDamageMultiplier()
    {
        damageMultiplier = 0;
        Debug.Log("Update damage multiplier.  New value: " + damageMultiplier);
    }

    public void UpdateDamageMultiplier(float multiplier)
    {
        damageMultiplier = multiplier;
        Debug.Log("Update damage multiplier.  New value: " + damageMultiplier);
    }

    public void ResetPushbackChance()
    {
        pushbackChance = 0;
        Debug.Log("Update pushback chance.  New value: " + pushbackChance);
    }

    public void UpdatePushbackChance(float multiplier)
    {
        pushbackChance = multiplier;
        Debug.Log("Update pushback chance.  New value: " + pushbackChance);
    }

    public bool CheckPushback()
    {
        // Debug.Log("Checking for pushback.");
        float randomValue = Random.Range(0f, 1f);
        return randomValue < pushbackChance;
    }
}

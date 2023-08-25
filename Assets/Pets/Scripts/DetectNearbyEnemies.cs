using UnityEngine;

public class DetectNearbyEnemies : MonoBehaviour
{
    public LayerMask enemyLayer; // Layer mask to specify which objects are considered enemies
    public float detectionRadius = 5f; // Radius to detect nearby enemies
    private float slowMultiplier;

    private void Start()
    {
        ResetSlowMultiplier();
    }

    private void Update()
    {
        // Detect nearby enemies using Physics.OverlapSphere
        Collider[] nearbyEnemies = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);

        // Apply or remove the slow effect based on nearby enemies
        foreach (Collider enemyCollider in nearbyEnemies)
        {
            Enemy enemy = enemyCollider.GetComponent<Enemy>();
            if (enemy != null && !enemy.isSlowed)
            {
                enemy.UpdateMoveSpeed(slowMultiplier);
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
}

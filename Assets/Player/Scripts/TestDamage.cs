using UnityEngine;

public class TestDamage : MonoBehaviour
{
    public int damageAmount = 25;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called!");
        // Check if the collider belongs to an enemy
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Player touched an enemy!");
            // Get the enemy's health component
            Enemy enemyHealth = other.GetComponent<Enemy>();

            // If the enemy has a health component, deal damage to it
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
            }
        }
    }
}

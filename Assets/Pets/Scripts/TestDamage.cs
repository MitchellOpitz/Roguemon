using UnityEngine;

public class TestDamage : MonoBehaviour
{
    public int damageAmount = 25; // Amount of damage to deal

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to an enemy
        if (other.CompareTag("Enemy"))
        {
            // Get the enemy's health component
            Enemy enemyHealth = other.GetComponent<Enemy>();

            // If the enemy has a health component, deal damage to it
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount * (damageAmount + GetComponent<Pet>().fightingMultiplier));
            }

            // Get the pet component
            Pet pet = GetComponent<Pet>();

            // If the pet has a component, deal damage to it
            if (pet != null)
            {
                pet.TakeDamage(damageAmount);
            }
        }
    }
}

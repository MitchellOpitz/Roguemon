using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;     // Current health of the enemy
    public float baseMoveSpeed = 3f;    // Speed at which the enemy moves
    public bool isSlowed;

    public float currentMoveSpeed;

    private void Start()
    {
        ResetMoveSpeed();
    }

    // Called to apply damage to the enemy
    public virtual void TakeDamage(float damage)
    {
        health -= damage; // Reduce health by the damage amount
        if (health <= 0f)
        {
            Die(); // If health drops to or below 0, the enemy dies
        }
    }

    // Called when the enemy dies
    protected virtual void Die()
    {
        // Common death behavior for all enemy types
        EnemySpawner spawner = FindAnyObjectByType<EnemySpawner>();
        spawner.EnemyDefeated(); // Notify the enemy spawner about the defeat
        Destroy(gameObject); // Destroy the enemy game object
    }

    public void ResetMoveSpeed()
    {
        currentMoveSpeed = baseMoveSpeed;
        isSlowed = false;
        Debug.Log("Updating enemy move speed.  New value: " + currentMoveSpeed);
    }

    public void UpdateMoveSpeed(float multiplier)
    {
        currentMoveSpeed = baseMoveSpeed - (baseMoveSpeed * multiplier);
        isSlowed = true;
        Debug.Log("Updating enemy move speed.  New value: " + currentMoveSpeed);
    }
}

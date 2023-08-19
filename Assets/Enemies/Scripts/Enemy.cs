using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public float moveSpeed = 3f;

    public virtual void TakeDamage(float damage)
    {
        //Debug.Log("Enemy taking " + damage + " damage.");
        health -= damage;
        if (health <= 0f)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // Common death behavior for all enemy types
        FindAnyObjectByType<EnemySpawner>().EnemyDefeated();
        Destroy(gameObject);
    }
}

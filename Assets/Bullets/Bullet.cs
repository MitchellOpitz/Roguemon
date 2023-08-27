using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;   // Speed of the bullet
    public float damage = 10f;  // Damage dealt to the target
    public bool isFlaming;
    public float fireMultiplier;

    private Transform target;   // The target to hit

    // Set the target for the bullet
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void Update()
    {
        // Check if there's a valid target
        if (target != null)
        {
            MoveToTarget();
            CheckHitTarget();
        }
        else
        {
            FindNewTarget();
            if (target != null)
            {
                Destroy(gameObject);
            }
        }
    }

    private void MoveToTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void CheckHitTarget()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget < 0.1f)
        {
            HitTarget();
        }
    }

    private void HitTarget()
    {
        // Handle damage to the target enemy
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            if (isFlaming)
            {
                enemy.ApplyBurnEffect(damage, fireMultiplier);
            }
        }

        Destroy(gameObject); // Destroy the bullet
    }

    private void FindNewTarget()
    {
        // Find all enemies in the scene with the "Enemy" tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length > 0)
        {
            // Find the nearest enemy
            GameObject nearestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

            target = nearestEnemy.transform;
        }
    }

    public void AddBurnEffect(float multiplier)
    {
        Debug.Log("Burn effect added to bullet.");
        isFlaming = true;
        fireMultiplier = multiplier;
    }
}

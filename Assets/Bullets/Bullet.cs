using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;   // Speed of the bullet
    public float damage = 10f;  // Damage dealt to the target
    public bool isFlaming;
    public float fireMultiplier;
    public bool isPoison;
    public float poisonDamage;
    public bool isElectric;
    public float electricBounces;
    public Enemy electricOriginalTarget = null;

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
        if (enemy != null && enemy != electricOriginalTarget)
        {
            enemy.TakeDamage(damage);

            if (isFlaming)
            {
                enemy.ApplyBurnEffect(damage, fireMultiplier);
            }

            if (isPoison)
            {
                enemy.ApplyPoisonEffect(damage, poisonDamage);
            }

            if (isElectric)
            {
                // Create a new projectile
                Bullet bullet = Instantiate(this, target.position, target.rotation);

                // Find all enemies in the scene with the "Enemy" tag
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                if (enemies.Length > 0)
                {
                    // Find the nearest enemy
                    GameObject nearestEnemy = null;
                    float closestDistance = Mathf.Infinity;

                    // Find the second closest enemy
                    GameObject secondClosestEnemy = null;
                    float secondClosestDistance = Mathf.Infinity;

                    foreach (GameObject newEnemy in enemies)
                    {
                        float distanceToEnemy = Vector3.Distance(transform.position, newEnemy.transform.position);

                        if (distanceToEnemy < closestDistance)
                        {
                            secondClosestDistance = closestDistance;
                            secondClosestEnemy = nearestEnemy;

                            closestDistance = distanceToEnemy;
                            nearestEnemy = newEnemy;
                        }
                        else if (distanceToEnemy < secondClosestDistance)
                        {
                            secondClosestDistance = distanceToEnemy;
                            secondClosestEnemy = newEnemy;
                        }
                    }

                    bullet.electricOriginalTarget = enemy;
                    bullet.electricBounces = electricBounces - 1;
                    bullet.target = secondClosestEnemy.transform;
                }

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

    public void AddPoisonEffect(float multiplier)
    {
        Debug.Log("Poison effect added to bullet.");
        isPoison = true;
        poisonDamage = multiplier;
    }
}

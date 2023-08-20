using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 10f;

    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);

            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget < 0.1f)
            {
                HitTarget();
            }
        }
    }

    private void HitTarget()
    {
        // Handle damage to the target enemy
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject); // Destroy the bullet
    }
}

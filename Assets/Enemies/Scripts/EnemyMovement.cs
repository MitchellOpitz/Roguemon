using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform target; // Target to move towards (the player)
    private Enemy enemy; // Reference to the Enemy script

    private void Start()
    {
        FindPlayer();
        enemy = GetComponent<Enemy>(); // Get the reference to the Enemy script
    }

    private void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    private void Update()
    {
        if (target != null)
        {
            // Calculate the direction to the target (player's current position)
            Vector3 direction = (target.position - transform.position).normalized;

            // Calculate the new position using linear interpolation
            Vector3 newPosition = transform.position + direction * enemy.moveSpeed * Time.deltaTime;

            // Move the enemy
            transform.position = newPosition;
        }
    }
}

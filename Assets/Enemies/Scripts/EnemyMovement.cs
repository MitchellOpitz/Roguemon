using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform target; // The target to move towards (usually the player)
    private Enemy enemy; // Reference to the Enemy script

    private void Start()
    {
        FindPlayer(); // Find and assign the target (player)
        enemy = GetComponent<Enemy>(); // Get the reference to the Enemy script component
    }

    private void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Find the player by tag
        if (player != null)
        {
            target = player.transform; // Set the player's transform as the target
        }
    }

    private void Update()
    {
        if (target != null)
        {
            MoveTowardsTarget(); // Move the enemy towards the target
        }
    }

    private void MoveTowardsTarget()
    {
        // Calculate the direction to the target (player's current position)
        Vector3 directionToTarget = (target.position - transform.position).normalized;

        // Calculate the new position using linear interpolation
        Vector3 newPosition = transform.position + directionToTarget * enemy.moveSpeed * Time.deltaTime;

        // Move the enemy to the new position
        transform.position = newPosition;
    }
}

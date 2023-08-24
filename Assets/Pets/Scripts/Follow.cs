using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target; // Reference to the target pet to follow
    public float followDistance = 2f; // Distance between pets
    private float moveSpeed;

    private void Start()
    {
        // Get the move speed from the attached PlayerMovement component
        moveSpeed = GetComponent<PlayerMovement>().moveSpeed;
    }

    private void Update()
    {
        if (target != null)
        {
            // Calculate the position behind the target pet
            Vector3 targetPosition = target.position - target.forward * followDistance;

            // Calculate the desired position for smooth movement
            Vector3 desiredPosition = targetPosition;
            if (Vector3.Distance(transform.position, targetPosition) > followDistance)
            {
                desiredPosition = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }

            // Move the current pet towards the desired position
            transform.position = desiredPosition;

            // Rotate the pet smoothly to match the target's rotation
            Quaternion targetRotation = Quaternion.LookRotation(target.forward, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, moveSpeed * Time.deltaTime);
        }
    }
}

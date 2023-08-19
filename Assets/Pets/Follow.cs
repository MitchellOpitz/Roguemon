using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target; // Reference to the target pet to follow
    public float followDistance = 5.2f; // Distance between pets
    public float moveSpeed = 2.0f; // Speed at which followers move towards the target

    private Vector3 lastPosition; // Store the last target position

    private void Start()
    {
        if (target != null)
        {
            lastPosition = target.position;
        }
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position - target.forward * followDistance;

            // Calculate the movement vector
            Vector3 moveVector = targetPosition - lastPosition;

            // Calculate the desired position behind the target pet
            Vector3 desiredPosition = transform.position + moveVector;

            // Move the current pet towards the desired position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, moveSpeed * Time.deltaTime);

            // Store the current target position for the next frame
            lastPosition = target.position;

            // Rotate the pet smoothly to match the target's rotation
            Quaternion targetRotation = Quaternion.LookRotation(target.forward, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, moveSpeed * Time.deltaTime);
        }
    }
}

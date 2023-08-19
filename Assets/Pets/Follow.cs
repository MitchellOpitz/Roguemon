using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target; // Reference to the target pet to follow
    public float followDistance = 2f; // Distance between pets
    public float moveSpeed = 2.0f; // Speed at which followers move towards the target

    private void Update()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position - target.forward * followDistance;

            // Calculate the desired position behind the target pet
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

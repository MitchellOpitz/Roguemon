using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the player moves

    private Rigidbody rb; // Reference to the Rigidbody component

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component on start
    }

    private void Update()
    {
        // Get input values for horizontal and vertical movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement vector based on input and speed
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;

        // Calculate the new position after applying movement
        Vector3 newPosition = rb.position + movement;

        // Clamp the new position within the specified bounds
        newPosition.x = Mathf.Clamp(newPosition.x, Clamp.Instance.minX, Clamp.Instance.maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, Clamp.Instance.minZ, Clamp.Instance.maxZ);

        // Move the player to the new position
        rb.MovePosition(newPosition);

        // Rotate the object towards the direction of movement
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.MoveRotation(targetRotation);
        }
    }
}

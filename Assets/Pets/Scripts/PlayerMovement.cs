using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;

        Vector3 newPosition = rb.position + movement;

        // Clamp the new position within the specified bounds
        newPosition.x = Mathf.Clamp(newPosition.x, Clamp.Instance.minX, Clamp.Instance.maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, Clamp.Instance.minZ, Clamp.Instance.maxZ);

        rb.MovePosition(newPosition);

        // Rotate the object towards the direction of movement
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.MoveRotation(targetRotation);
        }
    }
}

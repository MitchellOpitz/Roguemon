using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float baseMoveSpeed = 5f; // Speed at which the player moves

    private Rigidbody rb; // Reference to the Rigidbody component
    private float currentMoveSpeed;

    private Animator _animator; // Reference to Animation controller

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component on start
        _animator= GetComponentInChildren<Animator>(); //Get the Animation controller on start
        ResetSpeed();

    }

    private void Update()
    {
        // Get input values for horizontal and vertical movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement vector based on input and speed
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * currentMoveSpeed * Time.deltaTime;

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
            _animator.SetBool("isWalking", true);
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.MoveRotation(targetRotation);
        }
        else {
            _animator.SetBool("isWalking", false);
        }
    }

    public void ResetSpeed()
    {
        currentMoveSpeed = baseMoveSpeed;
        // Debug.Log("Updating movement speed.  New speed: " + currentMoveSpeed);
    }

    public void UpdateSpeed(float multiplier)
    {
        currentMoveSpeed = baseMoveSpeed * (1 + multiplier);
        // Debug.Log("Updating movement speed.  New speed: " + currentMoveSpeed);
    }
}

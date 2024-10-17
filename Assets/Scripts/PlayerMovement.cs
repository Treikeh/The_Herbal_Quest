using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Max speed the player can move
    public float moveSpeed = 4.0f;
    // How fast to player can reacn max speed
    public float acceleration = 10.0f;
    // How much force to apply the the player when jumping
    public float jumpForce = 5.0f;
    // Graound mask
    public LayerMask groundLayer;
    public Transform groundCheckOrigin;
    public float groundCheckRadius = 0.1f;

    // Direction the player will move inn
    private Vector3 moveDirection;
    // Rigidbody reference
    private Rigidbody rBody;   
    private bool isGrounded;
    // Start is called before the first frame update
    private void Start()
    {
        // Get the rigidbody
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Align moveDirecion to player transform
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        moveDirection = (transform.forward * verticalInput) + (transform.right * horizontalInput);
        moveDirection = moveDirection.normalized;

        // Jump when pressing Space
        if (Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }
    }

    private void FixedUpdate() {
        // Check if player is grounded
        isGrounded = Physics.CheckSphere(groundCheckOrigin.position, groundCheckRadius, groundLayer);
        // Apply movement
        Movement();
    }

    private void Movement() {
        // Get the old rigidbody velocity
        Vector3 velocity = rBody.velocity;

        // Set new velocity
        velocity.x = Mathf.Lerp(velocity.x, moveDirection.x * moveSpeed, acceleration * Time.fixedDeltaTime);
        velocity.z = Mathf.Lerp(velocity.z, moveDirection.z * moveSpeed, acceleration * Time.fixedDeltaTime);

        // Apply new velocity
        rBody.velocity = velocity;
    }

    private void Jump() {
        if (isGrounded) {
            rBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    // How fast the player can move
    public float maxSpeed = 4.0f;
    // How fast player reaches max speed
    public float acceleration = 10.0f;
    // Force to apply when jumping
    public float jumpForce = 5.0f;
    public Transform moveTransform;

    [Header("Ground check")]
    // How far the ground check should reach
    public float groundCheckRadius = 0.1f;
    // Graound mask
    public LayerMask groundLayer;
    public Transform groundCheckOrigin;
    private bool isGrounded;

    // Direction the player will move inn
    private Vector3 moveDirection;
    // Rigidbody reference
    private Rigidbody rBody;   


    private void Start()
    {
        // Get the rigidbody
        rBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Check if player is grounded
        isGrounded = Physics.CheckSphere(groundCheckOrigin.position, groundCheckRadius, groundLayer);

        // Apply movement
        // Get the old rigidbody velocity
        Vector3 velocity = rBody.velocity;

        // Set new velocity
        velocity.x = Mathf.Lerp(velocity.x, moveDirection.x * maxSpeed, acceleration * Time.fixedDeltaTime);
        velocity.z = Mathf.Lerp(velocity.z, moveDirection.z * maxSpeed, acceleration * Time.fixedDeltaTime);

        // Apply new velocity
        rBody.velocity = velocity;
    }

    public void UpdateMoveInput(Vector2 moveInput)
    {
        moveDirection = (moveTransform.transform.forward * moveInput.y) + (moveTransform.transform.right * moveInput.x);
        moveDirection = moveDirection.normalized;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}

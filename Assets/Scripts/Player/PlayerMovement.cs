using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerData playerData;

    // Sounds should be moved
    public string walkSound;
    public string jumpSound;

    public Transform moveTransform;
    public Transform groundCheckOrigin;

    private Vector2 moveInput;
    private Vector3 moveDirection;
    private Rigidbody rBody;   


    private void Start()
    {
        // Get the rigidbody
        rBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveDirection = (moveTransform.transform.forward * moveInput.y) + (moveTransform.transform.right * moveInput.x);
        moveDirection = moveDirection.normalized;
    }

    private void FixedUpdate()
    {
        // Check if player is grounded
        playerData.isGrounded = Physics.CheckSphere(groundCheckOrigin.position, playerData.groundCheckRadius, playerData.groundLayer);

        // Apply movement
        // Get the old rigidbody velocity
        Vector3 velocity = rBody.velocity;

        // Set new velocity
        velocity.x = Mathf.Lerp(velocity.x, moveDirection.x * playerData.walkSpeed, playerData.acceleration * Time.fixedDeltaTime);
        velocity.z = Mathf.Lerp(velocity.z, moveDirection.z * playerData.walkSpeed, playerData.acceleration * Time.fixedDeltaTime);

        // Apply new velocity
        rBody.velocity = velocity;
    }

    public void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }

    public void OnJump()
    {
        if (playerData.isGrounded)
        {
            rBody.AddForce(Vector3.up * playerData.jumpForce, ForceMode.Impulse);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float acceleration = 10f;
    public float jumpForce = 5f;
    public Transform cameraYaw;
    public AudioClip jumpSound;

    [Header("Ground check")]
    public bool isGrounded;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    public Transform groundCheckOrigin;

    private Rigidbody rBody;
    private Vector2 moveInput;
    private Vector3 moveDirection;


    private void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Align moveInput to face cameraPitch Direction
        moveDirection = (cameraYaw.transform.forward * moveInput.y) + (cameraYaw.transform.right * moveInput.x);
        moveDirection = moveDirection.normalized;
    }

    private void FixedUpdate()
    {
        // Check if player is grounded
        isGrounded = Physics.CheckSphere(groundCheckOrigin.position, groundCheckRadius, groundLayer);

        // Apply movement
        // Get the old rigidbody velocity
        Vector3 velocity = rBody.velocity;

        // Set new velocity
        velocity.x = Mathf.Lerp(velocity.x, moveDirection.x * walkSpeed, acceleration * Time.fixedDeltaTime);
        velocity.z = Mathf.Lerp(velocity.z, moveDirection.z * walkSpeed, acceleration * Time.fixedDeltaTime);

        // Apply new velocity
        rBody.velocity = velocity;
    }

    public void OnJump()
    {
        if (isGrounded && !GameManager.isGamePaused)
        {
            rBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            AudioSource.PlayClipAtPoint(jumpSound, gameObject.transform.position);
        }
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float acceleration = 10f;
    public float maxSlopeAngle = 45f;
    public float jumpForce = 5f;
    public float jumpBufferDuration = 0.2f;
    public Transform cameraYaw;
    public AudioClip jumpSound;

    [Header("Ground check")]
    public bool isGrounded = true;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    public Transform groundCheckOrigin;

    private Rigidbody rBody;
    public bool canJump;
    private float jumpBufferCount;
    private RaycastHit slopeHit;
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

        canJump = isGrounded;
        jumpBufferCount -= Time.deltaTime;
        if (canJump && jumpBufferCount > 0f)
        {
            jumpBufferCount = 0f;
            rBody.velocity = new Vector3(rBody.velocity.x, jumpForce, rBody.velocity.z);
            AudioSource.PlayClipAtPoint(jumpSound, gameObject.transform.position);
        }
    }

    private void FixedUpdate()
    {
        // Check if player is grounded
        isGrounded = Physics.CheckSphere(groundCheckOrigin.position, groundCheckRadius, groundLayer);;
        rBody.useGravity = !isGrounded;

        // Apply movement
        // Get the old rigidbody velocity
        Vector3 velocity = rBody.velocity;

        // Set new velocity
        velocity.x = Mathf.Lerp(velocity.x, moveDirection.x * walkSpeed, acceleration * Time.fixedDeltaTime);
        velocity.z = Mathf.Lerp(velocity.z, moveDirection.z * walkSpeed, acceleration * Time.fixedDeltaTime);

        // Apply new velocity
        rBody.velocity = velocity;
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump()
    {
        if (!GameManager.isGamePaused)
        {
            canJump = false;
            jumpBufferCount = jumpBufferDuration;
        }
    }
}

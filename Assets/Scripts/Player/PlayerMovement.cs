using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float airAcceleration = 5f;
    [SerializeField] private float groundAcceleration = 10f;
    [SerializeField] float maxFloorAngle = 40f;
    [SerializeField] private float groundCheckDistance = 1.3f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float  jumpForce = 5f;
    [SerializeField] private float jumpBufferDuration = 0.2f;
    [SerializeField] private float coyoteTimeDuration = 0.2f;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private Transform orientation;


    private Rigidbody rBody;
    private RaycastHit groundHit;
    private bool isGrounded;
    private bool canJump = true;
    private bool exitGround;
    private float jumpCooldown = 0.25f;
    private float jumpBufferCount;
    private float coyoteTimeCount;
    private Vector2 moveInput;
    private Vector3 moveDirection;
    private Vector3 checkpointPosition;


    private void OnEnable() {
        CheckpointManager.CheckpointSaved += OnCheckpointSaved;
        CheckpointManager.CheckpointReloaded += OnCheckpointReloaded;
    }

    private void OnDisable() {
        CheckpointManager.CheckpointSaved -= OnCheckpointSaved;
        CheckpointManager.CheckpointReloaded -= OnCheckpointReloaded;
    }



    private void Start()
    {
        rBody = GetComponent<Rigidbody>();
        OnCheckpointSaved();
    }

    private void Update()
    {
        // Ground check
        isGrounded = IsOnWalkableSlope();
        // Disable gravity while grounded
        rBody.useGravity = !isGrounded;

        // Align moveDirection to orientation
        moveDirection = (orientation.forward * moveInput.y + orientation.right * moveInput.x).normalized;

        // Jump while jump buffer active
        jumpBufferCount -= Time.deltaTime;
        if (isGrounded && canJump && jumpBufferCount > 0f)
        {
            ApplyJumpForce();
        }
    }

    private void FixedUpdate()
    {        
        Vector3 velocity = rBody.velocity;

        if (isGrounded && !exitGround)
        {
            // Grounded
            Vector3 slopeDirection = GetSlopeMoveDirection();
            velocity.x = Mathf.Lerp(velocity.x, slopeDirection.x * walkSpeed, groundAcceleration * Time.fixedDeltaTime);
            velocity.y = Mathf.Lerp(velocity.y, slopeDirection.y * walkSpeed, groundAcceleration * Time.fixedDeltaTime);
            velocity.z = Mathf.Lerp(velocity.z, slopeDirection.z * walkSpeed, groundAcceleration * Time.fixedDeltaTime);
            coyoteTimeCount = coyoteTimeDuration;
        }
        else
        {
            // Airborne
            velocity.x = Mathf.Lerp(velocity.x, moveDirection.x * walkSpeed, airAcceleration * Time.fixedDeltaTime);
            velocity.z = Mathf.Lerp(velocity.z, moveDirection.z * walkSpeed, airAcceleration * Time.fixedDeltaTime);
            // Coyote time
            coyoteTimeCount -= Time.fixedDeltaTime;
        }

        rBody.velocity = velocity;
    }

    // Move input
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // Jump input
    public void OnJump()
    {
        if (GameManager.CurrenctGameState == GameManager.GameStates.Running)
        {
            // Coyote time
            if (!exitGround && canJump && coyoteTimeCount > 0f)
            {
                ApplyJumpForce();
            }
            jumpBufferCount = jumpBufferDuration;
        }
    }

    private void ApplyJumpForce()
    {
        canJump = false;
        exitGround = true;
        coyoteTimeCount = 0f;
        jumpBufferCount = 0f;
        // Reset jump after a short duration
        Invoke(nameof(ResetJump), jumpCooldown);
        rBody.velocity = new Vector3(rBody.velocity.x, jumpForce, rBody.velocity.z);
        AudioSource.PlayClipAtPoint(jumpSound, transform.position);
    }

    private void ResetJump()
    {
        canJump = true;
        exitGround = false;
    }

    // Check if player is on a slope shallow enough to walk on
    private bool IsOnWalkableSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out groundHit, groundCheckDistance, groundLayer))
        {
            float floorAngle = Vector3.Angle(groundHit.normal, Vector3.up);
            return floorAngle < maxFloorAngle;
        }
        return false;
    }

    // Align moveDirection to ground normal
    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, groundHit.normal);
    }

    private void OnCheckpointSaved()
    {
        checkpointPosition = transform.position;
    }

    private void OnCheckpointReloaded()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
        playerInput.ActivateInput();
        rBody.velocity = Vector3.zero;
        transform.position = checkpointPosition;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * groundCheckDistance));
    }
}

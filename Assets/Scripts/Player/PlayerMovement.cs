using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float acceleration = 10f;
    public float maxFloorAngle = 40f;
    public float groundCheckDistance = 1.3f;
    public LayerMask groundLayer;
    public float  jumpForce = 5f;
    public float jumpCooldown = 0.25f;
    public float jumpBufferDuration = 0.2f;
    public AudioClip jumpSound;
    public Transform orientation;

    private Rigidbody rBody;
    private RaycastHit groundHit;
    private bool isGrounded;
    private bool canJump = true;
    private bool exitGround;
    private float jumpBufferCount;
    private Vector2 moveInput;
    private Vector3 moveDirection;


    private void Start()
    {
        rBody = GetComponent<Rigidbody>();
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
            canJump = false;
            exitGround = true;
            jumpBufferCount = 0f;
            // Reset jump after a short duration
            Invoke(nameof(ResetJump), jumpCooldown);
            rBody.velocity = new Vector3(rBody.velocity.x, jumpForce, rBody.velocity.z);
            AudioSource.PlayClipAtPoint(jumpSound, transform.position);
        }
    }

    private void FixedUpdate()
    {
        Vector3 velocity = rBody.velocity;

        if (isGrounded && !exitGround)
        {
            Vector3 slopeDirection = GetSlopeMoveDirection();
            velocity.x = Mathf.Lerp(velocity.x, slopeDirection.x * walkSpeed, acceleration * Time.fixedDeltaTime);
            velocity.y = Mathf.Lerp(velocity.y, slopeDirection.y * walkSpeed, acceleration * Time.fixedDeltaTime);
            velocity.z = Mathf.Lerp(velocity.z, slopeDirection.z * walkSpeed, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            velocity.x = Mathf.Lerp(velocity.x, moveDirection.x * walkSpeed, acceleration * Time.fixedDeltaTime);
            velocity.z = Mathf.Lerp(velocity.z, moveDirection.z * walkSpeed, acceleration * Time.fixedDeltaTime);
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
        if (!GameManager.isGamePaused)
            { jumpBufferCount = jumpBufferDuration; }
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

    private void OnDrawGizmosSelected() {
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * groundCheckDistance));
    }
}

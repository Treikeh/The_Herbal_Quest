using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float WalkSpeed = 5f;
    [SerializeField] private float AirAcceleration = 5f;
    [SerializeField] private float GroundAcceleration = 10f;
    [SerializeField] float MaxFloorAngle = 40f;
    [SerializeField] private float GroundCheckDistance = 1.3f;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private float  JumpForce = 5f;
    [SerializeField] private float JumpCooldown = 0.25f;
    [SerializeField] private float JumpBufferDuration = 0.2f;
    [SerializeField] private AudioClip JumpSound;
    [SerializeField] private Transform Orientation;

    private Rigidbody _rBody;
    private RaycastHit _groundHit;
    private bool _isGrounded;
    private bool _canJump = true;
    private bool _exitGround;
    private float _jumpBufferCount;
    private Vector2 _moveInput;
    private Vector3 _moveDirection;


    private void Start()
    {
        _rBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Ground check
        _isGrounded = IsOnWalkableSlope();
        // Disable gravity while grounded
        _rBody.useGravity = !_isGrounded;

        // Align _moveDirection to Orientation
        _moveDirection = (Orientation.forward * _moveInput.y + Orientation.right * _moveInput.x).normalized;

        // Jump while jump buffer active
        _jumpBufferCount -= Time.deltaTime;
        if (_isGrounded && _canJump && _jumpBufferCount > 0f)
        {
            _canJump = false;
            _exitGround = true;
            _jumpBufferCount = 0f;
            // Reset jump after a short duration
            Invoke(nameof(ResetJump), JumpCooldown);
            _rBody.velocity = new Vector3(_rBody.velocity.x, JumpForce, _rBody.velocity.z);
            AudioSource.PlayClipAtPoint(JumpSound, transform.position);
        }
    }

    private void FixedUpdate()
    {
        Vector3 velocity = _rBody.velocity;

        if (_isGrounded && !_exitGround)
        {
            Vector3 slopeDirection = GetSlopeMoveDirection();
            velocity.x = Mathf.Lerp(velocity.x, slopeDirection.x * WalkSpeed, GroundAcceleration * Time.fixedDeltaTime);
            velocity.y = Mathf.Lerp(velocity.y, slopeDirection.y * WalkSpeed, GroundAcceleration * Time.fixedDeltaTime);
            velocity.z = Mathf.Lerp(velocity.z, slopeDirection.z * WalkSpeed, GroundAcceleration * Time.fixedDeltaTime);
        }
        else
        {
            velocity.x = Mathf.Lerp(velocity.x, _moveDirection.x * WalkSpeed, AirAcceleration * Time.fixedDeltaTime);
            velocity.z = Mathf.Lerp(velocity.z, _moveDirection.z * WalkSpeed, AirAcceleration * Time.fixedDeltaTime);
        }

        _rBody.velocity = velocity;
    }

    // Move input
    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    // Jump input
    public void OnJump()
    {
        if (GameManager.currenctGameState == GameManager.GameStates.Running)
            { _jumpBufferCount = JumpBufferDuration; }
    }

    private void ResetJump()
    {
        _canJump = true;
        _exitGround = false;
    }

    // Check if player is on a slope shallow enough to walk on
    private bool IsOnWalkableSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out _groundHit, GroundCheckDistance, GroundLayer))
        {
            float floorAngle = Vector3.Angle(_groundHit.normal, Vector3.up);
            return floorAngle < MaxFloorAngle;
        }
        return false;
    }

    // Align _moveDirection to ground normal
    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(_moveDirection, _groundHit.normal);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * GroundCheckDistance));
    }
}

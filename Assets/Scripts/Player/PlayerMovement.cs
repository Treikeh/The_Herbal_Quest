using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float acceleration = 10f;
    public float groundDrag = 1f;
    public float groundCheckDistance = 0.75f;
    public LayerMask groundLayer;
    public Transform orientation;

    private Rigidbody rBody;
    private bool isGrounded;
    private Vector2 moveInput;
    private Vector3 moveDirection;


    private void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Ground check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        LimitVelocity();

        // Apply drag when grounded
        if (isGrounded)
        {
            rBody.drag = groundDrag;
            rBody.useGravity = false;
        }
        // If airborne disable drag
        else
        {
            rBody.drag = 0f;
            rBody.useGravity = true;
        }
    }

    private void FixedUpdate()
    {
        // Move player
        moveDirection = orientation.forward * moveInput.y + orientation.right * moveInput.x;
        Debug.Log(moveDirection.magnitude);
        rBody.AddForce(moveDirection.normalized * walkSpeed * acceleration, ForceMode.Force);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void LimitVelocity()
    {
        Vector3 xzVelocity = new Vector3(rBody.velocity.x, 0.0f, rBody.velocity.z);
        // Limit velocity if greater than walk speed
        if (xzVelocity.magnitude > walkSpeed)
        {
            Vector3 newVelocity = xzVelocity.normalized * walkSpeed;
            rBody.velocity = new Vector3(newVelocity.x, rBody.velocity.y, newVelocity.z);
        }
    }
}

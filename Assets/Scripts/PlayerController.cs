using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerData playerData;

    [Header("Camera")]
    public float rayDistance = 2f;
    public float cameraRotationLimit = 89f;
    public Transform cameraYaw;
    public Transform cameraPitch;
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float acceleration = 10f;
    public float jumpForce = 5f;
    public AudioClip jumpSound;
    [Header("Ground check")]
    public bool isGrounded;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    public Transform groundCheckOrigin;
    [Header("Attacking")]
    public float attackDamage = 5f;
    public float attackSpeed = 0.5f;
    public AudioClip attackHitSound;
    public AudioClip attackMissSound;
    public AudioSource attackAudioSource;
    public Animator attackAnimations;
    
    private Vector2 lookInput;
    private Vector2 cameraRotation;
    private Vector2 moveInput;
    private Vector3 moveDirection;
    private Interactable interactTarget;
    private bool canAttack = true;
    private Health attackTarget;
    private Rigidbody rBody;

    private void Start()
    {
        // Hide mouse cursor
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        playerData.interactPrompt = "";
        playerData.displayAttackIcon = false;
        
        rBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (GameManager.isGamePaused == true)
        {
            return;
        }
        
        CheckForInteractable();
        CheckForAttackable();

        // Camera rotation
        cameraRotation.x = lookInput.x * (playerData.cameraSensitivity * Time.deltaTime);
        cameraRotation.y -= lookInput.y * (playerData.cameraSensitivity * Time.deltaTime);
        // Limit how far up and down the camera can rotate
        cameraRotation.y = Mathf.Clamp(cameraRotation.y, -cameraRotationLimit, cameraRotationLimit);
        // Convert rotation.y to Quaternion
        var yQuat = Quaternion.AngleAxis(cameraRotation.y, Vector3.right);

        // Apply rotation
        cameraYaw.Rotate(Vector3.up * cameraRotation.x);
        cameraPitch.localRotation = yQuat;

        // Align moveInput to face cameraPitch Direction
        moveDirection = (cameraYaw.transform.forward * moveInput.y) + (cameraYaw.transform.right * moveInput.x);
        moveDirection = moveDirection.normalized;
    }

    private void FixedUpdate()
    {
        if (GameManager.isGamePaused == true)
        {
            return;
        }

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

    // Check if the player is looking at an interactable object
    private void CheckForInteractable()
    {
        Ray ray = new Ray(cameraPitch.position, cameraPitch.forward);
        if (Physics.Raycast(ray, out RaycastHit rayHit, rayDistance))
        {
            if (rayHit.collider.TryGetComponent(out Interactable target))
            {
                interactTarget = target;
                playerData.interactPrompt = target.Prompt;
            }
            else if (interactTarget != null) // Clear if rayHit collider does not have an Interactable component
            {
                interactTarget = null;
                playerData.interactPrompt = "";
            }
        }
        else if (interactTarget != null) // Clear if raycast is not hitting anything
        {
            interactTarget = null;
            playerData.interactPrompt = "";
        }
    }

    // Check if the player is looking at an attackable object
    private void CheckForAttackable()
    {
        Ray ray = new Ray(cameraPitch.position, cameraPitch.forward);
        if (Physics.Raycast(ray, out RaycastHit rayHit, rayDistance))
        {
            if (rayHit.collider.TryGetComponent(out Health target))
            {
                attackTarget = target;
                playerData.displayAttackIcon = true;
            }
            else if (attackTarget != null) // Clear if rayHit collider does not have an health component
            {
                attackTarget = null;
                playerData.displayAttackIcon = false;
            }
        }
        else if (attackTarget != null) // Clear if raycast is not hitting anything
        {
            attackTarget = null;
            playerData.displayAttackIcon = false;
        }
    }

// INPUTS

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump()
    {
        if (GameManager.isGamePaused == true)
        {
            return;
        }
        
        if (isGrounded)
        {
            rBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            AudioSource.PlayClipAtPoint(jumpSound, gameObject.transform.position);
        }
    }

    public void OnInteract()
    {
        // Disable interacting when the cursor is visable
        if (Cursor.visible == true)
        {
            return;
        }

        if (interactTarget != null)
        {
            interactTarget.Interact();
        }
    }

    public void OnAttack()
    {
        // Disable attacking when the cursor is visable
        if (Cursor.visible == true)
        {
            return;
        }

        if (!canAttack)
        {
            Debug.Log("Player can't attack");
            return;
        }
        else
        {
            canAttack = false;
            StartCoroutine(AttackDelay());
            attackAnimations.Play("AttackAnim", -1, 0f);
            
            if (attackTarget != null)
            {
                attackTarget.TakeDamage(attackDamage);
                attackAudioSource.PlayOneShot(attackHitSound);
            }
            else
            {
                attackAudioSource.PlayOneShot(attackMissSound);
            }
        }
    }

// GameEvents Responses

    public void OnExtinguishTorch()
    {
        //
    }

    public void OnLightTorch()
    {
        //
    }


// IEnumerators

    // Delay when the player can attack again
    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }
}

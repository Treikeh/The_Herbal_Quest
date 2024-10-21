using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInputs : MonoBehaviour
{
    public PlayerDatas playerData;

    private Vector2 moveInput;
    private Vector2 lookInput;

    // Input events 
    // By doing it this way the different player components doesn't have to keep references to other player components which ->
    // <- keeps the code decoupled and (arguably) easier to work with
    // For example, When the player presses the WASD keys to move the this component takes that info and sends it to the ->
    // <- PlayerMovement component using the OnMoveInput action 
    public UnityEvent<Vector2> OnLookInput;
    public UnityEvent<Vector2> OnMoveInput;
    public UnityEvent OnJumpInput;
    public UnityEvent OnInteractInput;
    public UnityEvent OnAttackInput;


    private void Start()
    {
        // Lock mouse cursor
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Movement Input
        moveInput = Vector2.zero;

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        
        OnMoveInput.Invoke(moveInput);

        // Camera Input
        lookInput = Vector2.zero;

        lookInput.x = Input.GetAxis("Mouse X") * (playerData.cameraSensitivity * Time.deltaTime);
        lookInput.y = Input.GetAxis("Mouse Y") * (playerData.cameraSensitivity * Time.deltaTime);

        OnLookInput.Invoke(lookInput);

        // Jump Input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpInput.Invoke();
        }

        // Ineract input
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteractInput.Invoke();
        }

        // Attack input
        if (Input.GetMouseButtonDown(0))
        {
            OnAttackInput.Invoke();
        }
    }

    public void DisableInput()
    {
        // Stop Movement
        moveInput = Vector2.zero;
        OnMoveInput.Invoke(moveInput);
        // Stop Camera
        lookInput = Vector2.zero;
        OnLookInput.Invoke(lookInput);
        // Show mouse cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Disable this component
        enabled = false;
    }
}
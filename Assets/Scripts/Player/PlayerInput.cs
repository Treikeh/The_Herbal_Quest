using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerData playerData;

    private Vector2 moveInput;
    private Vector2 lookInput;


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
        
        playerData.Events.OnMoveInput?.Invoke(moveInput);

        // Camera Input
        lookInput = Vector2.zero;

        lookInput.x = Input.GetAxis("Mouse X") * (playerData.cameraSensitivity * Time.deltaTime);
        lookInput.y = Input.GetAxis("Mouse Y") * (playerData.cameraSensitivity * Time.deltaTime);

        playerData.Events.OnLookInput?.Invoke(lookInput);

        // Jump Input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerData.Events.OnJumpInput?.Invoke();
        }

        // Ineract input
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerData.Events.OnInteraction?.Invoke();
        }

        // Attack input
        if (Input.GetMouseButtonDown(0))
        {
            playerData.Events.OnAttacking?.Invoke();
        }
    }

    public void DisableInput()
    {
        // Stop Movement
        moveInput = Vector2.zero;
        playerData.Events.OnMoveInput?.Invoke(moveInput);
        // Stop Camera
        lookInput = Vector2.zero;
        playerData.Events.OnLookInput?.Invoke(lookInput);
        // Show mouse cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Disable this component
        enabled = false;
    }
}
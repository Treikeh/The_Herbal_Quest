using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Interact")]
    // Distance player can interact with objects
    [SerializeField] private float interactDistance = 3f;
    // If the player is looking at an interactable object this will be valid and data can be retrived from it.
    private IInteract currentInteractable;

    // Event used to update the UI interact prompt
    public static event Action<string> UpdateInteractPrompt;

    private void Update()
    {
        // Check for interactable object
        CheckInteraction();
        // If has interactable object and is pressing E
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            // Interact with the interactable object
            currentInteractable.Interact();
        }
    }

    // Check if the player is looking at an interactable object
    private void CheckInteraction()
    {
        // Disable currentInteractable in case the player is no longer looking at an interactable object
        // Also make sure we have a currentInteractable to disable
        // You can do this in the raycast if statment at any point where it doesn't hit an interactable object
        if (currentInteractable != null)
        {
            // Clear ui text element
            UpdateInteractPrompt?.Invoke("");
            currentInteractable = null;
        }

        // Raycast variables
        RaycastHit rayHit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        // If the raycast collides with anything within interactionDistance
        if (Physics.Raycast(ray, out rayHit, interactDistance))
        {
            // Check if collider has the Iinteractable interface
            if (rayHit.collider.TryGetComponent(out IInteract interactableObject))
            {
                currentInteractable = interactableObject;
                // Update ui element to match interaction prompt
                UpdateInteractPrompt?.Invoke(interactableObject.interactPrompt);
            } // Here and bellow you can add an else statment to disable currentInteractable
        }
    }
}

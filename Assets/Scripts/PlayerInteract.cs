using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactDistance = 3f;

    private IInteract currentInteractable;

    public static event Action<string> UpdateInteractPrompt;


    private void Update()
    {
        // Check if the player is looking at an interactable object
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit rayHit, interactDistance))
        {
            // Check if collider has the Iinteractable interface
            if (rayHit.collider.TryGetComponent(out IInteract target))
            {
                currentInteractable = target;
                // Update ui element to match interaction prompt
                UpdateInteractPrompt?.Invoke(target.interactPrompt);
            }
            else
            {
                ClearInteractable();
            }
        }
        else
        {
            ClearInteractable();
        }
    }

    private void ClearInteractable()
    {
        if (currentInteractable != null)
        {
            // Clear ui text element
            UpdateInteractPrompt?.Invoke("");
            currentInteractable = null;
        }
    }

    // Called when player presses the Interact input
    public void InteractWithObject()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }
}

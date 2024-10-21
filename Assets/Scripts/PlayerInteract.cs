using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactDistance = 3f;

    private Interactable interactTarget;

    public static event Action<string> UpdateInteractPrompt;


    private void Update()
    {
        // Check if the player is looking at an interactable object
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit rayHit, interactDistance))
        {
            // Check if collider has the Iinteractable interface
            if (rayHit.collider.TryGetComponent(out Interactable target))
            {
                interactTarget = target;
                // Update ui element to match interaction prompt
                UpdateInteractPrompt?.Invoke(target.Prompt);
            }
            else
            {
                ClearInteractTarget();
            }
        }
        else
        {
            ClearInteractTarget();
        }
    }

    private void ClearInteractTarget()
    {
        if (interactTarget != null)
        {
            // Clear ui text element
            UpdateInteractPrompt?.Invoke("");
            interactTarget = null;
        }
    }

    // Called when player presses the Interact input
    public void InteractWithTarget()
    {
        if (interactTarget != null)
        {
            interactTarget.Interact();
        }
    }
}

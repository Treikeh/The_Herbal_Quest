using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public PlayerData playerData;

    private Interactable interactTarget;


    private void Update()
    {
        // Check if the player is looking at an interactable object
        Ray ray = new Ray(playerData.cameraTransform.position, playerData.cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit rayHit, playerData.interactDistance))
        {
            // Check if collider has the Iinteractable interface
            if (rayHit.collider.TryGetComponent(out Interactable target))
            {
                interactTarget = target;
                playerData.interactPrompt = target.Prompt;
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
            playerData.interactPrompt = "";
            interactTarget = null;
        }
    }

    // Called when player presses the Interact input
    public void OnInteract()
    {
        if (interactTarget != null)
        {
            interactTarget.Interact();
        }
    }
}

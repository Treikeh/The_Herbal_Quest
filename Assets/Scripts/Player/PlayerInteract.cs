using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public PlayerData playerData;
    public StringAsset interactPrompt;

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
                interactPrompt.value = target.Prompt;
            }
            else if (interactTarget != null) // Clear if rayHit collider does not have an interactable component
            {
                interactTarget = null;
                interactPrompt.value = "";
            }
        }
        else if (interactTarget != null) // Clear if raycast is not hitting anything
        {
            interactTarget = null;
            interactPrompt.value = "";
        }
    }

    // Called when player presses the Interact button
    public void OnInteract()
    {
        if (interactTarget != null)
        {
            interactTarget.Interact();
        }
    }
}

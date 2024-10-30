using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] private Transform head;

    private Interactable interactTarget;

    public static Action<string> UpdateInteractPrompt;



    private void Update()
    {
        // Check for interactable
        if (Physics.Raycast(head.position, head.forward, out RaycastHit rayHit, interactDistance))
        {
            if (rayHit.collider.TryGetComponent(out Interactable target))
            {
                interactTarget = target;
                UpdateInteractPrompt?.Invoke(target.Prompt);
            } 
            else
                { ClearInteractTarget(); } // Clear if rayHi.collider does not have an Interactable component
        } 
        else
            { ClearInteractTarget(); } // Clear if raycast is not hitting anything
    }

    private void ClearInteractTarget()
    {
        UpdateInteractPrompt?.Invoke("");
        if (interactTarget != null)
            { interactTarget = null; }
    }

    public void OnInteract()
    {
        // Only allow interaction when the cursor is hidden and when interactTarget is valid
        if (!Cursor.visible && interactTarget != null)
            { interactTarget.Interact(transform); }
    }
}

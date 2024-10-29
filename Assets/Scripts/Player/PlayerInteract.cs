using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float InteractDistance = 2f;
    [SerializeField] private Transform Head;

    private Interactable _interactTarget;

    public static Action<string> UpdateInteractPrompt;



    private void Update()
    {
        // Check for interactable
        if (Physics.Raycast(Head.position, Head.forward, out RaycastHit rayHit, InteractDistance))
        {
            if (rayHit.collider.TryGetComponent(out Interactable target))
            {
                _interactTarget = target;
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
        if (_interactTarget != null)
            { _interactTarget = null; }
    }

    public void OnInteract()
    {
        // Only allow interaction when the cursor is hidden and when _interactTarget is valid
        if (!Cursor.visible && _interactTarget != null)
            { _interactTarget.Interact(); }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public PlayerData playerData;
    public Transform cameraPitch;
    public float interactDistance = 2f;

    private Interactable interactTarget;


    private void Start()
    {
        playerData.interactPrompt = "";
    }

    private void Update()
    {
        CheckForInteractable();
    }

    private void CheckForInteractable()
    {
        Ray ray = new Ray(cameraPitch.position, cameraPitch.forward);
        if (Physics.Raycast(ray, out RaycastHit rayHit, interactDistance))
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
}

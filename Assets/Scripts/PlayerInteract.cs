using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Interact")]
    // Distance player can interact with objects
    public float interactDistance = 3f;
    // If the player is looking at an interactable object this will be valid and data can be retrived from it.
    private IInteract currentInteractable;

    [Header("Attack")]
    // // Distance player can attack objects
    public float attackDistance = 3f;
    public float attackDamage = 2f;

    [Header("General")]
    // Where the raycasts will start from
    public Transform raycastOrigin;

    // Delegate and Event to update the UI interact prompt
    public delegate void UpdateInteractPrompt(string text);
    public static event UpdateInteractPrompt interactPromptUpdated;

    // Update is called once per frame
    void Update()
    {
        // Interact Input
        CheckInteraction();
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }

        // Attack input
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    // Check if the player is looking at an interactable object
    private void CheckInteraction()
    {
        // Disable currentInteractable in case the player is no longer looking at an interactable object
        // Also make sure we have a currentInteractable to disable
        // You can do this in the raycast if statment at any point where it doesn't hit an interactable object
        if (currentInteractable != null) {
            // Clear ui text element
            if (interactPromptUpdated != null)
            {
                interactPromptUpdated.Invoke("");
            }
            currentInteractable = null;
        }
        
        // Raycast variables
        RaycastHit rayHit;
        Ray ray = new Ray(raycastOrigin.position, raycastOrigin.transform.forward);

        // If the raycast collides with anything within interactionDistance
        if (Physics.Raycast(ray, out rayHit, interactDistance)) {
            // Check if collider has the Iinteractable interface
            if (rayHit.collider.TryGetComponent(out IInteract interactableObject)) {
                currentInteractable = interactableObject;
                // Update ui element to match interaction prompt
                if (interactPromptUpdated != null)
                {
                    interactPromptUpdated.Invoke(interactableObject.interactPrompt);
                }
            } // Here and bellow you can add an else statment to disable currentInteractable
        }
    }
    private void Attack()
    {
        Debug.Log("Pow");
       // Raycast variables
        RaycastHit rayHit;
        Ray ray = new Ray(raycastOrigin.position, raycastOrigin.transform.forward);
        
        // Check if raycast hits
        if (Physics.Raycast(ray, out rayHit, attackDistance))
        {
            // Check if hit collider has ITakeDamage interface
            if (rayHit.collider.TryGetComponent(out ITakeDamage damagObject))
            {
                damagObject.TakeDamage(attackDamage);
            }
        }
    }
}

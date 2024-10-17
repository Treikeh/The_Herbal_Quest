using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    // Distance player can interact with objects
    public float interactDistance = 3f;
    // If the player is looking at an interactable object this will be valid and data can be retrived from it.
    private Interactable currentInteractable;

    // // Distance player can attack objects
    public float attackDistance = 3f;
    // Mask used by burnable objects
    public LayerMask attackMask;

    // Where the raycasts will start from
    public Transform raycastOrigin;

    // Update is called once per frame
    void Update()
    {
        // Interact Input
        CheckInteraction();
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null) {
            currentInteractable.Interact();
        }

        // Attack input
        if (Input.GetMouseButtonDown(0)) {
            Attack();
        }
    }

    // Check if the player is looking at an interactable object
    private void CheckInteraction() {
        // Disable currentInteractable in case the player is no longer looking at an interactable object
        // Also make sure we have a currentInteractable to disable
        // You can do this in the raycast if statment at any where it doesn't hit an Interactable object
        if (currentInteractable) {
            currentInteractable = null;
        }
        
        // Raycast variables
        RaycastHit rayHit;
        Ray ray = new Ray(raycastOrigin.position, raycastOrigin.transform.forward);

        // If the raycast collides with anything within interactionDistance
        if (Physics.Raycast(ray, out rayHit, interactDistance)) {
            // If hit collider has interactable component
            if (rayHit.collider.tag == "Interactable") {
                Interactable new_interactable = rayHit.collider.GetComponent<Interactable>();
                // Check if the interactable component is enabled
                if (new_interactable.enabled) {
                    currentInteractable = new_interactable;
                }
            } // Here, above and bellow you can add an else statment to disable currentInteractable
        }
    }

        private void Attack() {
        RaycastHit rayHit;
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.transform.forward, out rayHit, attackDistance, attackMask)) {
            Destroy(rayHit.collider.gameObject);
        }
    }
}

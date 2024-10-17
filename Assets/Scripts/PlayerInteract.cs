using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    // Distance player can interact with objects
    public float interactDistance = 3f;
    // Mask used by interactable objects
    public LayerMask interactMask;
    // // Distance player can attack objects
    public float attackDistance = 3f;
    // Mask used by burnable objects
    public LayerMask attackMask;
    // Where the raycasts will start from
    public Transform raycastOrigin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Hello
        // Interact Input
        if (Input.GetKeyDown(KeyCode.E)) {
            Interact();
        }

        // Attack input
        if (Input.GetMouseButtonDown(0)) {
            Attack();
        }
    }

    private void Interact() {
        RaycastHit rayHit;
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.transform.forward, out rayHit, interactDistance, interactMask)) {
            Debug.Log(rayHit.collider.name);
        }
    }

        private void Attack() {
        RaycastHit rayHit;
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.transform.forward, out rayHit, attackDistance, attackMask)) {
            Destroy(rayHit.collider.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput1 : MonoBehaviour
{
    private PlayerMovement pMovement;
    private PlayerCamera pCamera;
    private PlayerInteract pInteract;
    private PlayerAttck pAttack;

    private void Start()
    {
        pMovement = GetComponent<PlayerMovement>();
        pCamera = GetComponent<PlayerCamera>();
        pInteract = GetComponent<PlayerInteract>();
        pAttack = GetComponent<PlayerAttck>();
    }

    private void Update()
    {
        //
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttck : MonoBehaviour
{
    public PlayerData playerData;

    [Header("Attack")]
    // // Distance player can attack objects
    [SerializeField] private float attackDistance = 3f;
    [SerializeField] private float attackDamage = 2f;

    // Subscribe to events
    private void OnEnable()
    {
        playerData.Events.OnAttacking += OnAttacking;
    }

    // Unsubscribe to events
    private void OnDisable()
    {
        playerData.Events.OnAttacking -= OnAttacking;
    }


    private void Update()
    {
        // Check if we are looking at an attackable object so that we can update the hud to display an attack icon
        //TODO
    }

    private void OnAttacking()
    {
       // Raycast variables
        RaycastHit rayHit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        
        // Check if raycast hits
        if (Physics.Raycast(ray, out rayHit, attackDistance))
        {
            // Check if hit collider has ITakeDamage interface
            if (rayHit.collider.TryGetComponent(out ITakeDamage attackTarget))
            {
                attackTarget.TakeDamage(attackDamage);
            }
        }
    }
}

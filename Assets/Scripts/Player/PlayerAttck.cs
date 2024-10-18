using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttck : MonoBehaviour
{
    [Header("Attack")]
    // // Distance player can attack objects
    [SerializeField] private float attackDistance = 3f;
    [SerializeField] private float attackDamage = 2f;

    void Update()
    {
        // Attack input
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Attack()
    {
       // Raycast variables
        RaycastHit rayHit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        
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

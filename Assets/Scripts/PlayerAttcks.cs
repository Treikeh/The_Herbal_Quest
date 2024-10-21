using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttck : MonoBehaviour
{
    [SerializeField] private float attackDistance = 3f;
    [SerializeField] private float attackDamage = 2f;

    private ITakeDamage attackTarget;

    public static event Action<bool> UpdateCrosshair;


    private void Update()
    {
        // Check if the player is looking at an attackable object
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit rayHit, attackDistance))
        {
            if (rayHit.collider.TryGetComponent(out ITakeDamage target))
            {
                attackTarget = target;
                // Update ui to display attack icon
                UpdateCrosshair?.Invoke(true);
            }
            else
            {
                ClearAttackTarget();
            }
        }
        else
        {
            ClearAttackTarget();
        }
    }

    private void ClearAttackTarget()
    {
        if (attackTarget != null)
        {
            // Update ui to display normal crosshair
            UpdateCrosshair?.Invoke(false);
            attackTarget = null;
        }
    }

    public void AttackObject()
    {
        if (attackTarget != null)
        {
            attackTarget.TakeDamage(attackDamage);
        }
        else
        {
            //
        }
    }
}

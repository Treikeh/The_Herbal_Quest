using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttck : MonoBehaviour
{
    public PlayerData playerData;

    // Sounds should be moved
    public string hitSound;
    public string missSound;

    private Health attackTarget;


    private void Update()
    {
        // Check if the player is looking at an attackable object
        Ray ray = new Ray(playerData.cameraTransform.position, playerData.cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit rayHit, playerData.interactDistance))
        {
            if (rayHit.collider.TryGetComponent(out Health target))
            {
                attackTarget = target;
                playerData.displayAttackIcon = true;
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
            attackTarget = null;
            playerData.displayAttackIcon = false;
        }
    }

    public void OnAttack()
    {
        if (attackTarget != null)
        {
            attackTarget.TakeDamage(playerData.attackDamage);
            AudioManager.Instance.PlaySound(hitSound);
        }
        else
        {
            AudioManager.Instance.PlaySound(missSound);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttck : MonoBehaviour
{
    public PlayerData playerData;
    public BoolAsset displayAttackIcon;

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
                displayAttackIcon.value = true;
            }
            else if (attackTarget != null) // Clear if rayHit collider does not have an health component
            {
                attackTarget = null;
                displayAttackIcon.value = false;
            }
        }
        else if (attackTarget != null) // Clear if raycast is not hitting anything
        {
            attackTarget = null;
            displayAttackIcon.value = false;
        }
    }

    // Called when player presses the Attack button
    public void OnAttack()
    {
        if (!playerData.canAttack)
        {
            Debug.Log("Player can't attack");
            return;
        }
        else
        {
            playerData.canAttack = false;
            StartCoroutine(AttackDelay());
            
            if (attackTarget != null)
            {
                attackTarget.TakeDamage(playerData.attackDamage);
                AudioManager.Instance.PlaySound(playerData.attackHitSound);
            }
            else
            {
                AudioManager.Instance.PlaySound(playerData.attackMissSound);
            }
        }
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(1f);
        playerData.canAttack = true;
    }
}

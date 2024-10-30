using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour
{
    [SerializeField] private bool startWithTorchActive = true;
    [SerializeField] private float attackDelay = 0.5f;
    [SerializeField] private float attackDistance = 2f;
    [SerializeField] private Transform head;
    [SerializeField] private GameObject torch;
    [SerializeField] private Light flameLight;
    [SerializeField] private Animator attackAnimations;
    [SerializeField] private AudioClip attackHitSound;
    [SerializeField] private AudioClip attackMissSound;
    [SerializeField] private AudioSource attackAudioSource;

    private bool canAttack;
    private Hitable attackTarget;

    public static Action<bool> UpdateCrosshair;

    private void Start()
    {
        if (startWithTorchActive)
        {
            torch.SetActive(true);
            canAttack = true;
        }
        else
        {
            torch.SetActive(false);
            canAttack = false;
        }
    }

    private void Update()
    {
        if (torch.activeInHierarchy)
            { CheckForAttackable(); }
    }

    // Check if the player is looking at an attackable object
    private void CheckForAttackable()
    {
        if (Physics.Raycast(head.position, head.forward, out RaycastHit rayHit, attackDistance))
        {
            if (rayHit.collider.TryGetComponent(out Hitable target))
            {
                attackTarget = target;
                UpdateCrosshair?.Invoke(true);
            }
            else
                { ClearAttackTarget(); }
        }
        else
            { ClearAttackTarget(); }
    }

    private void ClearAttackTarget()
    {
        if (attackTarget != null)
        {
            attackTarget = null;
            UpdateCrosshair?.Invoke(false);
        }
    }

    // INPUT
    public void OnAttack()
    {
        if (canAttack && !Cursor.visible)
        {
            // Disable attacking
            canAttack = false;
            // Reset attack after a short duration
            Invoke(nameof(ResetAttack), attackDelay);
            attackAnimations.Play("AttackAnim", -1, 0f);
            
            // Reactons to what the player hits
            if (attackTarget != null)
            {
                attackTarget.Hit(flameLight.enabled, transform);
                attackAudioSource.PlayOneShot(attackHitSound);
            }
            else
            {
                attackAudioSource.PlayOneShot(attackMissSound);
            }
        }
    }

    private void ResetAttack()
        { canAttack = true; }

    // GAME EVENTS //

    public void PickUpTorch()
    {
        if (!torch.activeInHierarchy)
        {
            torch.SetActive(true);
            canAttack = true;
        }
    }

    public void LightTorch()
    {
        if (torch.activeInHierarchy && !flameLight.enabled)
        {
            flameLight.enabled = true;
            // Play sound
        }
    }

    public void ExtinguishTorch()
    {
        if (torch.activeInHierarchy && flameLight.enabled)
        {
            flameLight.enabled = false;
            // Play sound
        }
    }
}

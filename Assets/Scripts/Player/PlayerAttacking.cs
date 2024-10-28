using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacking : MonoBehaviour
{
    public bool startWithTorchActive = true;
    public float attackDelay = 0.5f;
    public float attackDistance = 2f;
    
    public SharedData sharedData;
    public Transform head;
    public GameObject torch;
    public Light flameLight;
    public Animator attackAnimations;
    public AudioClip attackHitSound;
    public AudioClip attackMissSound;
    public AudioSource attackAudioSource;

    private bool canAttack;
    private BurnableObject attackTarget;


    private void Start()
    {
        sharedData.displayAttackIcon = false;
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
            if (rayHit.collider.TryGetComponent(out BurnableObject target))
            {
                attackTarget = target;
                sharedData.displayAttackIcon = true;
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
            sharedData.displayAttackIcon = false;
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
                attackTarget.Hit(flameLight.enabled);
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

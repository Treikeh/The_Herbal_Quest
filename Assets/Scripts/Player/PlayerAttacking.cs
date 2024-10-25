using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacking : MonoBehaviour
{
    public SharedData sharedData;
    public bool startWithTorchActive = true;
    public float attackSpeed = 0.5f;
    public float attackDistance = 2f;
    public Transform head;
    public GameObject torch;
    public Light flameLight;
    public Animator attackAnimations;
    public AudioClip attackHitSound;
    public AudioClip attackMissSound;
    public AudioSource attackAudioSource;

    private bool canAttack;
    private IHitable attackTarget;


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
        {
            CheckForAttackable();
        }
    }

    // Check if the player is looking at an attackable object
    private void CheckForAttackable()
    {
        Ray ray = new Ray(head.position, head.forward);
        if (Physics.Raycast(ray, out RaycastHit rayHit, attackDistance))
        {
            if (rayHit.collider.TryGetComponent(out IHitable target))
            {
                attackTarget = target;
                sharedData.displayAttackIcon = true;
            }
            else if (attackTarget != null) // Clear if rayHit collider does not have an health component
            {
                attackTarget = null;
                sharedData.displayAttackIcon = false;
            }
        }
        else if (attackTarget != null) // Clear if raycast is not hitting anything
        {
            attackTarget = null;
            sharedData.displayAttackIcon = false;
        }
    }

    public void OnAttack()
    {
        // Disable attacking when the cursor is visable
        if (Cursor.visible)
        {
            Debug.Log("Player can't attack");
            return;
        }
        else if (canAttack)
        {
            canAttack = false;
            StartCoroutine(AttackDelay());
            attackAnimations.Play("AttackAnim", -1, 0f);
            
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

// Game events //

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

// IEnumerators //

    // Delay when the player can attack again
    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacking : MonoBehaviour
{
    public PlayerData playerData;
    public Light flameLight;
    public GameObject torch;
    public Transform cameraPitch;
    public bool startWithTorch = true;
    public float rayDistance = 2f;
    public float attackSpeed = 0.5f;
    public AudioClip attackHitSound;
    public AudioClip attackMissSound;
    public AudioSource attackAudioSource;
    public Animator attackAnimations;

    private bool canAttack;
    private IHitable attackTarget;


    private void Start()
    {
        playerData.displayAttackIcon = false;
        if (startWithTorch)
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
        Ray ray = new Ray(cameraPitch.position, cameraPitch.forward);
        if (Physics.Raycast(ray, out RaycastHit rayHit, rayDistance))
        {
            if (rayHit.collider.TryGetComponent(out IHitable target))
            {
                attackTarget = target;
                playerData.displayAttackIcon = true;
            }
            else if (attackTarget != null) // Clear if rayHit collider does not have an health component
            {
                attackTarget = null;
                playerData.displayAttackIcon = false;
            }
        }
        else if (attackTarget != null) // Clear if raycast is not hitting anything
        {
            attackTarget = null;
            playerData.displayAttackIcon = false;
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
                attackTarget.Hit(playerData.torchLit);
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
            playerData.torchLit = true;
            // Play sound
        }
    }

    public void ExtinguishTorch()
    {
        if (torch.activeInHierarchy && flameLight.enabled)
        {
            flameLight.enabled = false;
            playerData.torchLit = false;
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

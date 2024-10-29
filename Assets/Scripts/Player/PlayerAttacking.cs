using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour
{
    [SerializeField] private bool StartWithTorchActive = true;
    [SerializeField] private float AttackDelay = 0.5f;
    [SerializeField] private float AttackDistance = 2f;
    [SerializeField] private Transform Head;
    [SerializeField] private GameObject Torch;
    [SerializeField] private Light FlameLight;
    [SerializeField] private Animator AttackAnimations;
    [SerializeField] private AudioClip AttackHitSound;
    [SerializeField] private AudioClip AttackMissSound;
    [SerializeField] private AudioSource AttackAudioSource;

    private bool _canAttack;
    private BurnableObject _attackTarget;

    public static Action<bool> UpdateCrosshair;

    private void Start()
    {
        if (StartWithTorchActive)
        {
            Torch.SetActive(true);
            _canAttack = true;
        }
        else
        {
            Torch.SetActive(false);
            _canAttack = false;
        }
    }

    private void Update()
    {
        if (Torch.activeInHierarchy)
            { CheckForAttackable(); }
    }

    // Check if the player is looking at an attackable object
    private void CheckForAttackable()
    {
        if (Physics.Raycast(Head.position, Head.forward, out RaycastHit rayHit, AttackDistance))
        {
            if (rayHit.collider.TryGetComponent(out BurnableObject target))
            {
                _attackTarget = target;
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
        if (_attackTarget != null)
        {
            _attackTarget = null;
            UpdateCrosshair?.Invoke(false);
        }
    }

    // INPUT
    public void OnAttack()
    {
        if (_canAttack && !Cursor.visible)
        {
            // Disable attacking
            _canAttack = false;
            // Reset attack after a short duration
            Invoke(nameof(ResetAttack), AttackDelay);
            AttackAnimations.Play("AttackAnim", -1, 0f);
            
            // Reactons to what the player hits
            if (_attackTarget != null)
            {
                _attackTarget.Hit(FlameLight.enabled);
                AttackAudioSource.PlayOneShot(AttackHitSound);
            }
            else
            {
                AttackAudioSource.PlayOneShot(AttackMissSound);
            }
        }
    }

    private void ResetAttack()
        { _canAttack = true; }

    // GAME EVENTS //

    public void PickUpTorch()
    {
        if (!Torch.activeInHierarchy)
        {
            Torch.SetActive(true);
            _canAttack = true;
        }
    }

    public void LightTorch()
    {
        if (Torch.activeInHierarchy && !FlameLight.enabled)
        {
            FlameLight.enabled = true;
            // Play sound
        }
    }

    public void ExtinguishTorch()
    {
        if (Torch.activeInHierarchy && FlameLight.enabled)
        {
            FlameLight.enabled = false;
            // Play sound
        }
    }
}

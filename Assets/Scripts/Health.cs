using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 10f;
    private float currentHealth;

    public UnityEvent<float, float> OnHit;
    public UnityEvent OnKilled;

    private void Start()
    {
        // Start object at max health
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        OnHit.Invoke(currentHealth, maxHealth);
        // Check if ded
        if (currentHealth <= 0.0f)
        {
            OnKilled.Invoke();
        }
    }
}

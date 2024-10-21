using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, ITakeDamage
{
    // The maximum amount of health this object can have
    [SerializeField] private float maxHealth = 10f;
    // How much health this object has
    private float currentHealth;

    // Send an event 
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
        if (currentHealth <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}

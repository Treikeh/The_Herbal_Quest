using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, ITakeDamage
{
    // The maximum amount of health this object can have
    public float maxHealth = 10f;
    // How much health this object has
    private float currentHealth;

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

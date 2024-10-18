using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, ITakeDamage
{
    public float maxHealth = 100f;
    private float currentHealth;
    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log(currentHealth);
        if (currentHealth <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}

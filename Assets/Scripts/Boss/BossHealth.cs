using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The BossHealth class manages the health and behavior of the boss enemy.
public class BossHealth : MonoBehaviour
{
    public int MaxHealth = 500;
    public int currentHealth;

    // Reference to the BossHealthbar script attached to the Healthbar game object.
    public BossHealthbar Healthbar;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseHealth();
    }

    // Initialize the boss's health at the start of the boss scene.
    public void InitialiseHealth()
    {
        currentHealth = MaxHealth;
        Healthbar.SetMaxHealth(MaxHealth);

    }

    // Inflict damage on the boss based on the provided damage value.
    public void TakeDamage(int damage)
    {
        // Decrease the boss's current health by the damage value.
        currentHealth -= damage;

        // Update the boss's health bar with the current health value.
        Healthbar.SetHealth(currentHealth);

        // Check if the boss's health has reached or fallen below zero.
        if (currentHealth <= 0)
        {
            Die();
        }

    }

    // Destroy boss when defeated
    public void Die()
    {
        GameManager.Instance.isLevelOver = true;
        Destroy(gameObject);
    }

}

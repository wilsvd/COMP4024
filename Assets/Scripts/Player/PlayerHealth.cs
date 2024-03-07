using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The PlayerHealth class manages the health of the player character.
public class PlayerHealth : MonoBehaviour
{
    public int MaxHealth = 125;
    public int currentHealth;

    // Reference to the PlayerHealthbar script for updating the health UI.
    public PlayerHealthbar Healthbar;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseHealth();
    }

    // Initialize the player's health to the maximum and update the health UI.
    public void InitialiseHealth()
    {
        currentHealth = MaxHealth;
        Healthbar.SetMaxHealth(MaxHealth);
    }

    // Inflict damage to the player, update the health UI, and check for death.
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Healthbar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Handle the player's death by resetting the level.
    public void Die()
    {
        GameManager.Instance.ResetLevel();
    }

}

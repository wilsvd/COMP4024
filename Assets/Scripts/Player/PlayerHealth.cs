using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHealth = 125;
    public int currentHealth;

    public PlayerHealthbar Healthbar;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseHealth();
    }

    public void InitialiseHealth()
    {
        currentHealth = MaxHealth;
        Healthbar.SetMaxHealth(MaxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Healthbar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();

        }
    }

    public void Die()
    {
        GameManager.Instance.ResetLevel();
    }

}

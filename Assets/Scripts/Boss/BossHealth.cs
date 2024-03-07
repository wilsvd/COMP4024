using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int MaxHealth = 500;
    public int currentHealth;

    public BossHealthbar Healthbar;

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
        /* 
         * TODO: Create some nice death affect
         */
        GameManager.Instance.isLevelOver = true;
        Destroy(gameObject);
    }

}

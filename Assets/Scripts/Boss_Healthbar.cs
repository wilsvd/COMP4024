using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Healthbar : MonoBehaviour
{
    public int MaxHealth = 500;
    public int currentHealth;

    public Slider_bossHP Healthbar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        Healthbar.SetMaxHealth(MaxHealth);

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Healthbar.SetHealth(currentHealth);

    }

}

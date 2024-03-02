using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heath_bar : MonoBehaviour
{
    public int MaxHealth = 125;
    public int currentHealth;

    public sliderscript Healthbar;

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

        if (currentHealth <= 0) {
            /*TODO:
             * Kill Player
             * Restart Game
             */
            return;
        }


    }

}

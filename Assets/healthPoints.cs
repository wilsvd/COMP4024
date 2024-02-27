using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heath_bar : MonoBehaviour
{
    public int MaxHealth = 100;
    public int currentHealth;

    public sliderscript Healthbar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        Healthbar.SetMaxHealth(MaxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Healthbar.SetHealth(currentHealth);

    }

}

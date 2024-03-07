using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// The PlayerHealthbar class manages the health bar UI for the player character.
public class PlayerHealthbar : MonoBehaviour
{
    // Reference to the Slider component representing the health bar.
    public Slider slider;

    // Set the maximum health value for the health bar.
    public void SetMaxHealth(int health)
    {  
        slider.maxValue = health;
        slider.value = health;
    
    }

    // Update the health bar with the current health value.
    public void SetHealth(int health)
    {  
        slider.value = health;
    }
    
}

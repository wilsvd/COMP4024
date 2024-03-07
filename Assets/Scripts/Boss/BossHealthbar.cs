using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// The BossHealthbar class manages the visual representation of the boss's health using a UI slider.
public class BossHealthbar : MonoBehaviour
{
    // Reference to the Slider component for visualizing health.
    public Slider slider;

    // Set the maximum health value for the health bar slider.
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

    }

    // Update the health value on the health bar slider.
    public void SetHealth(int health)
    {
        slider.value = health;
    }

}

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HPtesting
{
    [UnityTest]
    public IEnumerator SetMaxHealth_SetsMaxHealthCorrectly()
    {
        GameObject gameObject = new();

        sliderscript sliderScript = gameObject.AddComponent<sliderscript>();
        sliderScript.slider = gameObject.AddComponent<Slider>();

        sliderScript.SetMaxHealth(125);

        Assert.AreEqual(125, sliderScript.slider.maxValue, "Max health not set correctly");
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

    [UnityTest]
    public IEnumerator TakeDamage_ReducesCurrentHealth()
    {
        GameObject gameObject = new();

        healthPoints playerScript = gameObject.AddComponent<healthPoints>();
        sliderscript healthBarScript = gameObject.AddComponent<sliderscript>();
        healthBarScript.slider = gameObject.AddComponent<Slider>();


        playerScript.Healthbar = healthBarScript;

        playerScript.MaxHealth = 125;
        playerScript.currentHealth = 100;

        playerScript.TakeDamage(20); // Taking 20 damage

        Assert.AreEqual(80, playerScript.currentHealth, "Current health not reduced correctly");


        yield return null;
    }

    [UnityTest]
    public IEnumerator InitialiseHealth_Start()
    {
        GameObject gameObject = new();

        healthPoints playerScript = gameObject.AddComponent<healthPoints>();
        sliderscript healthBarScript = gameObject.AddComponent<sliderscript>();
        healthBarScript.slider = gameObject.AddComponent<Slider>();
        playerScript.Healthbar = healthBarScript;

        playerScript.InitialiseHealth();

        // Assert that the initial health values are set correctly
        Assert.AreEqual(125, playerScript.MaxHealth, "Max health not initialized correctly");
        Assert.AreEqual(125, playerScript.currentHealth, "Current health not initialized correctly");

        yield return null;
    }
}
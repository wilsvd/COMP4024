using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HPbossTesting
{
    private GameObject gameObject;

    [SetUp]
    public void SetUp()
    {
        // Create a new GameObject for each test
        gameObject = new GameObject();
    }

    [TearDown]
    public void TearDown()
    {
        // Destroy the test GameObject after each test
        Object.Destroy(gameObject);
    }

    [UnityTest]
    public IEnumerator SetMaxHealth_SetsMaxHealthCorrectly()
    {
        //GameObject gameObject = new();
        Slider_bossHP sliderScript = gameObject.AddComponent<Slider_bossHP>();
        sliderScript.slider = gameObject.AddComponent<Slider>();

        sliderScript.SetMaxHealth(500);

        Assert.AreEqual(500, sliderScript.slider.maxValue, "Max health not set correctly");
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

    }

    [UnityTest]
    public IEnumerator TakeDamage_ReducesCurrentHealth()
    {
        //GameObject gameObject = new();

        Boss_Healthbar playerScript = gameObject.AddComponent<Boss_Healthbar>();
        Slider_bossHP healthBarScript = gameObject.AddComponent<Slider_bossHP>();
        healthBarScript.slider = gameObject.AddComponent<Slider>();


        playerScript.Healthbar = healthBarScript;

        playerScript.MaxHealth = 500;
        playerScript.currentHealth = 100;

        playerScript.TakeDamage(20); // Taking 20 damage

        Assert.AreEqual(80, playerScript.currentHealth, "Current health not reduced correctly");


        yield return null;
    }
    [UnityTest]
    public IEnumerator InitialiseHealth_Start()
    {
        //GameObject gameObject = new();

        Boss_Healthbar playerScript = gameObject.AddComponent<Boss_Healthbar>();
        Slider_bossHP healthBarScript = gameObject.AddComponent<Slider_bossHP>();
        healthBarScript.slider = gameObject.AddComponent<Slider>();
        playerScript.Healthbar = healthBarScript;

        playerScript.InitialiseHealth();

        // Assert that the initial health values are set correctly
        Assert.AreEqual(500, playerScript.MaxHealth, "Max health not initialized correctly");
        Assert.AreEqual(500, playerScript.currentHealth, "Current health not initialized correctly");

        yield return null;
    }

}
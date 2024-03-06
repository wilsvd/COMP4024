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
        Boss_Healthbar bossScript = gameObject.AddComponent<Boss_Healthbar>();
        Slider_bossHP healthBarScript = gameObject.AddComponent<Slider_bossHP>();
        healthBarScript.slider = gameObject.AddComponent<Slider>();


        bossScript.Healthbar = healthBarScript;

        bossScript.MaxHealth = 500;
        bossScript.currentHealth = 100;

        bossScript.TakeDamage(20); // Taking 20 damage

        Assert.AreEqual(80, bossScript.currentHealth, "Current health not reduced correctly");


        yield return null;
    }
    [UnityTest]
    public IEnumerator InitialiseHealth_Start()
    {
        Boss_Healthbar bossScript = gameObject.AddComponent<Boss_Healthbar>();
        Slider_bossHP healthBarScript = gameObject.AddComponent<Slider_bossHP>();
        healthBarScript.slider = gameObject.AddComponent<Slider>();
        bossScript.Healthbar = healthBarScript;

        bossScript.InitialiseHealth();

        // Assert that the initial health values are set correctly
        Assert.AreEqual(500, bossScript.MaxHealth, "Max health not initialized correctly");
        Assert.AreEqual(500, bossScript.currentHealth, "Current health not initialized correctly");

        yield return null;
    }
    [UnityTest]
    public IEnumerator BossDie()
    {
        Boss_Healthbar bossScript = gameObject.AddComponent<Boss_Healthbar>();
        Slider_bossHP healthBarScript = gameObject.AddComponent<Slider_bossHP>();
        healthBarScript.slider = gameObject.AddComponent<Slider>();
        bossScript.Healthbar = healthBarScript;

        bossScript.MaxHealth = 500;
        bossScript.currentHealth = 0;

        // Trigger the boss die action
        bossScript.Die();

        // Assert that the boss's health is set to zero
        Assert.AreEqual(0, bossScript.currentHealth, "Current health not set to zero when boss dies");

        yield return null;
    }
}
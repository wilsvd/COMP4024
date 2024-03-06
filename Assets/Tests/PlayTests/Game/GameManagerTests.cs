using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class GameManagerTests
{
    [UnityTest]
    public IEnumerator LoadLevel_LoadsCorrectScene()
    {
        GameManager gameManager = new GameObject("GameManager").AddComponent<GameManager>();

        yield return null; // Wait for a frame to let the GameManager initialize

        gameManager.LoadLevel(GameManager.Level.One);

        yield return new WaitForSeconds(1f); // Wait for scene to load

        Scene currentScene = SceneManager.GetActiveScene();
        Assert.AreEqual(GameManager.LevelOne, currentScene.name);
    }

    [UnityTest]
    public IEnumerator LoadNextLevel_LoadsCorrectScene()
    {
        GameManager gameManager = new GameObject("GameManager").AddComponent<GameManager>();
        gameManager.LoadLevel(GameManager.Level.Nav); // Player gets created in Nav screen (preventing an error when we load the Boss Screen)


        yield return new WaitForSeconds(1f); // Wait for scene to load

        gameManager.LoadLevel(GameManager.Level.One);

        yield return new WaitForSeconds(1f); // Wait for scene to load

        gameManager.LoadNextLevel();

        yield return new WaitForSeconds(1f); // Wait for scene to load

        Scene currentScene = SceneManager.GetActiveScene();
        Assert.AreEqual(GameManager.BossLevel, currentScene.name);
    }

    [UnityTest]
    public IEnumerator ResetNextLevel_LoadsCorrectScene()
    {
        GameManager gameManager = new GameObject("GameManager").AddComponent<GameManager>();
        gameManager.LoadLevel(GameManager.Level.Nav); // Player gets created in Nav screen (preventing an error when we load the Boss Screen)
        yield return new WaitForSeconds(1f); // Wait for scene to load
        Scene currentScene = SceneManager.GetActiveScene();
        Assert.AreEqual(GameManager.NavLevel, currentScene.name);

        gameManager.LoadLevel(GameManager.Level.One);
        yield return new WaitForSeconds(1f); // Wait for scene to load
        currentScene = SceneManager.GetActiveScene();
        Assert.AreEqual(GameManager.LevelOne, currentScene.name);

        gameManager.LoadNextLevel(); // Navigate to the Boss Room of level 1
        yield return new WaitForSeconds(1f); // Wait for scene to load
        currentScene = SceneManager.GetActiveScene();
        Assert.AreEqual(GameManager.BossLevel, currentScene.name);

        gameManager.ResetLevel(); // Reset at the beginning of level 1
        yield return new WaitForSeconds(1f); // Wait for scene to load
        currentScene = SceneManager.GetActiveScene();
        Assert.AreEqual(GameManager.LevelOne, currentScene.name);
    }

    [UnityTest]
    public IEnumerator UpdateTimer_CountdownCorrectly()
    {
        GameManager gameManager = new GameObject("GameManager").AddComponent<GameManager>();
        yield return null; // Wait for a frame to let the GameManager initialize

        // Create a new Text component and assign it to countdownText
        gameManager.countdownText = new GameObject("CountdownText").AddComponent<Text>();

        gameManager.countdownTime = 2f; // Set a short initial countdown time for testing

        while (gameManager.countdownTime > 0)
        {
            gameManager.UpdateTimer(); // Update the timer in the GameManager
            yield return null;
        }

        // Expect timer after counting down to be 0
        string expectedText = string.Format("{0:00}:{1:00}", 0, 0);

        Assert.AreEqual(expectedText, gameManager.countdownText.text);
    }


}

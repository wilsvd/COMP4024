using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class GameManagerTests
{
    // Hacky workaround to getting tests to run sequentially
    public class LoadLevelTests
    {
        [UnityTest]
        public IEnumerator LoadLevel_LoadsLevelOne()
        {
            SceneManager.LoadScene(GameManager.NavLevel);
            yield return new WaitForSeconds(1f);

            Scene currentScene = SceneManager.GetActiveScene();
            Assert.AreEqual(GameManager.NavLevel, currentScene.name);
            // Game Manager is instatiated in Nav and is a Singleton which should persist across all scenes
            GameManager.Instance.LoadLevel(GameManager.Level.One);
            yield return new WaitForSeconds(2f); // Wait for scene to load
            currentScene = SceneManager.GetActiveScene();
            Assert.AreEqual(GameManager.LevelOne, currentScene.name);
        }
    }

    public class LoadNextLevelTests
    {
        [UnityTest]
        public IEnumerator LoadNextLevel_LoadsCorrectScene()
        {
            SceneManager.LoadScene(GameManager.NavLevel);
            yield return new WaitForSeconds(1f);

            Scene currentScene = SceneManager.GetActiveScene();
            Assert.AreEqual(GameManager.NavLevel, currentScene.name);
            // Game Manager is instatiated in Nav and is a Singleton which should persist across all scenes
            GameManager.Instance.LoadLevel(GameManager.Level.One);
            yield return new WaitForSeconds(1f); // Wait for scene to load
            currentScene = SceneManager.GetActiveScene();
            Assert.AreEqual(GameManager.LevelOne, currentScene.name);


            GameManager.Instance.LoadNextLevel();

            yield return new WaitForSeconds(1f); // Wait for scene to load
            currentScene = SceneManager.GetActiveScene();
            Assert.AreEqual(GameManager.BossLevel, currentScene.name);
        }
    }
    

    public class ResetNextLevel
    {
        [UnityTest]
        public IEnumerator ResetNextLevel_LoadsCorrectScene()
        {
            SceneManager.LoadScene(GameManager.NavLevel);
            yield return new WaitForSeconds(1f);

            Scene currentScene = SceneManager.GetActiveScene();
            Assert.AreEqual(GameManager.NavLevel, currentScene.name);

            GameManager.Instance.LoadLevel(GameManager.Level.One);
            yield return new WaitForSeconds(1f); // Wait for scene to load
            currentScene = SceneManager.GetActiveScene();
            Assert.AreEqual(GameManager.LevelOne, currentScene.name);

            GameManager.Instance.LoadNextLevel(); // Navigate to the Boss Room of level 1
            yield return new WaitForSeconds(1f); // Wait for scene to load
            currentScene = SceneManager.GetActiveScene();
            Assert.AreEqual(GameManager.BossLevel, currentScene.name);

            GameManager.Instance.ResetLevel(); // Reset at the beginning of level 1
            yield return new WaitForSeconds(1f); // Wait for scene to load
            currentScene = SceneManager.GetActiveScene();
            Assert.AreEqual(GameManager.LevelOne, currentScene.name);
        }
    }

    public class GameTimerTests
    {
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

            Object.Destroy(gameManager.countdownText);
            Object.Destroy(gameManager);
        }

    }

}

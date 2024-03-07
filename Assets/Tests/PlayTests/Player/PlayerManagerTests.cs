using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManagerTests
{
    [UnityTest]
    public IEnumerator PlayerManager_ResetPlayer()
    {
        // Setup initial scenes to contain relevant game objects
        GameManager gameManager = new GameObject("GameManager").AddComponent<GameManager>();
        gameManager.LoadLevel(GameManager.Level.Nav);
        yield return new WaitForSeconds(1f);
        gameManager.LoadLevel(GameManager.Level.One);
        yield return new WaitForSeconds(1f);


        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerManager playerManager = player.GetComponent<PlayerManager>();
        GameObject originalSpawn = GameObject.Find("SpawnPoint");

        playerManager.ResetPlayer(false);
        // Yield to next frame to allow for asynchronous operations (like Destroy)
        yield return null;


        PlayerHealth health = player.GetComponent<PlayerHealth>();
        PlayerInventory inventory = player.transform.GetChild(0).GetComponent<PlayerInventory>();
        // Assert
        Assert.NotNull(health);
        Assert.NotNull(inventory);

        // Float numbers can't be compared exactly so take into account some variance
        float epsilon = 0.1f;
        Assert.IsTrue(Mathf.Abs(originalSpawn.transform.position.x - player.transform.position.x) < epsilon);
        Assert.IsTrue(Mathf.Abs(originalSpawn.transform.position.y - player.transform.position.y) < epsilon);
        Assert.IsTrue(Mathf.Abs(originalSpawn.transform.position.z - player.transform.position.z) < epsilon);


        Assert.AreEqual(125, health.currentHealth);
        Assert.AreEqual("Fists", inventory.slots[0].item.name);
        Assert.AreEqual("Fists", inventory.slots[1].item.name);
    }

}


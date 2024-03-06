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
        Player playerManager = player.GetComponent<Player>();
        GameObject originalSpawn = GameObject.Find("SpawnPoint");

        playerManager.ResetPlayer(false);
        // Yield to next frame to allow for asynchronous operations (like Destroy)
        yield return null;


        PlayerController playerController = player.GetComponent<PlayerController>();
        healthPoints health = player.GetComponent<healthPoints>();
        Inventory inventory = player.transform.GetChild(0).GetComponent<Inventory>();
        // Assert
        Assert.NotNull(playerController);
        Assert.NotNull(health);
        Assert.NotNull(inventory);

        Assert.AreEqual(originalSpawn.transform.position, player.transform.position);
        Assert.AreEqual(125, health.currentHealth);
        Assert.AreEqual("Fists", inventory.slots[0].item.name);
        Assert.AreEqual("Fists", inventory.slots[1].item.name);
    }

}


using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerMovementTests
{
    private Vector3 originalGravity;
    private PlayerMovement playerMovement;
    private float deltaTime;

    enum Movement
    {
        Right,
        Left,
        Up,
        Down
    }

    [SetUp]
    public void SetUp()
    {
        // This method will be executed before each test method

        // Example: Create an instance of the class or initialize resources
        originalGravity = Physics.gravity;
        Physics.gravity = Vector3.zero;
        deltaTime = Time.deltaTime;

        GameObject go = new GameObject();
        playerMovement = go.AddComponent<PlayerMovement>();
        playerMovement.transform.position = new Vector3(0, 0, 0);

    }

    [TearDown]
    public void TearDown()
    {
        Physics.gravity = originalGravity;
        GameObject.Destroy(playerMovement.gameObject);
    }

    [UnityTest]
    public IEnumerator PlayerMovesRight()
    {
        playerMovement.MovePlayer((float)Movement.Right, 0, deltaTime);


        // Wait for one frame to allow movement to take effect
        yield return null;

        Assert.AreEqual(
            new Vector3(
                playerMovement.speed.x * (float)Movement.Right * deltaTime,
                0,
                0),
            playerMovement.transform.position);

        Physics.gravity = originalGravity;
    }

    [UnityTest]
    public IEnumerator PlayerMovesLeft()
    {
        playerMovement.MovePlayer((float)Movement.Left, 0, deltaTime);

        // Wait for one frame to allow movement to take effect
        yield return null;

        Assert.AreEqual(
            new Vector3(
                playerMovement.speed.x * (float)Movement.Left * deltaTime,
                0,
                0),
            playerMovement.transform.position);

        Physics.gravity = originalGravity;
    }


    [UnityTest]
    public IEnumerator PlayerMovesUp()
    {
        playerMovement.MovePlayer(0, (float)Movement.Up, deltaTime);

        // Wait for one frame to allow movement to take effect
        yield return null;

        Assert.AreEqual(
            new Vector3(
                0,
                playerMovement.speed.y * (float)Movement.Up * deltaTime,
                0),
            playerMovement.transform.position);

        Physics.gravity = originalGravity;
    }

    [UnityTest]
    public IEnumerator PlayerMovesDown()
    {
        playerMovement.MovePlayer(0, (float)Movement.Down, deltaTime);

        // Wait for one frame to allow movement to take effect
        yield return null;

        Assert.AreEqual(
            new Vector3(
                0,
                playerMovement.speed.y * (float)Movement.Down * deltaTime,
                0),
            playerMovement.transform.position);

        Physics.gravity = originalGravity;
    }
}

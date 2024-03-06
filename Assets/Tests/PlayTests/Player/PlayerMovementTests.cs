    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;

public class PlayerMovementTests
{
    private PlayerMovement playerMovement;
    private float deltaTime;

    [SetUp]
    public void SetUp()
    {
        
        Physics.gravity = Vector3.zero;
        deltaTime = Time.deltaTime;

        GameObject go = new GameObject();
        playerMovement = go.AddComponent<PlayerMovement>();
        Rigidbody2D rb = go.AddComponent<Rigidbody2D>();
        playerMovement.rb = rb;
        playerMovement.rb.velocity = new Vector2(0, 0);
        playerMovement.rb.position = new Vector2(0, 0);
        playerMovement.rb.gravityScale = 0f;
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.Destroy(playerMovement.gameObject);
    }

    [UnityTest]
    public IEnumerator PlayerMovesRight()
    {
        playerMovement.MovePlayer(1f, 0f, false,deltaTime);
        Assert.AreEqual(new Vector2(playerMovement.moveSpeed, 0f), playerMovement.rb.velocity);
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerMovesLeft()
    {
        playerMovement.MovePlayer(-1f, 0f, false, deltaTime);
        Assert.AreEqual(new Vector2(-playerMovement.moveSpeed, 0f), playerMovement.rb.velocity);
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerMovesUp()
    {
        playerMovement.MovePlayer(0f, 1f, true, deltaTime);
        Assert.AreEqual(new Vector2(0f, playerMovement.jumpForce), playerMovement.rb.velocity);
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerMovesDown()
    {
        Vector2 originalVelocity = playerMovement.rb.velocity;
        playerMovement.MovePlayer(0f, -1f, false, deltaTime);
        Vector2 expectedDown = originalVelocity + Vector2.up * Physics2D.gravity.y * (playerMovement.fallMultiplier - 1) * deltaTime;
        Assert.AreEqual(expectedDown, playerMovement.rb.velocity);
        yield return null;
    }

}

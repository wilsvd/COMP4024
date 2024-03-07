using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Boss class manages the behavior and attributes of the boss enemy.
public class Boss : MonoBehaviour
{
    int health = 200;

    // Reference to the BossWeapon script attached to the BossWeapon game object.
    private BossWeapon sword;

    // Flag to determine if the boss is flipped horizontally.
    public bool isFlipped = false;

    // Start is called before the first frame update
    void Start()
    {
        sword = transform.Find("BossWeapon").transform.GetChild(0).GetComponent<BossWeapon>();
    }

    // Initiates the boss's attack by calling the Attack method of the BossWeapon.
    public void PerformAttack()
    {
        sword.Attack();
    }

    // Inflict damage on the boss based on the provided damage value.
    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    // Rotate the boss to face the player's direction.
    public void LookAtPlayer(Transform player)
    {
        // Create a flipped vector for scaling the boss's local scale.
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        // Check if the boss is not facing the player
        if (transform.position.x > player.position.x && isFlipped)
        {
            Flip(flipped);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            Flip(flipped);
            isFlipped = true;
        }
    }

    // Flip the boss by changing its local scale and rotating it.
    private void Flip(Vector3  flipped)
    {
        transform.localScale = flipped;
        transform.Rotate(0f, 180f, 0f);
    }
}

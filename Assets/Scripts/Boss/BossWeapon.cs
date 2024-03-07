using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The BossWeapon class defines the behavior and attributes of the boss's weapon.
public class BossWeapon : MonoBehaviour
{
    public int attackDamage = 40;

    // Flag to control whether the weapon can currently attack.
    private bool canAttack = true;

    // Initiates the boss's weapon attack by enabling its collider.
    public void Attack()
    {
        // Enable the BoxCollider2D as a trigger to register collisions during the attack.
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the weapon has collided with an object tagged as "Player" and can currently attack.
        if (collision.CompareTag("Player") && canAttack)
        {
            // Inflict damage on the player using the attackDamage value.
            collision.GetComponent<PlayerHealth>().TakeDamage(attackDamage);

            // Prevent further attacks until the player exits the weapon's collider.
            canAttack = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the player has exited the collider of the boss's weapon.
        if (collision.CompareTag("Player"))
        {
            // Reset canAttack only if the player is the one exiting the weapon's collider.
            canAttack = true;
        }
    }
}

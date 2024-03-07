using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Sword class defines the behavior and functionality of the player's sword.
public class Sword : MonoBehaviour
{
    public int attackDamage = 30;

    // Flag to control whether the sword can currently attack.
    private bool canAttack = true;

    // Initiates the sword attack by enabling its collider and triggering the attack animation.
    public void Attack()
    {
        // Enable the BoxCollider2D as a trigger to register collisions during the attack.
        GetComponent<BoxCollider2D>().isTrigger = true;

        // Trigger the "SwordAttack" animation in the parent game object.
        transform.parent.gameObject.GetComponent<Animator>().SetTrigger("SwordAttack");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the sword has collided with an object tagged as "Boss" and can currently attack.
        if (collision.CompareTag("Boss") && canAttack)
        {
            Debug.Log("Player takes damage");
            // Inflict damage on the boss using the attackDamage value.
            collision.GetComponent<BossHealth>().TakeDamage(attackDamage);
            // Prevent further attacks until the sword exits the boss's collider.
            canAttack = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the sword has exited the collider of an object tagged as "Boss."
        if (collision.CompareTag("Boss"))
        {
            // Allow the sword to attack again once it exits the boss's collider.
            canAttack = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public int attackDamage = 20;
    private bool canAttack = true;
    public void Attack()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canAttack)
        {
            Debug.Log("Player takes damage");
            collision.GetComponent<heath_bar>().TakeDamage(attackDamage);
            canAttack = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Reset canAttack only if the player is the one exiting
            canAttack = true;
        }
    }
}

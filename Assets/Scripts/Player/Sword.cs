using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int attackDamage = 30;
    private bool canAttack = true;

    public void Attack()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        transform.parent.gameObject.GetComponent<Animator>().SetTrigger("SwordAttack");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss") && canAttack)
        {
            Debug.Log("Player takes damage");
            collision.GetComponent<Boss_Healthbar>().TakeDamage(attackDamage);
            canAttack = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            canAttack = true;
        }
    }
}

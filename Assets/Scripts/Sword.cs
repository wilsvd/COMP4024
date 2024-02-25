using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    

    public void Attack()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        transform.parent.gameObject.GetComponent<Animator>().SetTrigger("SwordAttack");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            Debug.Log("Boss takes damage");
            /*TODO: Add damage logic to Boss*/
        }
    }
}

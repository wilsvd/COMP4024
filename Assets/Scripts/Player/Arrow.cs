using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float life = 3;
    public Bow bow;

    private void Awake()
    {
        Destroy(gameObject, life);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boss"))
        {
            Destroy(gameObject);
            other.GetComponent<Boss_Healthbar>().TakeDamage(bow.attackDamage);
        }
        
    }
}

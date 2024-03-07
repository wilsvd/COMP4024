using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Arrow class defines the behavior of the arrow projectile shot from the Bow.
public class Arrow : MonoBehaviour
{

    // Determines the lifespan of the arrow.
    private float life = 3;

    // Reference to the Bow script for obtaining attack damage.
    public Bow bow;

    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        // Destroy the arrow game object after a specified lifespan.
        Destroy(gameObject, life);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boss"))
        {
            Destroy(gameObject);
            // Inflict damage on the Boss using the Bow's attack damage.
            other.GetComponent<BossHealth>().TakeDamage(bow.attackDamage);
        }
        
    }
}

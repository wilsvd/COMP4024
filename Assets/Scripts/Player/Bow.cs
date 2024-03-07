using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Bow class handles the logic for shooting arrows and defining arrow properties.
public class Bow : MonoBehaviour
{
    // Reference to the arrow prefab.
    public GameObject arrow;
    public Transform arrowSpawnPoint;

    public float arrowSpeed = 10;
    public int attackDamage = 20;

    // Attack method is called to initiate the arrow attack.
    public void Attack(float dir)
    {
        GameObject newArrow = Instantiate(arrow, arrowSpawnPoint.position, arrowSpawnPoint.rotation);

        newArrow.GetComponent<Arrow>().bow = this;

        // Determine the direction of the arrow
        Vector2 direction = dir >= 0 ? transform.right : -transform.right;
        newArrow.GetComponent<Rigidbody2D>().velocity = direction * arrowSpeed;
    }

}

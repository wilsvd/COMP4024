using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject arrow;
    public Transform arrowSpawnPoint;
    public float arrowSpeed = 10;
    public int attackDamage = 20;

    public void Attack(float dir)
    {
        GameObject newArrow = Instantiate(arrow, arrowSpawnPoint.position, arrowSpawnPoint.rotation);

        newArrow.GetComponent<Arrow>().bow = this;
        Vector2 direction = dir >= 0 ? transform.right : -transform.right;
        newArrow.GetComponent<Rigidbody2D>().velocity = direction * arrowSpeed;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject arrow;
    public Transform arrowSpawnPoint;
    public float arrowSpeed = 10;

    public void Attack(float dir)
    {
        GameObject newArrow = Instantiate(arrow, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
        Vector2 direction = dir >= 0 ? transform.right : -transform.right;
        newArrow.GetComponent<Rigidbody2D>().velocity = direction * arrowSpeed;
    }

}

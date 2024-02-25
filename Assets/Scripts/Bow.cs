using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject arrow;
    public Transform arrowSpawnPoint;
    public float arrowSpeed = 10;

    public void Attack()
    {
        GameObject newArrow = Instantiate(arrow, arrowSpawnPoint.position, arrowSpawnPoint.rotation);

        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * arrowSpeed;
    }

}

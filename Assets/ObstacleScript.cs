using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object is the "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Execute your code here when the "Player" bumps into the bottom of the square
            Debug.Log("Player collided with Obstacle's bottom!");

            // Add your custom code or function calls here
        }
    }
}

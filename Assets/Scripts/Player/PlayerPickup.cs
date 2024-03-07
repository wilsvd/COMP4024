using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The PlayerPickup class handles the player's ability to pick up items.
public class PlayerPickup : MonoBehaviour
{
    internal PlayerInventory inventory;
    private void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<PlayerInventory>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Debug.Log("Enter item");
            // Collides with the hitbox of VFX game object
            // We need to pass in the parent of the VFX game object
            // Add the item to the player's inventory.
            inventory.AddItem(collision.transform.parent.gameObject);
        }
    }

}

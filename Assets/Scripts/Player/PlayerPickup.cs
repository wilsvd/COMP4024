using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            inventory.AddItem(collision.transform.parent.gameObject);
        }
    }

}

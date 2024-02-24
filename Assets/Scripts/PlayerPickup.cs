using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    internal Inventory inventory;
    private void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Debug.Log("Enter item");
            inventory.AddItem(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Debug.Log("Exit item");
        }
    }

}

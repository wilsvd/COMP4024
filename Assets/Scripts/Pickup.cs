using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private bool pickUpAllowed;
    internal Inventory inventory;

    private void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        pickUpAllowed = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (pickUpAllowed) {
            Debug.Log("Pick Up");
            PickUp(gameObject, inventory);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            pickUpAllowed = true;
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pickUpAllowed = false;
        }
    }

    internal void PickUp(GameObject item, Inventory inventory)
    {
        Debug.Log("Inventory Slots Length: " + inventory.slots.Length);
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            Debug.Log("Contains Item: " + inventory.slots[i]);
            if (inventory.slots[i] == null)
            {
                GameObject newItem = Instantiate(item, inventory.transform.position, Quaternion.identity, inventory.transform);
                newItem.SetActive(false);
                inventory.slots[i] = newItem;
                
                Destroy(item);
                break;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private bool pickUpAllowed;
    private Inventory inventory;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        pickUpAllowed = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (pickUpAllowed) {
            Debug.Log("Pick Up");
            PickUp();
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

    private void PickUp()
    {
        Debug.Log("Inventory Slots Length: " + inventory.slots.Length);
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            Debug.Log("Contains Item: " + inventory.slots[i]);
            if (inventory.slots[i] == null)
            {
                GameObject newItem = Instantiate(gameObject, inventory.transform.position, Quaternion.identity, inventory.transform);
                newItem.SetActive(false);
                inventory.slots[i] = newItem;
                
                Destroy(gameObject);
                break;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private bool pickUpAllowed;
    internal Inventory inventory;
    private Animator anim;
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
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.slots[i].name == "Fists")
            {
                GameObject newItem = Instantiate(item, inventory.transform.position, Quaternion.identity, inventory.transform);
                newItem.SetActive(false);
                GameObject.Find("Player").GetComponent<SwordAttack>().anim = newItem.GetComponent<Animator>();
                inventory.slots[i] = newItem;
                Destroy(item);
                break;
            }
        }
    }
}

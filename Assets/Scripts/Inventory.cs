using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    public GameObject[] slots;

    public GameObject equippedItem;

    private void Start()
    {
        equippedItem = slots[0];
    }

    // Update is called once per frame
    void Update()
    {
        SwitchItems(Input.GetKeyDown(KeyCode.Alpha1), 
            Input.GetKeyDown(KeyCode.Alpha2));
    }

    private void SwitchItems(bool input1, bool input2)
    {
        if (input1)
        {
            slots[0].SetActive(true);
            slots[1].SetActive(false);
            equippedItem = slots[0];
            Debug.Log(equippedItem);
        }
        else if (input2)
        {
            slots[0].SetActive(false);
            slots[1].SetActive(true);
            equippedItem = slots[1];
            Debug.Log(equippedItem);
        }
    }

    internal void AddItem(GameObject item)
    {

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].name == "Fists")
            {
                GameObject newItem = Instantiate(item, transform.position, Quaternion.identity, transform);
                newItem.SetActive(false);
                slots[i] = newItem;

                Destroy(item);
                break;
            }
        }
    }

}

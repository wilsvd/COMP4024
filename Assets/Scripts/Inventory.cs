using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    public GameObject[] slots;

    // Update is called once per frame
    void Update()
    {
        SwitchItems(Input.GetKeyDown("1"), Input.GetKeyDown("2"));
    }

    private void SwitchItems(bool input1, bool input2)
    {
        if (input1)
        {
            slots[1].SetActive(false);
            slots[0].SetActive(true);
        }
        else if (input2)
        {
            slots[0].SetActive(false);
            slots[1].SetActive(true);
        }
    }
}

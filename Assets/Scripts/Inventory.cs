using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    public GameObject[] slots;

    // Start is called before the first frame update
    void Start()
    {
        slots = new GameObject[2];
    }

    // Update is called once per frame
    void Update()
    {
        SwitchItems();
    }


    private void SwitchItems()
    {
        if (slots == null)
        {
            return;
        }

        if (slots[0] != null && Input.GetKeyDown("1"))
        {
            if (slots[1] != null)
            {
                slots[1].SetActive(false);
            }
            slots[0].SetActive(true);
        }
        else if (slots[1] != null && Input.GetKeyDown("2"))
        {
            if (slots[0] != null)
            {
                slots[0].SetActive(false);
            }
            slots[1].SetActive(true);
        }
    }
}

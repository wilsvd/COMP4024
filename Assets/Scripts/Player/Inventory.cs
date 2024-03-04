using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public enum Weapon
    {
        Fists,
        Sword,
        Bow
    }
    public struct Item
    {
        public GameObject item;
        public Weapon type;
    }

    public Item[] slots = new Item[2];

    private Dictionary<Weapon, int> weaponIndices = new Dictionary<Weapon, int>();

    public Item equippedItem = new Item();

    private bool hasSword = false;
    private bool hasBow = false;

    private void Start()
    {
        InitializeInventory();
        
    }

    private void InitializeInventory()
    {
        GameObject fists = transform.GetChild(0).gameObject;

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new Item { item = fists, type = Weapon.Fists };
        }
        UpdateEquippedItem(slots[0].item, slots[0].type);
        hasSword = false;
        hasBow = false;
        weaponIndices.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        SwitchItems(Input.GetKeyDown(KeyCode.Alpha1), Input.GetKeyDown(KeyCode.Alpha2));
    }

    private void SwitchItems(bool input1, bool input2)
    {
        if (input1 || input2)
        {
            int index = input1 ? 0 : 1;
            slots[index].item.SetActive(true);
            slots[1 - index].item.SetActive(false);
            UpdateEquippedItem(slots[index].item, slots[index].type);
        }
    }

    internal void AddItem(GameObject item)
    {
        Weapon weapon = CheckItemType(item);
        int weaponIndex = HasWeapon(weapon);

        if (weaponIndex >= 0)
        {
            ImproveWeapon(weapon, weaponIndex);
            Destroy(item);
            return;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].type == Weapon.Fists)
            {
                GameObject newItem = Instantiate(item, transform.position, Quaternion.identity, transform);
                newItem.SetActive(false);
                slots[i].item = newItem;
                slots[i].type = weapon;
                if (weapon == Weapon.Sword) { hasSword = true; }
                else if (weapon == Weapon.Bow) { hasBow = true; }

                weaponIndices[weapon] = i;

                Destroy(item);
                break;
            }
        }
    }

    private void UpdateEquippedItem(GameObject item, Weapon type)
    {
        equippedItem.item = item;
        equippedItem.type = type;
    }

    private int HasWeapon(Weapon item)
    {
        return weaponIndices.ContainsKey(item) ? weaponIndices[item] : -1;
    }

    private void ImproveWeapon(Weapon weapon, int weaponIndex)
    {
        if (weapon == Weapon.Sword && hasSword)
        {
            slots[weaponIndex].item.transform.GetChild(0).GetComponent<Sword>().attackDamage += 20;
        }
        else if (weapon == Weapon.Bow && hasBow)
        {
            slots[weaponIndex].item.GetComponent<Bow>().attackDamage += 15;
        }
    }

    private Weapon CheckItemType(GameObject item)
    {
        Transform parent = item.transform;
        if (parent.childCount > 0)
        {
            Transform child = parent.GetChild(0);
            if (child.GetComponent<Sword>() != null)
            {
                return Weapon.Sword;
            }
            else if (item.GetComponent<Bow>() != null)
            {
                return Weapon.Bow;
            }
        }
        return Weapon.Fists;
    }

    public void ResetInventory()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].type != Weapon.Fists)
            {
                Destroy(slots[i].item);
            }
        }

        InitializeInventory();
    }

}

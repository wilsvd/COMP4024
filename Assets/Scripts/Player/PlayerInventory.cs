using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The PlayerInventory class manages the player's inventory, including equipped items and weapon upgrades.
public class PlayerInventory : MonoBehaviour
{
    public enum Weapon
    {
        Fists,
        Sword,
        Bow
    }

    // Struct representing an inventory item with a GameObject and its weapon type.
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

    // Initialize the player's inventory with fists as the default item.
    internal void InitializeInventory()
    {
        GameObject fists = transform.GetChild(0).gameObject;

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new Item { item = fists, type = Weapon.Fists };
        }

        // Update the equipped item and reset flags and dictionary.
        UpdateEquippedItem(slots[0].item, slots[0].type);
        hasSword = false;
        hasBow = false;
        weaponIndices.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for input to switch between equipped items.
        SwitchItems(Input.GetKeyDown(KeyCode.Alpha1), Input.GetKeyDown(KeyCode.Alpha2));
    }

    // Switch between equipped items based on player input.
    internal void SwitchItems(bool input1, bool input2)
    {
        if (input1 || input2)
        {
            // Determine the index based on the input and activate/deactivate items accordingly.
            int index = input1 ? 0 : 1;
            slots[index].item.SetActive(true);
            slots[1 - index].item.SetActive(false);
            UpdateEquippedItem(slots[index].item, slots[index].type);
        }
    }

    // Add a new item to the inventory, upgrading if the same weapon type already exists.
    internal void AddItem(GameObject item)
    {
        // Check the weapon type of the item.
        Weapon weapon = CheckItemType(item);
        // Check if the player already has this weapon type.
        int weaponIndex = HasWeapon(weapon);
        if (weaponIndex >= 0)
        {
            // Upgrade the existing weapon and destroy the new item.
            ImproveWeapon(weapon, weaponIndex);
            Destroy(item);
            return;
        }

        // Iterate through inventory slots to find an empty slot for the new item.
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].type == Weapon.Fists)
            {
                // Instantiate a new item at the player's position and set it as inactive.
                GameObject newItem = Instantiate(item, transform.position, Quaternion.identity, transform);
                newItem.SetActive(false);
                // Update the inventory slot with the new item and its weapon type.
                slots[i].item = newItem;
                slots[i].type = weapon;
                // Update flags and dictionary for the new weapon.
                if (weapon == Weapon.Sword) { hasSword = true; }
                else if (weapon == Weapon.Bow) { hasBow = true; }

                weaponIndices[weapon] = i;
                // Destroy the item, as it is now part of the player's inventory.
                Destroy(item);
                break;
            }
        }
    }

    // Update the equipped item based on the current inventory slot.
    internal void UpdateEquippedItem(GameObject item, Weapon type)
    {
        equippedItem.item = item;
        equippedItem.type = type;
    }

    // Check if the player has a specific weapon and return its index in the inventory.
    internal int HasWeapon(Weapon item)
    {
        if (item == Weapon.Fists) return -1;
        return weaponIndices.ContainsKey(item) ? weaponIndices[item] : -1;
    }

    // Improve the damage of an existing weapon based on its type.
    internal void ImproveWeapon(Weapon weapon, int weaponIndex)
    {
        if (weapon == Weapon.Sword && hasSword)
        {
            // Increase the sword's attack damage by 20.
            slots[weaponIndex].item.transform.GetChild(0).GetComponent<Sword>().attackDamage += 20;
        }
        else if (weapon == Weapon.Bow && hasBow)
        {
            // Increase the bow's attack damage by 15.
            slots[weaponIndex].item.GetComponent<Bow>().attackDamage += 15;
        }
    }

    // Check the type of an item and return the corresponding weapon type.
    internal Weapon CheckItemType(GameObject item)
    {
        Transform parent = item.transform;
        if (parent.childCount > 0)
        {
            Transform child = parent.GetChild(0);
            
            if (child.GetComponent<Sword>() != null) // Check if the child has a Sword component, indicating a sword type.
            {
                return Weapon.Sword;
            }
            else if (item.GetComponent<Bow>() != null) // Check if the item itself has a Bow component, indicating a bow type.
            {
                return Weapon.Bow;
            }
        }
        // Default to Fists if no specific type is identified.
        return Weapon.Fists;
    }

    // Reset the player's inventory by destroying all equipped items and initializing it again.
    public void ResetInventory()
    {
        // Iterate through inventory slots, destroy equipped items (excluding Fists), and initialize inventory
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

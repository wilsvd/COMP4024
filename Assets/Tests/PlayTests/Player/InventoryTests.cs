using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;



public class InventoryTests
{
    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("NAV LEVEL");
    }


    [UnityTest]
    public IEnumerator InitializeInventory_SlotsHaveFists()
    {
        Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        inventory.InitializeInventory();

        Assert.AreEqual("Fists", inventory.slots[0].item.name);
        Assert.AreEqual("Fists", inventory.slots[1].item.name);

        yield return null;
    }

    [UnityTest]
    public IEnumerator ResetInventory_SlotsHaveFists()
    {
        Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        // Mock having a Sword item
        GameObject sword = new GameObject();
        sword.name = "Sword";
        GameObject swordVFX = new GameObject();
        swordVFX.AddComponent<Sword>();
        swordVFX.transform.SetParent(sword.transform);
        inventory.AddItem(sword);

        // Mock having a Bow item
        GameObject bow = new GameObject();
        bow.name = "Bow";
        bow.AddComponent<Sword>();
        GameObject bowVFX = new GameObject();
        bowVFX.transform.SetParent(bow.transform);
        inventory.AddItem(bow);


        inventory.ResetInventory();

        Assert.AreEqual("Fists", inventory.slots[0].item.name);
        Assert.AreEqual("Fists", inventory.slots[1].item.name);

        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckItemType_SwordItemReturnsSwordEnum()
    {
        Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        GameObject sword = new GameObject();
        sword.name = "Sword";
        GameObject swordVFX = new GameObject();
        swordVFX.AddComponent<Sword>();
        swordVFX.transform.SetParent(sword.transform);

        Inventory.Weapon itemType = inventory.CheckItemType(sword);

        Assert.AreEqual(Inventory.Weapon.Sword, itemType);

        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckItemType_BowItemReturnsBowEnum()
    {
        Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        GameObject bow = new GameObject();
        bow.name = "Bow";
        bow.AddComponent<Bow>();
        GameObject bowVFX = new GameObject();
        bowVFX.transform.SetParent(bow.transform);

        Inventory.Weapon itemType = inventory.CheckItemType(bow);

        Assert.AreEqual(Inventory.Weapon.Bow, itemType);

        yield return null;
    }

    [UnityTest]
    public IEnumerator ImproveWeapon_ImprovesSwordDamage()
    {
        Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        GameObject sword = new GameObject();
        sword.name = "Sword";
        GameObject swordVFX = new GameObject();
        Sword swordScript = swordVFX.AddComponent<Sword>();
        swordVFX.transform.SetParent(sword.transform);

        // Mock having a Sword item
        swordScript.attackDamage = 30;
        inventory.AddItem(sword);
        // Improve the sword
        inventory.ImproveWeapon(Inventory.Weapon.Sword, 0);
        int currentDamage = inventory.slots[0].item.transform.GetChild(0).GetComponent<Sword>().attackDamage;
        Assert.AreEqual(50, currentDamage);

        yield return null;
        inventory.ResetInventory();
    }

    [UnityTest]
    public IEnumerator HasWeapon_ReturnsCorrectIndex()
    {
        Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        GameObject sword = new GameObject();
        sword.name = "Sword";
        GameObject swordVFX = new GameObject();
        swordVFX.AddComponent<Sword>();
        swordVFX.transform.SetParent(sword.transform);
        inventory.AddItem(sword);

        int index = inventory.HasWeapon(Inventory.Weapon.Sword);

        Assert.AreEqual(0, index);

        yield return null;
        inventory.ResetInventory();
    }

    [UnityTest]
    public IEnumerator UpdateEquippedItem_UpdatesEquippedItem()
    {
        Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        GameObject sword = new GameObject();
        sword.name = "Sword";
        GameObject swordVFX = new GameObject();
        swordVFX.AddComponent<Sword>();
        swordVFX.transform.SetParent(sword.transform);
        inventory.AddItem(sword);

        inventory.UpdateEquippedItem(sword, Inventory.Weapon.Sword);

        Assert.AreEqual(sword, inventory.equippedItem.item);
        Assert.AreEqual(Inventory.Weapon.Sword, inventory.equippedItem.type);

        yield return null;
        inventory.ResetInventory();
    }

    [UnityTest]
    public IEnumerator SwitchItems_SwitchesActiveItemToBow()
    {
        Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();

        // Mock having a Sword item
        GameObject sword = new GameObject();
        sword.name = "Sword";
        GameObject swordVFX = new GameObject();
        swordVFX.AddComponent<Sword>();

        swordVFX.transform.SetParent(sword.transform);
        inventory.AddItem(sword);

        yield return null;

        // Mock having a Bow item
        GameObject bow = new GameObject();
        bow.name = "Bow";
        bow.AddComponent<Sword>();
        GameObject bowVFX = new GameObject();
        bowVFX.transform.SetParent(bow.transform);
        inventory.AddItem(bow);

        yield return null;
        Assert.AreEqual("Sword(Clone)", inventory.slots[0].item.name);
        Assert.AreEqual("Bow(Clone)", inventory.slots[1].item.name);

        // Switch the equipped item to the Bow
        inventory.SwitchItems(false, true);
        Assert.AreEqual("Bow(Clone)", inventory.equippedItem.item.name);

        yield return null;
        inventory.ResetInventory();
    }

    [UnityTest]
    public IEnumerator SwitchItems_SwitchesActiveItemToSword()
    {
        Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();

        // Mock having a Sword item
        GameObject sword = new GameObject();
        sword.name = "Sword";
        GameObject swordVFX = new GameObject();
        swordVFX.AddComponent<Sword>();

        swordVFX.transform.SetParent(sword.transform);
        inventory.AddItem(sword);

        yield return null;

        // Mock having a Bow item
        GameObject bow = new GameObject();
        bow.name = "Bow";
        bow.AddComponent<Sword>();
        GameObject bowVFX = new GameObject();
        bowVFX.transform.SetParent(bow.transform);
        inventory.AddItem(bow);

        yield return null;
        Assert.AreEqual("Sword(Clone)", inventory.slots[0].item.name);
        Assert.AreEqual("Bow(Clone)", inventory.slots[1].item.name);

        // Switch the equipped item to the Bow
        inventory.SwitchItems(true, false);
        Assert.AreEqual("Sword(Clone)", inventory.equippedItem.item.name);

        yield return null;
        inventory.ResetInventory();
    }

    [UnityTest]
    public IEnumerator AddItem_OneItemInEmptySlot()
    {
        Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        GameObject gameObject1 = new GameObject();
        gameObject1.name = "Item";

        inventory.AddItem(gameObject1);

        Assert.AreEqual("Item(Clone)", inventory.slots[0].item.name);
        Assert.AreEqual("Fists", inventory.slots[1].item.name);

        yield return null;
        inventory.ResetInventory();
    }

    [UnityTest]
    public IEnumerator AddItem_TwoItemsInEmptySlots()
    {
        Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();

        // Mock having a Sword item
        GameObject sword = new GameObject();
        sword.name = "Sword";
        GameObject swordVFX = new GameObject();
        swordVFX.AddComponent<Sword>();

        swordVFX.transform.SetParent(sword.transform);
        inventory.AddItem(sword);

        yield return null;

        // Mock having a Bow item
        GameObject bow = new GameObject();
        bow.name = "Bow";
        bow.AddComponent<Sword>();
        GameObject bowVFX = new GameObject();
        bowVFX.transform.SetParent(bow.transform);
        inventory.AddItem(bow);

        yield return null;

        Assert.AreEqual("Sword(Clone)", inventory.slots[0].item.name);
        Assert.AreEqual("Bow(Clone)", inventory.slots[1].item.name);


        yield return null;
        inventory.ResetInventory();
    }




}

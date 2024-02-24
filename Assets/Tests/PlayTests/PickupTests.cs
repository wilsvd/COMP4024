using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PickupTests
{

    // GameObject prefab = Library.GetPrefabFromAssets("FilePath")
    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("SampleScene");
    }

    [UnityTest]
    public IEnumerator PickupNoItems()
    {
        Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        Assert.AreEqual("Fists", inventory.slots[0].name);
        Assert.AreEqual("Fists", inventory.slots[1].name);

        yield return null;
    }

    [UnityTest]
    public IEnumerator PickupOneItem()
    {
        Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        GameObject gameObject1 = new GameObject();
        Pickup pickup1 = gameObject1.AddComponent<Pickup>();
        gameObject1.name = "Item";
        pickup1.PickUp(gameObject1, inventory);

        Assert.AreEqual("Item(Clone)", inventory.slots[0].name);
        Assert.AreEqual("Fists", inventory.slots[1].name);

        yield return null;
    }

    [UnityTest]
    public IEnumerator PickupTwoItems()
    {
        Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        
        GameObject gameObject1 = new GameObject();
        Pickup pickup1 = gameObject1.AddComponent<Pickup>();
        gameObject1.name = "Item";
        pickup1.PickUp(gameObject1, inventory);


        GameObject gameObject2 = new GameObject();
        Pickup pickup2 = gameObject2.AddComponent<Pickup>();
        gameObject2.name = "Item1";
        pickup2.PickUp(gameObject2, inventory);


        Assert.AreEqual("Item(Clone)", inventory.slots[0].name);
        Assert.AreEqual("Item1(Clone)", inventory.slots[1].name);


        yield return null;
    }


}

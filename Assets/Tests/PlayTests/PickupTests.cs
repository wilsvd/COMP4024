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
        gameObject1.name = "Item";

        inventory.AddItem(gameObject1);

        Assert.AreEqual("Item(Clone)", inventory.slots[0].name);
        Assert.AreEqual("Fists", inventory.slots[1].name);

        yield return null;
    }

    [UnityTest]
    public IEnumerator PickupTwoItems()
    {
        Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        GameObject gameObject1 = new GameObject();
        gameObject1.name = "Item";

        inventory.AddItem(gameObject1);

        GameObject gameObject2 = new GameObject();
        gameObject2.name = "Item1";
        inventory.AddItem(gameObject2);

        Assert.AreEqual("Item(Clone)", inventory.slots[0].name);
        Assert.AreEqual("Item1(Clone)", inventory.slots[1].name);


        yield return null;
    }


}

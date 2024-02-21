using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PickupTests
{
    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("SampleScene");
    }
    [UnityTest]
    public IEnumerator PickupGameObject()
    {
        Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        GameObject gameObject = new GameObject();
        Pickup pickup = gameObject.AddComponent<Pickup>();
        pickup.PickUp(gameObject, inventory);
        Assert.AreEqual(1, GameObject.Find("Inventory").transform.childCount);

        yield return null;
    }

    [UnityTest]
    public IEnumerator PickupGameObjects()
    {
        Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        GameObject gameObject1 = new GameObject();
        Pickup pickup1 = gameObject1.AddComponent<Pickup>();
        pickup1.PickUp(gameObject1, inventory);
        Assert.AreEqual(1, GameObject.Find("Inventory").transform.childCount);

        GameObject gameObject2 = new GameObject();
        Pickup pickup2 = gameObject2.AddComponent<Pickup>();
        pickup2.PickUp(gameObject2, inventory);
        Assert.AreEqual(2, GameObject.Find("Inventory").transform.childCount);

        yield return null;
    }

}

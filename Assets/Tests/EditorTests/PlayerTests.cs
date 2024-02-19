using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerTests
{
    [Test]
    public void PlayerObjectExists()
    {
        GameObject gameObject = GameObject.Find("Player");

        // Open the test scene using EditorSceneManager
        var scenePath = "Assets/Scenes/SampleScene.unity";
        var scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);

        // Assert that the object with the specified name is not null
        Assert.IsNotNull(gameObject);
    }
}
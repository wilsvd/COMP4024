using UnityEngine;

public class TagAssigner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Call a method to tag all child objects
        TagAllChildren(transform, "Ground");
    }

    // Method to tag all child objects
    internal void TagAllChildren(Transform parent, string tag)
    {
        foreach (Transform child in parent)
        {
            // Tag the current child
            child.gameObject.tag = tag;
        }
    }
}

using UnityEngine;

// The TagAssigner class makes sure that every platform contains the ground tag
public class TagAssigner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Call a method to tag all child objects
        TagAllChildren(transform, "Ground");
    }

    // Tag all child objects of platform as ground
    internal void TagAllChildren(Transform parent, string tag)
    {
        foreach (Transform child in parent)
        {
            // Tag the current child
            child.gameObject.tag = tag;
        }
    }
}

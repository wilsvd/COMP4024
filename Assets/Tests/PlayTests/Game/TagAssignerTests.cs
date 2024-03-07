using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TagAssignerTests
{

    [UnityTest]
    public IEnumerator TagAssignerTestsWithEnumeratorPasses()
    {
        GameObject parent = new();
        GameObject child1 = new();
        GameObject child2 = new();
        GameObject child3 = new();
        
        child1.transform.SetParent(parent.transform);
        child2.transform.SetParent(parent.transform);
        child3.transform.SetParent(parent.transform);

        TagAssigner tagAssigner = parent.AddComponent<TagAssigner>();
        string tag = "Ground";
        tagAssigner.TagAllChildren(parent.transform, tag);

        Assert.AreEqual(tag, child1.tag);
        Assert.AreEqual(tag, child2.tag);
        Assert.AreEqual(tag, child3.tag);


        yield return null;
    }
}

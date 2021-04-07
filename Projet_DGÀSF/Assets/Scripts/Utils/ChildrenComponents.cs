using UnityEngine;
using System.Collections.Generic;

public class ChildrenComponents
{
    public static Dictionary<string, GameObject> GetChildren(GameObject go)
    {
        Dictionary<string, GameObject> children = new Dictionary<string, GameObject>();
        Transform[] c = go.GetComponentsInChildren<Transform>();
        for(int i = 0 ; i < c.Length ; i++)
        {
            children[c[i].name] = c[i].gameObject;
        }
        return children;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper
{
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform child = FindChild<Transform>(go, name, recursive);
        if (child == null)
            return null;

        return child.gameObject;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : Object
    {
        if (go == null)
            return null;

        if(recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform child = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || child.name.Contains(name))
                {
                    T component = child.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach(T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name.Contains(name))
                    return component;
            }
        }

        return null;
    }

    public static List<T> FindChildren<T>(GameObject go, string name = null) where T : Object
    {
        List<T> children = new List<T>();
        for (int i = 0; i < go.transform.childCount; i++)
        {
            Transform child = go.transform.GetChild(i);
            if (child.name.Contains(name))
                children.Add(child.GetComponent<T>());
        }

        return children;
    }
}

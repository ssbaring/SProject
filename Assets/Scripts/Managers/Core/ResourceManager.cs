using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T: Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject origin = Load<GameObject>($"Prefabs/{path}");
        if(origin == null)
        {
            Debug.Log($"Failed to load prefabs at {path}");
            return null;
        }

        GameObject go = Object.Instantiate(origin, parent);
        go.name = origin.name;

        return go;
    }

    public void Destroy(GameObject go, float duration = 0.0f)
    {
        Object.Destroy(go, duration);
    }
}

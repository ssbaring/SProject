using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private UI_Scene _sceneUI;
    private GameObject _rootUI;
    private int _sortingOrder = 10;

    public UI_Scene SceneUI { get { return _sceneUI; } set { _sceneUI = value; } }
    public GameObject RootUI
    {
        get
        {
            if(_rootUI == null)
                _rootUI = new GameObject { name = "@UI_ROOT" };
            
            return _rootUI;
        }
    }

    public void SetCanvas(GameObject go, bool sort = false)
    {
        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if(sort)
            canvas.sortingOrder = _sortingOrder++;
        else
            canvas.sortingOrder = 0;
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");
        T sceneUI = go.GetComponent<T>();
        SceneUI = sceneUI;
        go.transform.SetParent(RootUI.transform);

        return sceneUI;
    }
}

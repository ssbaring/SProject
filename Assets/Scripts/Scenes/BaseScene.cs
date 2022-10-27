using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseScene : MonoBehaviour
{
    public Define.SceneType SceneType { get; protected set; } = Define.SceneType.Start;

    void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        Object eventSystem = GameObject.FindObjectOfType(typeof(EventSystem));
        if (eventSystem == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
    }
}

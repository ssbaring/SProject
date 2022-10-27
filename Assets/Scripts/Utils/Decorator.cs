using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Decorator
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }

    public static void BindEvent(this GameObject go, Action<PointerEventData> callback, Define.UIEventType uiEventType = Define.UIEventType.Click)
    {
        UI_EventHandler evt = go.GetOrAddComponent<UI_EventHandler>();

        switch (uiEventType)
        {
            case Define.UIEventType.Click:
                evt.ClickAction -= callback;
                evt.ClickAction += callback;
                break;
            case Define.UIEventType.Enter:
                evt.EnterAction -= callback;
                evt.EnterAction += callback;
                break;
            case Define.UIEventType.Exit:
                evt.ExitAction -= callback;
                evt.ExitAction += callback;
                break;
            default:
                break;
        }
    }
}

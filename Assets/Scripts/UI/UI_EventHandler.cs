using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Action<PointerEventData> ClickAction = null;
    public Action<PointerEventData> EnterAction = null;
    public Action<PointerEventData> ExitAction = null;

    public void OnPointerClick(PointerEventData evt)
    {
        if (ClickAction != null)
            ClickAction.Invoke(evt);
    }

    public void OnPointerEnter(PointerEventData evt)
    {
        if (EnterAction != null)
            EnterAction.Invoke(evt);
    }

    public void OnPointerExit(PointerEventData evt)
    {
        if (ExitAction != null)
            ExitAction.Invoke(evt);
    }
}

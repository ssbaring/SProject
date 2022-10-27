using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_StartScene : UI_Scene
{
    enum Images
    {
        StartButton,
        OptionButton
    }

    protected override void Init()
    {
        base.Init();

        Bind<Image>(typeof(Images));

        GameObject startButton = GetImage((int)Images.StartButton).gameObject;
        GameObject optionButton = GetImage((int)Images.OptionButton).gameObject;

        startButton.BindEvent(OnClickStartButton, Define.UIEventType.Click);
        optionButton.BindEvent(OnClickOptionButton, Define.UIEventType.Click);
    }

    private void OnClickStartButton(PointerEventData evt)
    {
        Managers.Scene.LoadScene(Define.SceneType.Game);
    }

    private void OnClickOptionButton(PointerEventData evt)
    {
        Debug.Log("Click Option");
    }
}

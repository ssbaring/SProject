using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.SceneType.Start;
        Managers.UI.ShowSceneUI<UI_StartScene>();
        Managers.Audio.Play($"BGM/xDeviruchi - 02 And The Journey Begins!", Define.AudioType.Bgm);
    }
}

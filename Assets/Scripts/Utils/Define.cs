using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum SceneType
    {
        Start,
        Game,
        Boss,
        Load
    }

    public enum UIEventType
    {
        Click,
        Enter,
        Exit
    }

    public enum AudioType
    {
        Bgm,
        Effect,
        MaxCount
    }

    public enum MonsterType
    {
        Fly,
        Melee,
        Range,
        Boss
    }

    public enum GameObjectState
    {
        Idle,
        Move,
        Dead,
        Attack,
        Jump,
        Hit
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public Define.SceneType SceneType { get; private set; }
    private BaseScene _currentScene;
    public BaseScene CurrentScene
    {
        get
        {
            if(_currentScene == null)
                _currentScene = GameObject.FindObjectOfType<BaseScene>();

            return _currentScene;
        }
    }

    public void LoadScene(Define.SceneType scene)
    {
        _currentScene = null;
        SceneType = scene;
        SceneManager.LoadScene("Load");
    }
}

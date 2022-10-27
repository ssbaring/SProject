using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : BaseScene
{
    [SerializeField]
    private Slider _slider;

    [SerializeField]
    private Text _text;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.SceneType.Load;
        StartCoroutine(AsyncLoadScene());
    }

    private IEnumerator AsyncLoadScene()
    {
        yield return null;
        AsyncOperation oper = SceneManager.LoadSceneAsync(System.Enum.GetName(typeof(Define.SceneType), Managers.Scene.SceneType));
        oper.allowSceneActivation = false;

        while(!oper.isDone)
        {
            yield return null;

            if(_slider.value < 0.9f)
            {
                _slider.value = Mathf.MoveTowards(_slider.value, 0.9f, Time.deltaTime);
            } 
            else if(_slider.value >= 0.9f)
            {
                _slider.value = Mathf.MoveTowards(_slider.value, 1.0f, Time.deltaTime);
            }

            if(_slider.value >= 1.0f)
            {
                _text.text = "Press Spacebar";
            }

            if(_slider.value >= 1.0f && oper.progress >= 0.9f && Input.GetKeyDown(KeyCode.Space))
                oper.allowSceneActivation = true;
        }
    }
}

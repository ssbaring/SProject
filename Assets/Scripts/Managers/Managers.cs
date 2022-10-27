using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    //Singleton
    private static Managers _instance;
    public static Managers Instance
    {
        get
        {
            Init();
            return _instance;
        }
    }

    #region Contents
    #endregion

    #region Core
    private AudioManager _audio = new AudioManager();
    private ObjectManager _object = new ObjectManager();
    private ResourceManager _resource = new ResourceManager();
    private SceneManagerEx _scene = new SceneManagerEx();
    private UIManager _ui = new UIManager();

    public static AudioManager Audio { get { return Instance._audio; } }
    public static ObjectManager Object { get { return Instance._object; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static UIManager UI { get { return Instance._ui; } }
    #endregion

    private static void Init()
    {
        if(_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if(go == null)
            {
                go = new GameObject { name = "@Managers" };
                _instance = go.GetOrAddComponent<Managers>();
            }

            DontDestroyOnLoad(go);

            {
                _instance._audio.Init();
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

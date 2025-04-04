using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 유일성이 보장된다
    static Managers Instance
    {
        get
        {
            init();
            return s_instance;
        }
    } // 유일한 매니저를 갖고온다
    #region Contents
    GameManager _game = new GameManager();

    public static GameManager Game
    {
        get { return Instance._game; }
    }
    #endregion

    #region Core
    DataManager _data = new DataManager();
    InputManager _input = new InputManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEX _scene = new SceneManagerEX();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();

    public static DataManager Data
    {
        get { return Instance._data; }
    }
    public static InputManager Input
    {
        get { return Instance._input; }
    }
    public static PoolManager Pool
    {
        get { return Instance._pool; }
    }
    public static ResourceManager Resource
    {
        get { return Instance._resource; }
    }
    public static SceneManagerEX Scene
    {
        get { return Instance._scene; }
    }
    public static SoundManager Sound
    {
        get { return Instance._sound; }
    }
    public static UIManager UI
    {
        get { return Instance._ui; }
    }
    #endregion

    void Start()
    {
        init();
    }

    void Update()
    {
        _input.OnUpdate();
    }

    static void init()
    {
        if (s_instance == null)
        {
            // 초기화
            GameObject managerObj = GameObject.Find("@Managers");
            if (managerObj == null)
            {
                managerObj = new GameObject { name = "@Managers" };
                managerObj.AddComponent<Managers>();
            }

            DontDestroyOnLoad(managerObj);
            s_instance = managerObj.GetComponent<Managers>();

            s_instance._data.Init();
            s_instance._pool.Init();
            s_instance._sound.Init();
        }
    }

    public static void Clear()
    {
        Input.Clear();
        Sound.Clear();
        Scene.Clear();
        UI.Clear();
        Pool.Clear();
    }
}

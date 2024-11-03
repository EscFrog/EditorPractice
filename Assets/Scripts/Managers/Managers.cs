using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;   // 유일성이 보장된다
    static Managers Instance { get { init(); return s_instance; } } // 유일한 매니저를 갖고온다

    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
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
        }
    }
}

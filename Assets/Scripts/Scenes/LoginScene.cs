using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Login;

        //test
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < 2; i++)
            list.Add(Managers.Resource.Instantiate("UnityChan"));

        foreach (GameObject obj in list)
            Managers.Resource.Destroy(obj);
    }

    //Temp
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Managers.Scene.LoadScene(Define.Scene.Game);
        }
    }

    public override void Clear()
    {
        Debug.Log("Login Scene Clear");
    }
}

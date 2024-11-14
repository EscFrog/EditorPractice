using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    Coroutine co;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        Managers.UI.ShowSceneUI<UI_Inven>();

        co = StartCoroutine("CoExplodeAfterSeconds", 4.0f);
        StartCoroutine("CoStopExplode", 2.0f);
    }

    IEnumerator CoStopExplode(float second)
    {
        Debug.Log("Stop Enter");
        yield return new WaitForSeconds(second);
        Debug.Log("Stop Execute!!!");
        if (co != null)
        {
            StopCoroutine(co);
            co = null;
        }
    }

    IEnumerator CoExplodeAfterSeconds(float second)
    {
        Debug.Log("Explode Enter");
        yield return new WaitForSeconds(second);
        Debug.Log("Explode Excute!!");
        co = null;
    }

    public override void Clear()
    {
        Debug.Log("Game Scene Clear");
    }
}

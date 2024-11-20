using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
        // Managers.UI.ShowSceneUI<UI_Inven>();
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        gameObject.GetOrAddComponent<CursorController>();

        GameObject player = Managers.Game.Spawn(Define.PawnType.Player, "UnityChan");
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

        // GameObject obj = Managers.Game.Spawn(Define.PawnType.Monster, "DogPBR");
        GameObject go = new GameObject { name = "SpawningPool" };
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
        go.transform.position = new Vector3(0.0f, 0.0f, -5.0f);
        pool.SetKeepMonsterCount(3);
    }

    public override void Clear()
    {
        Debug.Log("Game Scene Clear");
    }
}

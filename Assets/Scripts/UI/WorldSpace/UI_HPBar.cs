using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : UI_Base
{
    enum GameObjects
    {
        HPBar
    }

    StatBase _stat;

    private Transform _parent;
    private Vector3 _posOffset;
    public float _yOffset = 0.5f;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        _stat = transform.parent.GetComponent<StatBase>();

        _parent = transform.parent;
        _posOffset = Vector3.up * (_parent.GetComponent<Collider>().bounds.size.y + _yOffset);
    }

    public void Update()
    {
        transform.position = _parent.position + _posOffset;
        transform.rotation = Camera.main.transform.rotation;

        float ratio = (float)_stat.Hp / (float)_stat.MaxHP;
        SetHpRatio(ratio);
    }

    public void SetHpRatio(float ratio)
    {
        GetUIObject((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
    }
}

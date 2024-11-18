using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HPBar : UI_Base
{
    enum GameObjects
    {
        HPBar
    }

    private Transform _parent;
    private Vector3 _posOffset;
    private float _yOffset = 0.5f;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }

    public void Start()
    {
        _parent = transform.parent;
        Debug.Log(_parent);
        _posOffset =
            Vector3.up * (_parent.GetComponent<Collider>().bounds.size.y)
            + new Vector3(0.0f, _yOffset, 0.0f);
    }

    public void Update()
    {
        transform.position = _parent.position + _posOffset;
    }
}

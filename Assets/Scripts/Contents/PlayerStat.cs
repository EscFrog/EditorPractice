using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerStat : StatBase
{
    [SerializeField]
    protected int _exp;

    [SerializeField]
    protected int _gold;

    public int Exp
    {
        get { return _exp; }
        set { _exp = value; }
    }

    public int Gold
    {
        get { return _gold; }
        set { _gold = value; }
    }

    private void Start()
    {
        _level = 1;
        _hp = 100;
        _maxHP = 100;
        _attack = 10;
        _defence = 5;
        _moveSpeed = 5.0f;

        _exp = 0;
        _gold = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterStat : StatBase
{
    private void Start()
    {
        _level = 1;
        _hp = 100;
        _maxHP = 100;
        _attack = 10;
        _defence = 5;
        _moveSpeed = 5.0f;
    }
}

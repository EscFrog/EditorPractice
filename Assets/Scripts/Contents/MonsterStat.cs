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
        _attack = 8;
        _defence = 1;
        _moveSpeed = 5.0f;
    }

    protected override void OnDead(StatBase attacker)
    {
        PlayerStat playerStat = attacker as PlayerStat;
        if (playerStat != null)
        {
            playerStat.Exp += 15;
        }

        base.OnDead(attacker);
    }
}

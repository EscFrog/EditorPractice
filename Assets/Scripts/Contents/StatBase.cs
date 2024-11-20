using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StatBase : MonoBehaviour
{
    [SerializeField]
    protected int _level;

    [SerializeField]
    protected int _hp;

    [SerializeField]
    protected int _maxHP;

    [SerializeField]
    protected int _attack;

    [SerializeField]
    protected int _defence;

    [SerializeField]
    protected float _moveSpeed;

    public int Level
    {
        get { return _level; }
        set { _level = value; }
    }

    public int Hp
    {
        get { return _hp; }
        set { _hp = value; }
    }

    public int MaxHP
    {
        get { return _maxHP; }
        set { _maxHP = value; }
    }

    public int Attack
    {
        get { return _attack; }
        set { _attack = value; }
    }

    public int Defence
    {
        get { return _defence; }
        set { _defence = value; }
    }

    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }

    public virtual void OnAttacked(StatBase attacker)
    {
        int damage = Mathf.Max(0, attacker.Attack - Defence);
        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = 0;
            OnDead();
        }
    }

    protected virtual void OnDead()
    {
        Managers.Game.Despawn(gameObject);
    }
}

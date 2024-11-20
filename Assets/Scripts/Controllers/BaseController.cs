using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField]
    protected Vector3 _destPos;

    [SerializeField]
    protected GameObject _lockTarget;

    [SerializeField]
    protected Define.State _state = Define.State.Idle;

    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Unknown;

    protected Animator _anim;

    public virtual Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;

            if (!gameObject.IsValid())
                return;

            switch (_state)
            {
                case Define.State.Die:
                    break;
                case Define.State.Idle:
                    _anim.CrossFade("WAIT", 0.1f);
                    break;
                case Define.State.Moving:
                    _anim.CrossFade("RUN", 0.1f);
                    break;
                case Define.State.Skill:
                    _anim.CrossFade("ATTACK", 0.1f, -1, 0);
                    break;
            }
        }
    }

    public void Start()
    {
        Init();
        _anim = GetComponent<Animator>();
    }

    public virtual void Update()
    {
        switch (State)
        {
            case Define.State.Die:
                UpdateDie();
                break;
            case Define.State.Moving:
                UpdateMoving();
                break;
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Skill:
                UpdateSkill();
                break;
        }
    }

    public abstract void Init();

    protected virtual void UpdateDie() { }

    protected virtual void UpdateMoving() { }

    protected virtual void UpdateIdle() { }

    protected virtual void UpdateSkill() { }
}

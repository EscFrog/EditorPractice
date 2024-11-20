using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    MonsterStat _stat;

    [SerializeField]
    float _scanRange = 10.0f;

    [SerializeField]
    float _attackRange = 1.0f;

    [SerializeField]
    float _rotateSpeed = 0.1f;

    float _chaseTime = 0f;

    [SerializeField]
    float _maxChaseTime = 3.0f;

    GameObject _player;
    NavMeshAgent nma;

    public override void Init()
    {
        PawnType = Define.PawnType.Monster;

        _stat = gameObject.GetComponent<MonsterStat>();

        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);

        // TODO: 매니저가 생기면 옮기자
        _player = Managers.Game.GetPlayer();

        nma = gameObject.GetOrAddComponent<NavMeshAgent>();
    }

    protected override void UpdateIdle()
    {
        if (!_player.IsValid())
            return;

        float distance = (_player.transform.position - transform.position).magnitude;
        if (distance <= _scanRange)
        {
            _lockTarget = _player;
            State = Define.State.Moving;
            return;
        }
    }

    protected override void UpdateMoving()
    {
        if (!_lockTarget.IsValid())
        {
            State = Define.State.Idle;
            return;
        }

        _destPos = _lockTarget.transform.position;
        Vector3 dir = _destPos - transform.position;
        float distance = dir.magnitude;

        // 추적 시간이 길면 추적 포기
        _chaseTime += Time.deltaTime;
        if (_chaseTime >= _maxChaseTime && distance > _attackRange)
        {
            _chaseTime = 0;
            nma.ResetPath();
            nma.speed = 0;
            State = Define.State.Idle;
            return;
        }

        // 록타겟이 내 사정거리보다 가까우면 공격. 아니면 이동.
        if (distance <= _attackRange)
        {
            _chaseTime = 0;
            nma.ResetPath();
            nma.speed = 0;
            State = Define.State.Skill;
            return;
        }
        else
        {
            // float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, distance);
            nma.SetDestination(_destPos);
            nma.speed = _stat.MoveSpeed;

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(dir),
                _rotateSpeed
            );
        }
    }

    protected override void UpdateSkill()
    {
        if (!_lockTarget.IsValid())
        {
            State = Define.State.Idle;
            return;
        }

        Vector3 dir = _lockTarget.transform.position - transform.position;
        Quaternion quat = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, quat, _rotateSpeed);
    }

    void OnHitEvent()
    {
        if (!_lockTarget.IsValid())
        {
            return;
        }

        StatBase targetStat = _lockTarget.GetComponent<StatBase>();
        targetStat.OnAttacked(_stat);

        float distance = (_lockTarget.transform.position - transform.position).magnitude;
        if (distance <= _attackRange)
            State = Define.State.Skill;
        else
            State = Define.State.Moving;
    }
}

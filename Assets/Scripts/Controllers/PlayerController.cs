using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : BaseController
{
    PlayerStat _stat;
    bool _stopSkill = false;
    int _mouseReactLayer = (1 << (int)Define.LayerMask.Ground | 1 << (int)Define.LayerMask.Monster);

    [SerializeField]
    float _rotateSpeed = 0.1f;

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Player;

        _stat = gameObject.GetComponent<PlayerStat>();

        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;

        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }

    #region Process Update by State
    protected override void UpdateMoving()
    {
        // 록타겟이 내 사정거리보다 가까우면 공격
        if (_lockTarget.IsValid())
        {
            float distance = (_destPos - transform.position).magnitude;
            if (distance <= 1)
            {
                State = Define.State.Skill;
                return;
            }
        }

        // 이동
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = Define.State.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            // nma.CalculatePath();
            nma.Move(dir.normalized * moveDist);

            Debug.DrawRay(transform.position, dir.normalized * 0.8f, Color.red);
            if (Physics.Raycast(transform.position, dir, 0.8f, LayerMask.GetMask("Wall", "Block")))
            {
                if (Input.GetMouseButton(0) == false)
                    State = Define.State.Idle;
                return;
            }

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(dir),
                _rotateSpeed
            );
        }
    }

    protected override void UpdateSkill()
    {
        if (_lockTarget.IsValid())
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, quat, _rotateSpeed);
        }
    }
    #endregion

    void OnHitEvent()
    {
        if (_lockTarget.IsValid())
        {
            // 임시방편. 제대로 하려면 맞는 쪽에서 자신의 HP를 깎아야 한다.
            StatBase targetStat = _lockTarget.GetComponent<StatBase>();
            StatBase myStat = gameObject.GetComponent<StatBase>();
            int damage = Mathf.Max(0, myStat.Attack - targetStat.Defence);
            targetStat.Hp -= damage;

            if (targetStat.Hp <= 0)
            {
                Managers.Game.Despawn(_lockTarget);
                State = Define.State.Moving;
                return;
            }
        }

        if (_stopSkill)
        {
            State = Define.State.Idle;
        }
        else
        {
            State = Define.State.Skill;
        }
    }

    void OnMouseEvent(Define.MouseEvent evt)
    {
        switch (State)
        {
            case Define.State.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Skill:
                {
                    if (evt == Define.MouseEvent.PointerUp)
                        _stopSkill = true;
                }
                break;
        }
    }

    void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 1000.0f, _mouseReactLayer);
        // Debug.DrawRay(Camera.main.transform.position, ray.direction * 1000.0f, Color.red, 0.3f);

        switch (evt)
        {
            case Define.MouseEvent.PointerDown:
                {
                    if (raycastHit)
                    {
                        _destPos = hit.point;
                        _destPos.y = 0;
                        State = Define.State.Moving;
                        _stopSkill = false;

                        if (hit.collider.gameObject.layer == (int)Define.LayerMask.Monster)
                            _lockTarget = hit.collider.gameObject;
                        else
                            _lockTarget = null;
                    }
                }
                break;
            case Define.MouseEvent.Press:
                {
                    if (_lockTarget == null && raycastHit)
                    {
                        _destPos = hit.point;
                        _destPos.y = 0;
                    }
                }
                break;
            case Define.MouseEvent.PointerUp:
                _stopSkill = true;
                break;
        }
    }
}

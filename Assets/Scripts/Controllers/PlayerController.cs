using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
        Skill,
    }

    PlayerStat _stat;
    Vector3 _destPos;
    Animator _anim;
    GameObject _lockTarget;
    int _mouseReactLayer = (1 << (int)Define.LayerMask.Ground | 1 << (int)Define.LayerMask.Monster);

    [SerializeField]
    float _rotateSpeed = 0.1f;

    [SerializeField]
    PlayerState _state = PlayerState.Idle;

    public PlayerState State
    {
        get { return _state; }
        set
        {
            _state = value;

            switch (_state)
            {
                case PlayerState.Die:
                    break;
                case PlayerState.Idle:
                    _anim.CrossFade("WAIT", 0.1f);
                    break;
                case PlayerState.Moving:
                    _anim.CrossFade("RUN", 0.1f);
                    break;
                case PlayerState.Skill:
                    _anim.CrossFade("ATTACK", 0.1f, -1, 0);
                    break;
            }
        }
    }

    void Start()
    {
        _anim = GetComponent<Animator>();
        _stat = gameObject.GetComponent<PlayerStat>();

        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;
    }

    void Update()
    {
        switch (State)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Idle:
                UpdateIdel();
                break;
            case PlayerState.Skill:
                UpdateSkill();
                break;
        }
    }

    #region Process Update by State
    void UpdateDie()
    {
        // 아무것도 못함
    }

    void UpdateMoving()
    {
        // 몬스터가 내 사정거리보다 가까우면 공격
        if (_lockTarget != null)
        {
            float distance = (_destPos - transform.position).magnitude;
            if (distance <= 1)
            {
                State = PlayerState.Skill;
                return;
            }
        }

        // 이동
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = PlayerState.Idle;
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
                    State = PlayerState.Idle;
                return;
            }

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(dir),
                _rotateSpeed
            );
        }
    }

    void UpdateIdel() { }

    void UpdateSkill()
    {
        if (_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, quat, _rotateSpeed);
        }
    }
    #endregion

    void OnHitEvent()
    {
        Debug.Log("OnHitEvent");

        if (_stopSkill)
        {
            State = PlayerState.Idle;
        }
        else
        {
            State = PlayerState.Skill;
        }
    }

    bool _stopSkill = false;

    void OnMouseEvent(Define.MouseEvent evt)
    {
        switch (State)
        {
            case PlayerState.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case PlayerState.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case PlayerState.Skill:
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
                        State = PlayerState.Moving;
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
                        _destPos = hit.point;
                }
                break;
            case Define.MouseEvent.PointerUp:
                _stopSkill = true;
                break;
        }
    }
}

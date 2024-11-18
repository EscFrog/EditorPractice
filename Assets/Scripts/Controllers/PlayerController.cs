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

    [SerializeField]
    float _rotateSpeed = 0.1f;

    Vector3 _destPos;
    Animator _anim;

    PlayerState _state = PlayerState.Idle;

    int _mouseReactLayer = (1 << (int)Define.LayerMask.Ground | 1 << (int)Define.LayerMask.Monster);

    GameObject _lockTargat;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _stat = gameObject.GetComponent<PlayerStat>();

        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;
    }

    void Update()
    {
        switch (_state)
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
        }
    }

    #region Process Update by State
    void UpdateDie()
    {
        // 아무것도 못함
    }

    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            // nma.CalculatePath();
            nma.Move(dir.normalized * moveDist);

            Debug.DrawRay(transform.position, dir.normalized * 1.5f, Color.red);
            if (Physics.Raycast(transform.position, dir, 1.5f, LayerMask.GetMask("Wall", "Block")))
            {
                if (Input.GetMouseButton(0) == false)
                    _state = PlayerState.Idle;
                return;
            }

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(dir),
                _rotateSpeed
            );
        }

        // 애니메이션 처리
        _anim.SetBool("isRunning", true);
    }

    void UpdateIdel()
    {
        // 애니메이션 처리
        _anim.SetBool("isRunning", false);
    }
    #endregion

    void OnMouseEvent(Define.MouseEvent evt)
    {
        if (_state == PlayerState.Die)
            return;

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
                        _state = PlayerState.Moving;

                        if (hit.collider.gameObject.layer == (int)Define.LayerMask.Monster)
                            _lockTargat = hit.collider.gameObject;
                        else
                            _lockTargat = null;
                    }
                }
                break;
            case Define.MouseEvent.Press:
                {
                    if (_lockTargat != null)
                        _destPos = _lockTargat.transform.position;
                    else if (raycastHit)
                        _destPos = hit.point;
                }
                break;
            case Define.MouseEvent.PointerUp:
                _lockTargat = null;
                break;
        }
    }
}

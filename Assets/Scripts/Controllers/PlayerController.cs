using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    [SerializeField]
    float _rotateSpeed = 0.1f;

    Vector3 _destPos;

    Animator _anim;

    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
    }

    PlayerState _state = PlayerState.Idle;

    void UpdateDie()
    { 
        // 酒公巴档 给窃
    }

    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.0001f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), _rotateSpeed);
        }

        // 局聪皋捞记 贸府
        _anim.SetBool("isRunning", true);
    }

    void UpdateIdel()
    {
        // 局聪皋捞记 贸府
        _anim.SetBool("isRunning", false);
    }

    void Start()
    {
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

        _anim = GetComponent<Animator>();
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

    void OnMouseClicked(Define.MouseEvent evt)
    {
        //if (evt != Define.MouseEvent.Click)
        //    return;

        if (_state == PlayerState.Die)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 1000.0f, Color.red, 0.3f);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, LayerMask.GetMask("Floor")))
        {
            _destPos = hit.point;
            _state = PlayerState.Moving;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuarterView;

    [SerializeField]
    Vector3 _delta = new Vector3(0.0f, 6.0f, -5.0f);

    [SerializeField]
    GameObject _player = null;

    Vector3 _playerCenter;

    public void SetPlayer(GameObject player)
    {
        _player = player;
    }

    void Start() { }

    void LateUpdate()
    {
        if (!_player.IsValid())
            return;

        if (_mode == Define.CameraMode.QuarterView)
        {
            _playerCenter = _player.transform.position + new Vector3(0.0f, 0.8f, 0.0f);

            Vector3 targetPosition;
            float targetDistance = _delta.magnitude;

            if (
                Physics.Raycast(
                    _playerCenter,
                    _delta,
                    out RaycastHit hit,
                    targetDistance,
                    LayerMask.GetMask("Wall")
                )
            )
            {
                targetDistance = (hit.point - _player.transform.position).magnitude * 0.8f;
            }

            // z축 거리만 부드럽게 이동
            float currentDistance = Vector3.Distance(transform.position, _playerCenter);
            float smoothedDistance = Mathf.Lerp(
                currentDistance,
                targetDistance,
                Time.deltaTime * 10.0f
            );

            targetPosition = _playerCenter + _delta.normalized * smoothedDistance;

            transform.position = targetPosition;
            transform.LookAt(_playerCenter);
        }
    }

    public void SetQuaterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuarterView;
        _delta = delta;
    }
}

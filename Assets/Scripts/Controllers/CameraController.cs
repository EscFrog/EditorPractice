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

    void Start()
    {

    }

    void LateUpdate()
    {
        if (_mode == Define.CameraMode.QuarterView)
        {
            _playerCenter = _player.transform.position + new Vector3(0.0f, 0.8f, 0.0f);
            Debug.DrawRay(_playerCenter, _delta);
            
            if (Physics.Raycast(_playerCenter, _delta, out RaycastHit hit, _delta.magnitude, LayerMask.GetMask("Wall")))
            {
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                transform.position = _playerCenter + _delta.normalized * dist;
            }
            else
            {
                transform.position = _playerCenter + _delta;
                transform.LookAt(_playerCenter + new Vector3(0.0f, 0.8f, 0.0f));
            }
        }
    }

    public void SetQuaterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuarterView;
        _delta = delta;
    }
}

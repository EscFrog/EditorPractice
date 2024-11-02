using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    [SerializeField]
    float _rotateSpeed = 0.1f;


    void Start()
    {
        Managers.Input.KeyAction -= OnKeyboardPress;
        Managers.Input.KeyAction += OnKeyboardPress;

    }

    void Update()
    {

    }

    void OnKeyboardPress()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), _rotateSpeed);
            transform.position += Vector3.forward * Time.deltaTime * _speed;

        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), _rotateSpeed);
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), _rotateSpeed);
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), _rotateSpeed);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }
    }
}

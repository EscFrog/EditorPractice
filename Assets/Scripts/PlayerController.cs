using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    void Start()
    {
        
    }

    float _yAngle = 0.0f;
    void Update()
    {
        _yAngle += Time.deltaTime * 100.0f;

        // ���� ȸ����
        //transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);

        // +- delta
        // transform.Rotate(new Vector3(0.0f, Time.deltaTime + 100.0f, 0.0f));

        // ���ʹϾ� �̿��ϴ� ���
        //transform.rotation = Quaternion.Euler(new Vector3(0.0f, _yAngle, 0.0f));


        // TransfromDirection(): Local ��ǥ�� World ��ǥ�� ��ȯ

        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.1f);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.position += Vector3.forward * Time.deltaTime * _speed;

        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.1f);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed); // �� ����� �Ʒ� ����� ����� ����
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }    

        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.1f);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.1f);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }

    }
}

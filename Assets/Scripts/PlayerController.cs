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

        // 절대 회전값
        //transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);

        // +- delta
        // transform.Rotate(new Vector3(0.0f, Time.deltaTime + 100.0f, 0.0f));

        // 쿼터니언 이용하는 방법
        //transform.rotation = Quaternion.Euler(new Vector3(0.0f, _yAngle, 0.0f));


        // TransfromDirection(): Local 좌표를 World 좌표로 변환

        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.1f);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.position += Vector3.forward * Time.deltaTime * _speed;

        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.1f);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed); // 이 방법과 아래 방법의 결과는 같음
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

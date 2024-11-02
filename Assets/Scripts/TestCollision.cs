using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision @ {collision.gameObject.name}");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger @ {other.gameObject.name}");
    }

    void Start()
    {
        
    }

    void Update()
    {
        // Debug.Log(Input.mousePosition);   // Screen coordinate
        // Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition));   // Viewport coordinate


        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            Vector3 dir = mousePos - Camera.main.transform.position;
            dir = dir.normalized;

            Debug.DrawRay(Camera.main.transform.position, dir * 1000.0f, Color.red, 1.0f);

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 1000.0f))
            {
                Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
            }
        }
    }
}

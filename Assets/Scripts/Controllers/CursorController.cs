using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    int _mouseReactLayer = (1 << (int)Define.LayerMask.Ground | 1 << (int)Define.LayerMask.Monster);

    enum CursorType
    {
        None,
        Attack,
        Hand,
    }

    CursorType _cursorType = CursorType.None;

    Texture2D _attackCursor;
    Vector2 _attackCursorOffset;
    Texture2D _handCursor;
    Vector2 _handCursorOffset;

    void Start()
    {
        _attackCursor = Managers.Resource.Load<Texture2D>("Textures/Cursor/Attack");
        _attackCursorOffset = new Vector2(_attackCursor.width / 5, 0);
        _handCursor = Managers.Resource.Load<Texture2D>("Textures/Cursor/Hand");
        _handCursorOffset = new Vector2(_handCursor.width / 3, 0);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, _mouseReactLayer))
        {
            if (hit.collider.gameObject.layer == (int)Define.LayerMask.Monster)
            {
                if (_cursorType != CursorType.Attack)
                {
                    Cursor.SetCursor(_attackCursor, _attackCursorOffset, CursorMode.Auto);
                    _cursorType = CursorType.Attack;
                }
            }
            else
            {
                if (_cursorType != CursorType.Hand)
                {
                    Cursor.SetCursor(_handCursor, _handCursorOffset, CursorMode.Auto);
                    _cursorType = CursorType.Hand;
                }
            }
        }
    }
}

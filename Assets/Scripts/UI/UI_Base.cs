using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> _objectsDict =
        new Dictionary<Type, UnityEngine.Object[]>(); // 타입별로 오브젝트 리스트가 매치되는 딕셔너리

    public abstract void Init();

    public void Start()
    {
        Init();
    }

    protected void Bind<T>(Type type)
        where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objectsDict.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Utils.FindChild(gameObject, names[i], true);
            else
                objects[i] = Utils.FindChild<T>(gameObject, names[i], true);

            if (objects[i] == null)
                Debug.Log($"Failed to bind! {names[i]}");
        }
    }

    protected T GetUI<T>(int idx)
        where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objectsDict.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }

    protected GameObject GetUIObject(int idx)
    {
        return GetUI<GameObject>(idx);
    }

    protected Text GetUIText(int idx)
    {
        return GetUI<Text>(idx);
    }

    protected Button GetUIButton(int idx)
    {
        return GetUI<Button>(idx);
    }

    protected Image GetUIImage(int idx)
    {
        return GetUI<Image>(idx);
    }

    public static void BindEvent(
        GameObject go,
        Action<PointerEventData> action,
        Define.UIEvent type = Define.UIEvent.Click
    )
    {
        UI_EventHandler evt = Utils.GetOrAddComponent<UI_EventHandler>(go);

        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
        }
    }
}

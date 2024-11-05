using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Button : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> _objectsDict =
        new Dictionary<Type, UnityEngine.Object[]>(); // 타입별로 오브젝트 리스트가 매치되는 딕셔너리

    enum Buttons
    {
        PointButton
    }

    enum Texts
    {
        PointText,
        ScoreText,
    }

    enum GameObjects
    {
        TestObject,
    }

    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        // test
        GetText((int)Texts.ScoreText).text = "Bind Test";
    }

    void Bind<T>(Type type)
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

    T Get<T>(int idx)
        where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objectsDict.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }

    Text GetText(int idx)
    {
        return Get<Text>(idx);
    }

    Button GetButton(int idx)
    {
        return Get<Button>(idx);
    }

    Image GetImage(int idx)
    {
        return Get<Image>(idx);
    }

    int _score = 0;

    public void OnButtonClicked()
    {
        _score++;
    }
}

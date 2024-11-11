using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public AudioClip audioClip;
    public AudioClip audioClip2;

    bool swicher = false;

    public void OnTriggerEnter(Collider other)
    {
        if (swicher)
        {
            Managers.Sound.Play(audioClip, Define.Sound.Bgm);
            swicher = false;
        }
        else
        {
            Managers.Sound.Play(audioClip2, Define.Sound.Bgm);
            swicher = true;
        }
    }
}

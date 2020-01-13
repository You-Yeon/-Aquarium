using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 화면 해상도 고정 스크립트
public class SetScreen : MonoBehaviour
{
    void Start()
    {
        Screen.SetResolution(1280, 720, false);
    }

}

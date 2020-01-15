using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDie : MonoBehaviour
{
    // 씬이 변경되어도 죽지 않게
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}

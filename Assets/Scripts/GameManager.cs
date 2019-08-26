using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm_instance; // 싱글턴을 할당할 전역 변수

    public static int MoveLock; // 움직임 제한

    private void Awake() // 싱글턴 구성
    {
        if (gm_instance == null)
        {
            gm_instance = this; // null이라면 자기자신을 할당
        }
        else
        {
            // 씬에 두개 이상의 Gamemanager이 존재하므로 자기 자신 제거.
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 숨기기 및 잠금
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // 움직임 제한 초기화
        MoveLock = 0;
    }


    private void Update()
    {
        
    }
}

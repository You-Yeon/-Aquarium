using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    public static UI_Controller ui_instance; // 싱글턴을 할당할 전역 변수

    private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트

    // 크로스헤어 
    public RectTransform crosshair1; // 1
    public RectTransform crosshair2; // 2
    public RectTransform crosshair3; // 3
    public RectTransform crosshair4; // 4

    // 총알
    public Text bulletsText;

    private void Awake() // 싱글턴 구성
    {
        if (ui_instance == null)
        {
            ui_instance = this; // null이라면 자기자신을 할당
        }
        else
        {
            // 씬에 두개 이상의 Gamemanager이 존재하므로 자기 자신 제거.
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

    }
    
    private void Update()
    {
        Move_ch(); // 크로스헤어 이동시 거리 처리
    }

    private void Move_ch() // 크로스헤어 이동시 거리 처리
    {
        // 움직이고 있고 채팅을 치고있지 않을 경우
        if ((playerInput.move != 0 || playerInput.rotate != 0 ) && !GameObject.Find("Team_num/" + GameObject.Find("NetManager").GetComponent<InitNetManager>().m_team_num).GetComponent<PlayerController>().FocusChat)
        {
            crosshair1.localPosition = new Vector3(30, 0, 0);
            crosshair2.localPosition = new Vector3(-30, 0, 0);
            crosshair3.localPosition = new Vector3(0, 30, 0);
            crosshair4.localPosition = new Vector3(0, -30, 0);
        }

        else // 움직이지 않을 때
        {
            crosshair1.localPosition = new Vector3(20, 0, 0);
            crosshair2.localPosition = new Vector3(-20, 0, 0);
            crosshair3.localPosition = new Vector3(0, 20, 0);
            crosshair4.localPosition = new Vector3(0, -20, 0);
        }
    }
}

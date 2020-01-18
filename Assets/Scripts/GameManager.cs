using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm_instance; // 싱글턴을 할당할 전역 변수

    private InitNetManager m_Net; // 네트워크 컴포넌트

    private bool mouseOut; // 마우스 상태 ( 0 : 잠금, 1 : 풀림 )

    public GameObject[] m_user_Character_Prefabs = new GameObject[18]; // 본인 캐릭터 Prefabs
    public GameObject[] m_other_Character_Prefabs = new GameObject[18]; // 상대 캐릭터 Prefabs

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
        // 컴포넌트 불러오기
        m_Net = GameObject.Find("NetManager").GetComponent<InitNetManager>();

        // 숨기기 및 잠금
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mouseOut = false;

        // 플레이어 생성
        for (int i = 0; i < 4; ++i)
        {
            // 본인 플레이어 생성
            if (m_Net.m_team_num == i + 1)
            {
                var new_Player = (GameObject)Instantiate(m_user_Character_Prefabs[m_Net.r_chr_num[i] - 1], UnityEngine.Vector3.zero, Quaternion.identity);
                new_Player.name = "Team_num/" + (i + 1); // 오브젝트 이름 설정
                new_Player.transform.localPosition = new UnityEngine.Vector3(m_Net.r_posX[i], m_Net.r_posY[i], m_Net.r_posZ[i]); // 오브젝트 위치 설정
                new_Player.transform.eulerAngles = new UnityEngine.Vector3(m_Net.r_rotX[i], m_Net.r_rotY[i], m_Net.r_rotZ[i]); // 오브젝트 방향 설정

            }
            // 그 외 플레이어 생성
            else
            {
                var new_Player = (GameObject)Instantiate(m_other_Character_Prefabs[m_Net.r_chr_num[i] - 1], UnityEngine.Vector3.zero, Quaternion.identity);
                new_Player.name = "Team_num/" + (i + 1); // 오브젝트 이름 설정
                new_Player.transform.localPosition = new UnityEngine.Vector3(m_Net.r_posX[i], m_Net.r_posY[i], m_Net.r_posZ[i]); // 오브젝트 위치 설정
                new_Player.transform.eulerAngles = new UnityEngine.Vector3(m_Net.r_rotX[i], m_Net.r_rotY[i], m_Net.r_rotZ[i]); // 오브젝트 방향 설정

            }
        }

        // 플레이어 입력 컴포넌트 연결
        GameObject.Find("NetManager").GetComponent<InitNetManager>().playerInput = GameObject.Find("Team_num/" + m_Net.m_team_num).GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (GameObject.Find("Team_num/" + m_Net.m_team_num).GetComponent<PlayerInput>().mouseOut) // ESC 키를 눌렀을 때
        {
            Debug.Log("ESC");

            // 마우스가 숨겨져 있다면
            if (!mouseOut)
            {
                // 마우스 풀기
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                mouseOut = true;
            }
        }

        if (GameObject.Find("Team_num/" + m_Net.m_team_num).GetComponent<PlayerInput>().mouseOn) // 마우스를 눌렀을 때
        {
            Debug.Log("CLICK");

            // 마우스가 보인다면
            if (mouseOut)
            {
                // 마우스 잠금
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                mouseOut = false;
            }
        }
    }

}

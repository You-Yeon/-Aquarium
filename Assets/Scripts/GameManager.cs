using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager gm_instance; // 싱글턴을 할당할 전역 변수

    private InitNetManager m_Net; // 네트워크 컴포넌트
    private GraphicRaycaster m_gr; // 캔버스 Raycaster
    PointerEventData ped; // 마우스 클릭시 받아오는 데이터

    private bool mouseOut; // 마우스 상태 ( 0 : 잠금, 1 : 풀림 )

    public GameObject[] m_user_Character_Prefabs = new GameObject[18]; // 본인 캐릭터 Prefabs
    public GameObject[] m_other_Character_Prefabs = new GameObject[18]; // 상대 캐릭터 Prefabs

    public Sprite[] m_R_UI_chr_imgs = new Sprite[18]; // 루비팀 캐릭터 UI 이미지
    public Sprite[] m_S_UI_chr_imgs = new Sprite[18]; // 사파이어팀 캐릭터 UI 이미지

    public Material m_R_Circle; // 루비 circle
    public Material m_S_Circle; // 사파이어 circle

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
        m_gr = GameObject.Find("UI_canvas").GetComponent<GraphicRaycaster>();
        ped = new PointerEventData(null);

        // 숨기기 및 잠금
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mouseOut = false;

        // 플레이어 UI 설정
        if (m_Net.m_team_num % 2 == 0) // 루비
            GameObject.Find("character_img").transform.GetComponent<Image>().sprite = m_R_UI_chr_imgs[m_Net.r_chr_num[m_Net.m_team_num - 1] -1]; // 오브젝트 캐릭터 이미지 설정
        
        if (m_Net.m_team_num % 2 == 1) // 사파이어
            GameObject.Find("character_img").transform.GetComponent<Image>().sprite = m_S_UI_chr_imgs[m_Net.r_chr_num[m_Net.m_team_num - 1] -1]; // 오브젝트 캐릭터 이미지 설정


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

                // 플레이어 팀 circle 설정 
                if ((i + 1) % 2 == 0) // 루비
                    new_Player.transform.GetChild(2).GetComponent<MeshRenderer>().material = m_R_Circle;

                if ((i + 1) % 2 == 1) // 사파이어
                    new_Player.transform.GetChild(2).GetComponent<MeshRenderer>().material = m_S_Circle;
            }
            // 그 외 플레이어 생성
            else
            {
                var new_Player = (GameObject)Instantiate(m_other_Character_Prefabs[m_Net.r_chr_num[i] - 1], UnityEngine.Vector3.zero, Quaternion.identity);
                new_Player.name = "Team_num/" + (i + 1); // 오브젝트 이름 설정
                new_Player.transform.localPosition = new UnityEngine.Vector3(m_Net.r_posX[i], m_Net.r_posY[i], m_Net.r_posZ[i]); // 오브젝트 위치 설정
                new_Player.transform.eulerAngles = new UnityEngine.Vector3(m_Net.r_rotX[i], m_Net.r_rotY[i], m_Net.r_rotZ[i]); // 오브젝트 방향 설정
                
                // 플레이어 팀 circle 설정 
                if ((i + 1) % 2 == 0) // 루비
                    new_Player.transform.GetChild(2).GetComponent<MeshRenderer>().material = m_R_Circle;

                if ((i + 1) % 2 == 1) // 사파이어
                    new_Player.transform.GetChild(2).GetComponent<MeshRenderer>().material = m_S_Circle;
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

            // 채팅 UI를 클릭한 경우 return
            ped.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>(); // 여기에 히트 된 개체 저장
            m_gr.Raycast(ped, results);

            if (results.Count != 0)
            {
                GameObject obj = results[0].gameObject; // 가장 위쪽에 있는 UGUI 오브젝트
                if (obj.CompareTag("Chat_UI")) // 히트 된 오브젝트의 태그와 맞으면 실행
                {
                    OnFocusChat();
                    return;
                }
            }
            
            // 채팅 포커스 중이였다가 다른 UI를 눌렀을 경우
            if (GameObject.Find("Team_num/" + m_Net.m_team_num).GetComponent<PlayerController>().FocusChat)
                OffFocusChat();

            // 마우스가 보인다면
            if (mouseOut)
            {
                // 마우스 잠금
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                mouseOut = false;
            }
        }

        if (GameObject.Find("Team_num/" + m_Net.m_team_num).GetComponent<PlayerInput>().enter)
        {
            // 채팅 포커스 중이였다면
            if (GameObject.Find("Team_num/" + m_Net.m_team_num).GetComponent<PlayerController>().FocusChat)
            {
                // 입력한 채팅 네트워크 컴포넌트에 전달
                m_Net.GetChat(GameObject.Find("Input_Text").GetComponent<Text>().text);
            }
        }

        // UDP 핑 업데이트
        GameObject.Find("Ping_Text").GetComponent<Text>().text = m_Net.m_Client.GetRecentUnreliablePingMs(m_Net.m_playerP2PGroup) + "ms";
    }

    public void GetChat(Text _text)
    {
        // 입력한 채팅 네트워크 컴포넌트에 전달
        m_Net.GetChat(_text.text);
    }

    public void OnFocusChat()
    {
        // 채팅 중
        GameObject.Find("Team_num/" + m_Net.m_team_num).GetComponent<PlayerController>().FocusChat = true;
    }

    public void OffFocusChat()
    {
        // 채팅 끝
        GameObject.Find("Team_num/" + m_Net.m_team_num).GetComponent<PlayerController>().FocusChat = false ;
    }
}

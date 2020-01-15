using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm_instance; // 싱글턴을 할당할 전역 변수

    public static int MoveLock; // 움직임 제한

    public GameObject[] m_Character_Prefabs = new GameObject[18]; // 캐릭터 Prefabs

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

        // ** 클라이언트 플레이어 초기 설정 및 생성
        var new_Player = (GameObject)Instantiate(m_Character_Prefabs[GameObject.Find("NetManager").GetComponent<InitNetManager>().m_chr_num], UnityEngine.Vector3.zero, Quaternion.identity);
        new_Player.name = "Team_num/" + GameObject.Find("NetManager").GetComponent<InitNetManager>().m_team_num; // 오브젝트 이름 설정
        new_Player.transform.localPosition = new UnityEngine.Vector3(GameObject.Find("NetManager").GetComponent<InitNetManager>().res_posX, GameObject.Find("NetManager").GetComponent<InitNetManager>().res_posY, GameObject.Find("NetManager").GetComponent<InitNetManager>().res_posZ); // 오브젝트 위치 설정

    }


    private void Update()
    {
        
    }
}

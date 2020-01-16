using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm_instance; // 싱글턴을 할당할 전역 변수

    public static int MoveLock; // 움직임 제한

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
        // 숨기기 및 잠금
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // 움직임 제한 초기화
        MoveLock = 0;

        // 플레이어 생성
        for (int i = 0; i < 4; ++i)
        {
            // 본인 플레이어 생성
            if (GameObject.Find("NetManager").GetComponent<InitNetManager>().m_team_num == i + 1)
            {
                var new_Player = (GameObject)Instantiate(m_user_Character_Prefabs[GameObject.Find("NetManager").GetComponent<InitNetManager>().r_chr_num[i] - 1], UnityEngine.Vector3.zero, Quaternion.identity);
                new_Player.name = "Team_num/" + (i + 1); // 오브젝트 이름 설정
                new_Player.transform.localPosition = new UnityEngine.Vector3(GameObject.Find("NetManager").GetComponent<InitNetManager>().r_posX[i], GameObject.Find("NetManager").GetComponent<InitNetManager>().r_posY[i], GameObject.Find("NetManager").GetComponent<InitNetManager>().r_posZ[i]); // 오브젝트 위치 설정
                new_Player.transform.eulerAngles = new UnityEngine.Vector3(GameObject.Find("NetManager").GetComponent<InitNetManager>().r_rotX[i], GameObject.Find("NetManager").GetComponent<InitNetManager>().r_rotY[i], GameObject.Find("NetManager").GetComponent<InitNetManager>().r_rotZ[i]); // 오브젝트 방향 설정

            }
            // 그 외 플레이어 생성
            else
            {
                var new_Player = (GameObject)Instantiate(m_other_Character_Prefabs[GameObject.Find("NetManager").GetComponent<InitNetManager>().r_chr_num[i] - 1], UnityEngine.Vector3.zero, Quaternion.identity);
                new_Player.name = "Team_num/" + (i + 1); // 오브젝트 이름 설정
                new_Player.transform.localPosition = new UnityEngine.Vector3(GameObject.Find("NetManager").GetComponent<InitNetManager>().r_posX[i], GameObject.Find("NetManager").GetComponent<InitNetManager>().r_posY[i], GameObject.Find("NetManager").GetComponent<InitNetManager>().r_posZ[i]); // 오브젝트 위치 설정
                new_Player.transform.eulerAngles = new UnityEngine.Vector3(GameObject.Find("NetManager").GetComponent<InitNetManager>().r_rotX[i], GameObject.Find("NetManager").GetComponent<InitNetManager>().r_rotY[i], GameObject.Find("NetManager").GetComponent<InitNetManager>().r_rotZ[i]); // 오브젝트 방향 설정

            }
        }
    }

}

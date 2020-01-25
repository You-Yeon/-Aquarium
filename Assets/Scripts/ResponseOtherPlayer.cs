using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseOtherPlayer : MonoBehaviour
{
    private InitNetManager m_Net; // 네트워크 컴포넌트

    private void Start()
    {
        m_Net = GameObject.Find("NetManager").GetComponent<InitNetManager>();
    }

    // 처음 리스폰
    public void FirstResponse()
    {
        StartCoroutine(First());
    }

    // 처음 이후 리스폰
    public void AfterResponse(int team_num)
    {
        StartCoroutine(After(team_num));
    }

    IEnumerator First()
    {
        // 임시 플레이어 컨트롤 잠금 해제
        GetComponent<OthersController>().Dead = false;

        // 5초 뒤 무적해제 하기.
        yield return new WaitForSeconds(5);

        GetComponent<OthersController>().playerRenderer.material.SetColor("_Color", new Color32(255, 255, 255, 255)); // 색상 변경 ( 흰색 )
    }

    IEnumerator After(int team_num)
    {
        // ** ---- 3초 뒤에 플레이어 숨기기
        yield return new WaitForSeconds(3);

        transform.GetChild(0).gameObject.SetActive(false); // 플레이어 본체 숨기기

        transform.Find("Root/Hips/Spine_01/Spine_02/Spine_03/Clavicle_R/Shoulder_R/Elbow_R/Hand_R/Finger_01 1/Ray Gun").gameObject.SetActive(false); // 플레이어 총 숨기기

        transform.GetChild(2).gameObject.SetActive(false); // 플레이어 팀 서클 숨기기

        // **  --- 2초 뒤에 원상복구하기
        yield return new WaitForSeconds(2);

        GetComponent<OthersController>().playerAnimator.runtimeAnimatorController = GetComponent<OthersController>().default_controller; // 애니메이션 원상복구하기

        // 플레이어 위치 바꾸기
        transform.localPosition = new UnityEngine.Vector3(m_Net.r_posX[team_num - 1], m_Net.r_posY[team_num - 1], m_Net.r_posZ[team_num - 1]);
        transform.eulerAngles = new UnityEngine.Vector3(m_Net.r_rotX[team_num - 1], m_Net.r_rotY[team_num - 1], m_Net.r_rotZ[team_num - 1]);

        // 색상 변경 ( 노랑 )
        GetComponent<OthersController>().playerRenderer.material.SetColor("_Color", new Color32(255, 215, 100, 255));

        // 플레이어 보이게 하기
        transform.GetChild(0).gameObject.SetActive(true); // 플레이어 본체

        transform.Find("Root/Hips/Spine_01/Spine_02/Spine_03/Clavicle_R/Shoulder_R/Elbow_R/Hand_R/Finger_01 1/Ray Gun").gameObject.SetActive(true); // 플레이어 총 

        transform.GetChild(2).gameObject.SetActive(true); // 플레이어 팀 서클 

        // 죽음 여부 false ( 플레이어 컨트롤 잠금 해제)
        GetComponent<OthersController>().Dead = false;

        // **  --- 5초 뒤 무적해제 하기.
        yield return new WaitForSeconds(5);

        GetComponent<OthersController>().playerRenderer.material.SetColor("_Color", new Color32(255, 255, 255, 255)); // 색상 변경 ( 흰색 ) 
    }
}

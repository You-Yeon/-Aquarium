using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottle : MonoBehaviour
{
    private PlayerController m_Controller; // 컨트롤러 컴포넌트

    private float rotationSpeed; // 물병 회전 속도
    private int GetBullet = 25; // 25개의 총알

    private bool has_signal = false;

    private void Start()
    {
        rotationSpeed = 60f; // 물병 회전 속도
        m_Controller = GameObject.Find("Team_num/" + GameObject.Find("NetManager").GetComponent<InitNetManager>().m_team_num).GetComponent<PlayerController>(); // 컴포넌트 가져오기 
    }

    private void Update()
    {
        transform.Rotate(0f, -rotationSpeed * Time.deltaTime/4, rotationSpeed * Time.deltaTime); // 회전
    }

    private void OnTriggerEnter(Collider colli)
    {
        if (colli.gameObject.tag == "MyPlayer") // 본인이 얻을 경우
        {
            if (!has_signal)
            {
                has_signal = true;

                // 플레이어 탄창에 아이템을 반영한다.
                m_Controller.Item(GetBullet);

                Destroy(gameObject); // 아이템 삭제

                // 10초 뒤 아이템 리스폰을 요구한다.
                transform.parent.GetComponent<ResponseBottle>().Response();
            }
        }
        else if (colli.gameObject.tag == "OtherPlayer")
        {
            if (!has_signal)
            {
                has_signal = true;

                Destroy(gameObject); // 아이템 삭제

                // 10초 뒤 아이템 리스폰을 요구한다.
                transform.parent.GetComponent<ResponseBottle>().Response();
            }
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottle : MonoBehaviour
{
    private float rotationSpeed; // 물병 회전 속도
    private int GetBullet = 25; // 25개의 총알

    private bool has_signal = false;

    private void Start()
    {
        rotationSpeed = 60f; // 물병 회전 속도
    }

    private void Update()
    {
        transform.Rotate(0f, -rotationSpeed * Time.deltaTime/4, rotationSpeed * Time.deltaTime); // 회전
    }

    private void OnTriggerEnter(Collider colli)
    {
        if (has_signal)
            return;

        if (colli.gameObject.tag == "MyPlayer") // 본인이 얻을 경우
        {
            has_signal = true;

            // 플레이어의 총알을 늘린다.
            GameObject.Find("Team_num/" + GameObject.Find("NetManager").GetComponent<InitNetManager>().m_team_num).GetComponent<PlayerController>().bulletsTotal += GetBullet;


            Debug.Log("gameObject.name : " + gameObject.name);

            // 다른 플레이어에게 아이템을 먹은 것을 알린다.
            GameObject.Find("NetManager").GetComponent<InitNetManager>().DelItem(gameObject.name);

            Destroy(gameObject); // 아이템 삭제
        }

        if (colli.gameObject.tag == "OtherPlayer")
        {
            has_signal = true;

            Destroy(gameObject); // 아이템 삭제
        }


    }

    public void GetSound()
    {
        // 먹었을때 소리 재생 시키기
    }
}

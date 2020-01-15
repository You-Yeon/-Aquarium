using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCount : MonoBehaviour
{
    // game count
    private float TimeLeft = 1.0f;
    private float nextTime = 0.0f;
    private int img_index = 0;

    public Sprite[] m_Count_img = new Sprite[4];

    void OnEnable()
    {
        img_index = 0;
    }

    void Update()
    {
        //1.0초 마다 실행
        if (Time.time > nextTime)
        {
            // 카운트 끝나면 게임 넘기기
            if (img_index == 4)
                GameObject.Find("UIManager").GetComponent<SceneChange>().ChangeGameScene();

            // 이미지 변경
            nextTime = Time.time + TimeLeft;
            transform.GetComponent<Image>().sprite = m_Count_img[img_index]; // 오브젝트 캐릭터 이미지 설정
            img_index++;
        }
    }
}

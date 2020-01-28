using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvas_timer : MonoBehaviour
{
    // 타이머 설정
    private int Min;
    private int Sec;

    public void timer(int min, int sec)
    {
        // 시간 업데이트
        Min = min;
        Sec = sec;

        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (true)
        {
            if (Min < 5)
            {
                // No로 강제 전송
                GameObject.Find("NetManager").GetComponent<InitNetManager>().SetRoom(0);
                break;
            }
            
            Sec--; // 1초 감소
            if (Sec == -1)
            {
                Min--;
                if (Min < 0)
                {
                    // 텍스트 갱신
                    transform.GetComponent<Text>().text = 0 + "분 " + 00 + "초 남은 방이 존재합니다.";
                    break;
                }
                else
                {
                    Sec = 59;
                }
            }

            if (Sec < 10)
            {
                // 텍스트 갱신
                transform.GetComponent<Text>().text = Min + "분 0" + Sec + "초 남은 방이 존재합니다.";
            }
            else
            {
                // 텍스트 갱신
                transform.GetComponent<Text>().text = Min + "분 " + Sec + "초 남은 방이 존재합니다.";
            }

            yield return new WaitForSeconds(1);
        }

    }
}

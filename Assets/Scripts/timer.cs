using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    // 타이머 설정
    private int Min;
    private int Sec;

    public void TimerStart()
    {
        Min = 10;
        Sec = 0;
        StartCoroutine(Timer());
    }

    public void GetTimerStart(int min, int sec)
    {
        Min = min;
        Sec = sec - 4; // 카운터 만큼 제거
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            Sec--; // 1초 감소
            if (Sec == -1)
            {
                Min--;
                if (Min < 0)
                {
                    // 텍스트 갱신
                    transform.GetComponent<Text>().text = 0 + " : " + 00;
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
                transform.GetComponent<Text>().text = Min + " : 0" + Sec;
            }
            else
            {
                // 텍스트 갱신
                transform.GetComponent<Text>().text = Min + " : " + Sec;
            }

 
        }

    }

}

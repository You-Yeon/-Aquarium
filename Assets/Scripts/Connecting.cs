using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Connecting : MonoBehaviour
{
    // connect 텍스트 움직임.
    private float TimeLeft = 0.3f;
    private float nextTime = 0.0f;
    private int str_index = 0;

    private string[] str = { "connecting...", "connecting..", "connecting." };

    void Update()
    {
        //0.3초마다 실행
        if (Time.time > nextTime)
        {
            // 텍스트 변경
            nextTime = Time.time + TimeLeft;
            transform.GetComponent<Text>().text = str[str_index];
            str_index--;

            if (str_index == -1)
                str_index = 2;
        }
    }
}

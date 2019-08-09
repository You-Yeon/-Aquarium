using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{

    private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트

    // 크로스헤어 
    public RectTransform crosshair1; // 1
    public RectTransform crosshair2; // 2
    public RectTransform crosshair3; // 3
    public RectTransform crosshair4; // 4

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

    }
    
    private void Update()
    {
        Move_ch(); // 크로스헤어 이동시 거리 처리
    }

    private void Move_ch() // 크로스헤어 이동시 거리 처리
    {
        if (playerInput.move != 0 || playerInput.rotate != 0) // 움직일 때
        {
            crosshair1.localPosition = new Vector3(30, 0, 0);
            crosshair2.localPosition = new Vector3(-30, 0, 0);
            crosshair3.localPosition = new Vector3(0, 30, 0);
            crosshair4.localPosition = new Vector3(0, -30, 0);
        }

        else // 움직이지 않을 때
        {
            crosshair1.localPosition = new Vector3(20, 0, 0);
            crosshair2.localPosition = new Vector3(-20, 0, 0);
            crosshair3.localPosition = new Vector3(0, 20, 0);
            crosshair4.localPosition = new Vector3(0, -20, 0);
        }
    }
}

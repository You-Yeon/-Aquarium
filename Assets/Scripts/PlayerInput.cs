using UnityEngine;

// 플레이어 캐릭터를 조작하기 위한 사용자 입력을 감지
public class PlayerInput : MonoBehaviour
{
    public string moveAxisName = "Vertical"; // 앞뒤
    public string rotateAxisName = "Horizontal"; // 좌우 
    public string fireButtonName = "Fire1"; // 발사

    // 값 할당은 내부에서만 가능
    public float move { get; private set; } // 감지된 움직임 입력값
    public float rotate { get; private set; } // 감지된 회전 입력값
    public bool fire { get; private set; } // 감지된 발사 입력값

    // 매프레임 사용자 입력을 감지
    private void Update()
    {

        // move에 관한 입력 감지
        move = Input.GetAxis(moveAxisName);
        // rotate에 관한 입력 감지
        rotate = Input.GetAxis(rotateAxisName);
        // fire에 관한 입력 감지
        fire = Input.GetButton(fireButtonName);
    }
}
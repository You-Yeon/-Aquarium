using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{

    public Text theText; // 게임 시작 텍스트
    public GameObject theParticle; // 게임 시작 파티클 시스템

    public AudioSource pointerSound; // 포인터 사운드
    public AudioSource clickSound; // 클릭 사운드

    public Text IDText; // ID
    public InputField PWText; // PW

    public void OnPointerEnter(PointerEventData eventData) // 포인터가 들어올 때
    {
        pointerSound.Play(); // 소리 실행

        Color myColor = new Color();
        ColorUtility.TryParseHtmlString("#A8DEEB", out myColor); // hex color
        theText.color = myColor; // 텍스트 색상 변경
        theParticle.SetActive(true); // 파티클 실행
    }

    public void OnPointerExit(PointerEventData eventData) // 포인터가 나갈 때
    {
        theText.color = Color.white; // 텍스트 색상 변경
        theParticle.SetActive(false); // 파티클 끄기
    }

    public void OnPointerUp(PointerEventData eventData)  // 마우스로 누를 때
    {
        clickSound.Play(); // 소리 실행

        theText.color = Color.white; // 텍스트 색상 변경
        theParticle.SetActive(false); // 파티클 끄기
    }

    public void OnExitDown() // 캐릭터 선택 방 나갈 때
    {
        clickSound.Play(); // 소리 실행
    }

    public void OnLoginDown() // 로그인 버튼을 누를 때 
    {
        clickSound.Play(); // 소리 실행

        // 아이디 비밀번호 전달 및 로그인 함수 호출
        GameObject.Find("NetManager").GetComponent<InitNetManager>().GetLogin(IDText.text, PWText.text); // 네트워크 매니저
    }

    public void OnCharacterDown(int character_num) // 캐릭터를 선택할 때
    {
        // 캐릭터 번호 전달 및 방 호출
        GameObject.Find("NetManager").GetComponent<InitNetManager>().GetRoom(character_num); // 네트워크 매니저
    }

    public void OnLeaveDown() // 게임 방을 떠날 때
    {
        clickSound.Play(); // 소리 실행

        // 게임 방을 떠날 때
        GameObject.Find("NetManager").GetComponent<InitNetManager>().LeaveRoom(); // 네트워크 매니저
    }
}



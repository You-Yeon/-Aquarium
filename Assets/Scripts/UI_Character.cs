using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Character : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    public GameObject theParticle; // 파티클 시스템

    public AudioSource pointerSound; // 포인터 사운드
    public AudioSource clickSound; // 클릭 사운드

    public void OnPointerEnter(PointerEventData eventData) // 포인터가 들어올 때
    {
        pointerSound.Play(); // 소리 실행

        transform.localScale += new Vector3(0.1f, 0.1f, 0f);
        theParticle.SetActive(true); // 파티클 실행
    }

    public void OnPointerExit(PointerEventData eventData) // 포인터가 나갈 때
    {
        transform.localScale -= new Vector3(0.1f, 0.1f, 0f);
        theParticle.SetActive(false); // 파티클 끄기
    }

    public void OnPointerUp(PointerEventData eventData)  // 마우스로 누를 때
    {
        clickSound.Play(); // 소리 실행

        transform.localScale -= new Vector3(0.1f, 0.1f, 0f);
        theParticle.SetActive(false); // 파티클 끄기
    }
}

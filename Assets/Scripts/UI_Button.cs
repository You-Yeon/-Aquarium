using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Text theText; // 텍스트

    public void OnPointerEnter(PointerEventData eventData) // 포인터가 들어올 때
    {
        Color myColor = new Color();
        ColorUtility.TryParseHtmlString("#A8DEEB", out myColor); // hex color
        theText.color = myColor; // 텍스트 색상 변경
    }

    public void OnPointerExit(PointerEventData eventData) // 포인터가 나갈 때
    {
        theText.color = Color.white; // 텍스트 색상 변경
    }
}



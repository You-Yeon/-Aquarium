using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseBottle : MonoBehaviour
{
    public int delaysec = 10; // 딜레이 시간
    public GameObject m_bottle_prefabs; // 물병 아이템
    public AudioSource itemSound; // 아이템 사운드

    // 리스폰
    public void Response()
    {
        itemSound.Play(); // 사운드 플레이
        StartCoroutine(Item());
    }

    IEnumerator Item()
    {
        // 딜레이 후 아이템을 생성한다.
        yield return new WaitForSeconds(delaysec);
        var new_bottle = (GameObject)Instantiate(m_bottle_prefabs);
        new_bottle.transform.SetParent(gameObject.transform); // 오브젝트 부모 설정
        new_bottle.transform.localScale = new UnityEngine.Vector3(1, 1, 1); // 오브젝트 스케일 설정
        new_bottle.transform.localPosition = new UnityEngine.Vector3(0, -0.3f, 0); // 오브젝트 위치 설정
        new_bottle.transform.SetSiblingIndex(0); // 인덱스 설정
    }
}

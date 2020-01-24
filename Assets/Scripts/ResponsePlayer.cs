using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsePlayer : MonoBehaviour
{
    // 처음 리스폰
    public void FirstResponse()
    {
        StartCoroutine(First());
    }

    // 처음 이후 리스폰
    public void AfterResponse()
    {
        StartCoroutine(After());
    }

    IEnumerator First()
    {
        Debug.Log("first");
        // 5초 뒤 무적해제 하기.
        yield return new WaitForSeconds(5);
        GameObject.Find("NetManager").GetComponent<InitNetManager>().SetResponse(false); // 서버에 상태를 알린다.
        GetComponent<PlayerController>().playerRenderer.material.SetColor("_Color", new Color32(255, 255, 255, 255));
    }

    IEnumerator After()
    {
        //리스폰 후 5초 뒤 무적해제 하기.
        yield return new WaitForSeconds(5);
        GameObject.Find("NetManager").GetComponent<InitNetManager>().SetResponse(false); // 서버에 상태를 알린다.
        // 리스폰...
        //리스폰 된 오브젝트.GetComponent<PlayerController>().playerRenderer.material.SetColor("_Color", new Color32(255, 255, 255, 255));
    }

}

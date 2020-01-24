using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseOtherPlayer : MonoBehaviour
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
        GetComponent<OthersController>().playerRenderer.material.SetColor("_Color", new Color32(255, 255, 255, 255));
    }

    IEnumerator After()
    {
        //리스폰 후 5초 뒤 무적해제 하기.
        yield return new WaitForSeconds(5);
        // 리스폰...
        //리스폰 된 오브젝트.GetComponent<OthersController>().playerRenderer.material.SetColor("_Color", new Color32(255, 255, 255, 255));
    }
}

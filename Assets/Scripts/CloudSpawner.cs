using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public int del_z_position; // 부셔질 z 위치
    public Vector3 set_position; // 생성될 위치

    public float move_speed; // 속도

    public GameObject cloudPrefab; // 구름 프리팹

    void Update()
    {
        if ( transform.position.z <= del_z_position) // 구름의 z값이 해당 포지션 값일 경우
        {
            Instantiate(cloudPrefab, set_position, gameObject.transform.rotation); // 생성될 위치에 생성
            Destroy(gameObject); // 자기 자신을 없애고
        }

       transform.Translate(0,0,-move_speed); // 구름 이동
    }
}

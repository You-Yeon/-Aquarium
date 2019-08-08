using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed; // 탄알 이동 속력
    private Rigidbody bulletRigidbody; // 이동에 사용 할 리지드바디 컴포넌트

    public RaycastHit ray_water_mark; // 물 흔적 레이케스트
    public GameObject pre_water_mark; // 물 흔적 prefabs

    public Material[] mat_water_mark = new Material[6]; // 물 흔적 material


    void Start()
    {
        speed = 10f; // 속도 10

        // 게임 오브젝트에서 Rigidbody 컴포넌트를 찾아서 bulletRigidbody에 할당
        bulletRigidbody = GetComponent<Rigidbody>();

        // 리지드바디의 속도 = 앞쪽 방향 * 이동속력
        bulletRigidbody.velocity = transform.forward * speed;

        // 3초 뒤에 자신의 게임 오브젝트 파괴
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject ob_water_mark1 = Instantiate(pre_water_mark, ray_water_mark.point , Quaternion.FromToRotation(Vector3.up, ray_water_mark.normal)); // 물 흔적 생성
        ob_water_mark1.GetComponentInChildren<MeshRenderer>().material = mat_water_mark[Random.Range(0, mat_water_mark.Length)]; // 물 흔적 material 랜덤 생성
        Destroy(ob_water_mark1, 0.5f); // 0.5초뒤에 사라짐
        Destroy(gameObject); // 물론 총알도
    }

}

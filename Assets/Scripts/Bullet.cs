using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed; // 탄알 이동 속력
    private Rigidbody bulletRigidbody; // 이동에 사용 할 리지드바디 컴포넌트
    public GameObject pre_water_mark; // 물 흔적 prefabs

    public Material[] mat_water_mark = new Material[6]; // 물 흔적 material

    public int damage;

    private bool has_signal = false;

    void Start()
    {
        speed = 50f; // 속도 

        // 게임 오브젝트에서 Rigidbody 컴포넌트를 찾아서 bulletRigidbody에 할당
        bulletRigidbody = GetComponent<Rigidbody>();

        // 리지드바디의 속도 = 앞쪽 방향 * 이동속력
        bulletRigidbody.velocity = transform.forward * speed;

        // 3초 뒤에 자신의 게임 오브젝트 파괴
        Destroy(gameObject, 3f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);

        if (collision.gameObject.tag == "MyPlayer") // 본인에게는 맞지 않도록
        {
            Destroy(gameObject); // 총알 삭제
            return;
        }

        if (collision.gameObject.tag == "OtherPlayer") // 다른 사람이 맞을 경우
        {
            // 본인이 루비팀일 경우
            if (GameObject.Find("NetManager").GetComponent<InitNetManager>().m_team_num % 2 == 0)
            {
                // 같은 팀은 스킵
                if (collision.gameObject.name == "Team_num/2" || collision.gameObject.name == "Team_num/4")
                {
                    Destroy(gameObject); // 총알 삭제
                    return;
                }
                else // 다른 팀
                {
                    if (!has_signal)
                    {
                        has_signal = true;

                        // 팀 번호와 데미지 전송
                        string[] split_text = collision.gameObject.name.Split('/');
                        GameObject.Find("NetManager").GetComponent<InitNetManager>().SetHP(int.Parse(split_text[1]), damage);
                    }

                    Destroy(gameObject); // 총알 삭제
                    return;
                }
            }
            // 본인이 사파이어팀일 경우
            else if (GameObject.Find("NetManager").GetComponent<InitNetManager>().m_team_num % 2 == 1)
            {
                // 같은 팀은 스킵
                if (collision.gameObject.name == "Team_num/1" || collision.gameObject.name == "Team_num/3")
                {
                    Destroy(gameObject); // 총알 삭제
                    return;
                }
                else // 다른 팀
                {
                    if (!has_signal)
                    {
                        has_signal = true;

                        // 팀 번호와 데미지 전송
                        string[] split_text = collision.gameObject.name.Split('/');
                        GameObject.Find("NetManager").GetComponent<InitNetManager>().SetHP(int.Parse(split_text[1]), damage);
                    }

                    Destroy(gameObject); // 총알 삭제
                    return;
                }
            }
        }

        if (collision.gameObject.tag == "Wall") // 접근 제한 범위일 경우에
        {
            Destroy(gameObject); // 총알 삭제
            return;
        }

        GameObject ob_water_mark1 = Instantiate(pre_water_mark, collision.contacts[0].point, Quaternion.FromToRotation(Vector3.up, collision.contacts[0].normal)); // 물 흔적 생성
        ob_water_mark1.GetComponentInChildren<MeshRenderer>().material = mat_water_mark[Random.Range(0, mat_water_mark.Length)]; // 물 흔적 material 랜덤 생성
        Destroy(ob_water_mark1, 0.5f); // 0.5초뒤에 사라짐
        Destroy(gameObject); // 물론 총알도
    }
}

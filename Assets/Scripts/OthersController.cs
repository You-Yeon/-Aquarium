using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OthersController : MonoBehaviour
{
    private float moveSpeed; // 앞뒤 움직임의 속도
    private float mouseSpeed; // 마우스 속도

    public GameObject bulletPrefab; // 총알 Prefab
    public GameObject shootPoint; // 발사 지점

    private Rigidbody playerRigidbody; // 플레이어 캐릭터의 리지드바디
    private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터

    public Transform RayPoint; // 레이캐스트 시작 지점
    public float range; // 레이캐스트 범위

    public AudioSource audioSource_walk; // 걷는 소리
    public AudioSource audioSource_fire; // 발사 소리

    private Animator anim; // 애니메이터 
    private bool isReloading = false; // 장전 애니메이션 진행 여부
    public AudioClip reloadSound; // 장전 사운드

    public float m_move; // 움직임 입력 값
    public float m_rotate; // 회전 입력 값
    public float m_mouseX; // 마우스 입력 값

    public float m_px; // 위치
    public float m_py;
    public float m_pz;

    public float m_rx; // 방향
    public float m_ry;
    public float m_rz;

    private void Start()
    {
        // 사용할 컴포넌트들의 참조를 가져오기
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        anim = GetComponent<Animator>();

        moveSpeed = 5f; // 앞뒤 움직임의 속도
        mouseSpeed = 2.0f; // 마우스 속도

        RayPoint = GameObject.Find("RayPoint").GetComponent<Transform>();
    }

    // FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    private void FixedUpdate()
    {

        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(1); // 상의 애니메이션부분인 1번 레이어를 가져온다.
        isReloading = info.IsName("Reload"); // 장전 중인지 아닌지 판별

        // 움직임 실행
        Move();

        // 입력 값에 따라 애니메이터의 Move 파라미터 값 변경
        playerAnimator.SetFloat("Vertical", m_move);
        playerAnimator.SetFloat("Horizontal", m_rotate);

    }

    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void Move()
    {
        // 상대방 마우스 회전에 따른 캐릭터 방향 회전
        playerRigidbody.MoveRotation(Quaternion.Euler(m_rx, m_ry, m_rz));

        //// 이동 방향 벡터 계산
        //Vector3 moveDir = (m_move * transform.forward) + (m_rotate * transform.right);

        //// 상대적으로 이동할 거리 계산
        //Vector3 moveDistance = moveDir.normalized * moveSpeed * Time.deltaTime;

        // 리지드바디를 이용해 게임 오브젝트 위치 변경
        playerRigidbody.MovePosition(new Vector3(m_px, m_py, m_pz));

        if (!audioSource_walk.isPlaying) // 걷는 사운드가 나오지 않으면
        {
            if (m_move != 0 || m_rotate != 0) // 걷고 있다면
            {
                audioSource_walk.Play(); // 사운드 플레이
            }
        }

        if (m_move == 0 && m_rotate == 0) // 움직이지않으면
        {
            audioSource_walk.Stop(); // 사운드 스탑
        }

    }

    //private void fire()
    //{
    //    if (playerinput.move == 0 && playerinput.rotate == 0) // 산탄 효과 비활성
    //    {
    //        accuracy = 0f;
    //    }
    //    else // 산탄 효과 활성
    //    {
    //        accuracy = 0.02f;
    //    }

    //    playeranimator.crossfadeinfixedtime("fire", 0.01f); // 발사 애니메이션

    //    raycasthit hit;
    //    debug.drawray(raypoint.position, raypoint.transform.forward * range + random.onunitsphere * accuracy, color.blue, 0.3f); // 레이케스트 발사
    //    physics.raycast(raypoint.position, raypoint.transform.forward + random.onunitsphere * accuracy, out hit, range); // 레이케스트 발사

    //    gameobject bullet = instantiate(bulletprefab, shootpoint.transform.position, shootpoint.transform.rotation); // 총알 생성
    //    bullet.transform.lookat(hit.point);

    //    firetimer = 0.0f; // 시간 리셋

    //    audiosource_fire.play(); // 발사 소리

    //    debug.log("ray : " + hit.point);
    //}

    //private void DoReload() // 장전 시작
    //{
    //    if (!isReloading && currentBullets < bulletsPerMag && bulletsTotal > 0)
    //    {
    //        anim.CrossFadeInFixedTime("Reload", 0.01f); // 장전 애니메이션
    //        audioSource_fire.PlayOneShot(reloadSound); // 장전 사운드
    //    }
    //}

}
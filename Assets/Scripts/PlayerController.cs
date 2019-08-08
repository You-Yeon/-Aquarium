﻿using UnityEngine;
using Cinemachine;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerController : MonoBehaviour {
    public float moveSpeed; // 앞뒤 움직임의 속도
    public float mouseSpeed; // 마우스 속도

    public float fireRate; // 발사 딜레이
    public float fireTimer; // 타이머

    public CinemachineVirtualCamera vcam; // 추적 카메라

    public GameObject bulletPrefab; // 총알 Prefab
    public GameObject shootPoint; // 발사 지점

    private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody playerRigidbody; // 플레이어 캐릭터의 리지드바디
    private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터

    public Transform RayPoint;
    public float range;

    private void Start() {
        // 사용할 컴포넌트들의 참조를 가져오기
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();

        moveSpeed = 5f; // 앞뒤 움직임의 속도
        mouseSpeed = 2.0f; // 마우스 속도

        fireRate = 0.3f; // 발사 딜레이
        fireTimer = 0f; // 타이머

        vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = 1.9f;
    }

    // FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    private void FixedUpdate() {

        // 움직임 실행
        Move();

        // 발사
        Fire();

        // 마우스 회전
        Mouse();

        // 입력 값에 따라 애니메이터의 Move 파라미터 값 변경
        playerAnimator.SetFloat("Vertical", playerInput.move);
        playerAnimator.SetFloat("Horizontal", playerInput.rotate); 

        if (fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime;
        }
    }

    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void Move() {

        // 이동 방향 벡터 계산
        Vector3 moveDir = (playerInput.move * transform.forward) + (playerInput.rotate * transform.right);

        // 상대적으로 이동할 거리 계산
        Vector3 moveDistance = moveDir.normalized * moveSpeed * Time.deltaTime;

        // 리지드바디를 이용해 게임 오브젝트 위치 변경
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
    }

    private void Fire()
    {
        if (fireTimer < fireRate) //마지막 발사 시간 간격이 fireRate보다 작으면 return
        {
            return;
        }

        if (Input.GetButton("Fire1")) // 발사 버튼.
        {

            playerAnimator.CrossFadeInFixedTime("Fire", 0.01f); // 발사 애니메이션

            RaycastHit hit;
            Physics.Raycast(RayPoint.position, RayPoint.transform.forward, out hit, range); // 레이케스트 발사

            GameObject bullet = Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation); // 총알 생성
            Bullet bullet_script = bullet.GetComponent<Bullet>(); // Bullet 스크립트 접근
            bullet_script.ray_water_mark = hit; // 레이저 좌표 전송
            bullet.transform.LookAt(hit.point);

            fireTimer = 0.0f; // 시간 리셋
        }
    }

    private void Mouse()
    {
        playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(Vector3.up * mouseSpeed * playerInput.mouseX); // X

        if ((vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y -= playerInput.mouseY / 10 )<= 3f && (vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y -= playerInput.mouseY / 10 )>= 1f)
        {
            vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y -= playerInput.mouseY / 10; // Y 
        }
        else if (vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y > 3f)
        {
            vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = 3f;
        }
        else if (vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y < 1f)
        {
            vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = 1f;
        }

        Debug.Log(" cam : " + vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y);
        Debug.Log("mouse y : " + playerInput.mouseY);
    }
}
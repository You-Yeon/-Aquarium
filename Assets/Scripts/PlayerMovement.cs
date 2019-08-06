using UnityEngine;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerMovement : MonoBehaviour {
    public float moveSpeed; // 앞뒤 움직임의 속도
    public float mouseSpeed; // 마우스 속도

    public float fireRate; // 발사 딜레이
    public float fireTimer; // 타이머

    public GameObject bulletPrefab; // 총알 Prefab
    public GameObject shootPoint; // 발사 지점
    
    private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody playerRigidbody; // 플레이어 캐릭터의 리지드바디
    private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터

    private void Start() {
        // 사용할 컴포넌트들의 참조를 가져오기
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();

        moveSpeed = 5f; // 앞뒤 움직임의 속도
        mouseSpeed = 2.0f; // 마우스 속도

        fireRate = 0.3f; // 발사 딜레이
        fireTimer = 0f; // 타이머
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
            Debug.Log(fireTimer);
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

        if (playerInput.move != 0f || playerInput.rotate != 0f) // 움직이고 있는 상태면 return
        {
            return;
        }

        if (Input.GetButton("Fire1")) // 발사 버튼.
        {

            playerAnimator.CrossFadeInFixedTime("Fire", 0.01f); // fire animation

            GameObject bullet = Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation);

            fireTimer = 0.0f;
        }
    }

    private void Mouse()
    {
        playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(Vector3.up * mouseSpeed * playerInput.mouseX);

    }
}
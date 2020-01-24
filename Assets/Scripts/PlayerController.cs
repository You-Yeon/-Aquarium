using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerController : MonoBehaviour {

    private float moveSpeed; // 앞뒤 움직임의 속도
    private float mouseSpeed; // 마우스 속도

    private float fireRate; // 발사 딜레이
    private float fireTimer; // 타이머

    private float accuracy; // 산탄 거리

    public CinemachineVirtualCamera vcam; // 추적 카메라

    public GameObject bulletPrefab; // 기본총알 Prefab
    public GameObject SapphirebulletPrefab; // 보라색 총알 Prefab
    public GameObject shootPoint; // 발사 지점

    private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody playerRigidbody; // 플레이어 캐릭터의 리지드바디
    private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터
    public SkinnedMeshRenderer playerRenderer ; // 플레이어 캐릭터의 렌더링

    private int bulletsPerMag; // 탄창 속 총알 수
    private int bulletsTotal; // 총 총알 수
    private int currentBullets; // 현재 탄창의 총알 수

    public Transform RayPoint; // 레이캐스트 시작 지점
    public float range; // 레이캐스트 범위

    public AudioSource audioSource_walk; // 걷는 소리
    public AudioSource audioSource_fire; // 발사 소리

    private Animator anim; // 애니메이터 
    private bool isReloading = false; // 장전 애니메이션 진행 여부
    public AudioClip reloadSound; // 장전 사운드

    public bool FocusChat = false; // 채팅 focus

    public bool GetResponse = false; // 무적 여부

    private void Start() {
        // 사용할 컴포넌트들의 참조를 가져오기
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerRenderer = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
        anim = GetComponent<Animator>();

        bulletsPerMag = 25; // 한 탄창의 수 
        bulletsTotal = 125; // 전체 - 한 탄창
        currentBullets = bulletsPerMag; // 현재 총알 수

        moveSpeed = 5f; // 앞뒤 움직임의 속도
        mouseSpeed = 2.0f; // 마우스 속도

        fireRate = 0.3f; // 발사 딜레이
        fireTimer = 0f; // 타이머

        accuracy = 0f; // 초기 값은 0

        vcam = GameObject.Find("Follow Cam").GetComponent<CinemachineVirtualCamera>();
        RayPoint = GameObject.Find("RayPoint").GetComponent<Transform>();

        GameObject.Find("Follow Cam").GetComponent<CinemachineVirtualCamera>().Follow = GetComponent<Transform>();
        GameObject.Find("Follow Cam").GetComponent<CinemachineVirtualCamera>().LookAt = GetComponent<Transform>();


        vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = 1.9f; // 초기 화면 y값

        UI_Controller.ui_instance.bulletsText.text = currentBullets + " / " + bulletsTotal; // UI 총알 개수 반영
    }

    // FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    private void FixedUpdate() {

        // 채팅치고 있을 경우에는 return
        if (FocusChat)
        {
            return;
        }

        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(1); // 상의 애니메이션부분인 1번 레이어를 가져온다.
        isReloading = info.IsName("Reload"); // 장전 중인지 아닌지 판단

        // 움직임 실행
        Move();

        // 마우스 회전
        Mouse();

        if (playerInput.fire) // 발사 버튼을 눌렀을 때.
        {
            // 발사
            Fire();
        }

        if (playerInput.reload) // 장전 버튼을 눌렀을 때
        {
            // 장전
            DoReload();
        }

        // 입력 값에 따라 애니메이터의 Move 파라미터 값 변경
        playerAnimator.SetFloat("Vertical", playerInput.move);
        playerAnimator.SetFloat("Horizontal", playerInput.rotate); 

        if (fireTimer < fireRate) // 발사 시간 간격 갱신
        {
            fireTimer += Time.deltaTime;
        }
    }

    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void Move()
    {
        // 이동 방향 벡터 계산
        Vector3 moveDir = (playerInput.move * transform.forward) + (playerInput.rotate * transform.right);

        // 상대적으로 이동할 거리 계산
        Vector3 moveDistance = moveDir.normalized * moveSpeed * Time.deltaTime;

        // 리지드바디를 이용해 게임 오브젝트 위치 변경
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);

        if (!audioSource_walk.isPlaying) // 걷는 사운드가 나오지 않으면
        {
            if (playerInput.move != 0 || playerInput.rotate != 0 ) // 걷고 있다면
            {
                audioSource_walk.Play(); // 사운드 플레이
            }
        }

        if (playerInput.move == 0 && playerInput.rotate == 0) // 움직이지않으면
        {
            audioSource_walk.Stop(); // 사운드 스탑
        }

    }

    private void Fire()
    {
        if (fireTimer < fireRate) //마지막 발사 시간 간격이 fireRate보다 작으면 return
        {
            return;
        }

        if (currentBullets == 0) // 총알이 없으면 return
        {
            return;
        }

        if (playerInput.move == 0 && playerInput.rotate == 0) // 산탄 효과 비활성
        {
            accuracy = 0f;
        }
        else // 산탄 효과 활성
        {
            accuracy = 0.02f;
        }

        playerAnimator.CrossFadeInFixedTime("Fire", 0.01f); // 발사 애니메이션

        RaycastHit hit;
        Debug.DrawRay(RayPoint.position, RayPoint.transform.forward * range + Random.onUnitSphere * accuracy, Color.blue, 0.3f); // 레이케스트 발사
        Physics.Raycast(RayPoint.position, RayPoint.transform.forward + Random.onUnitSphere * accuracy, out hit, range); // 레이케스트 발사

        // 총알 종류
        int kind = 0;

        if (GameObject.Find("NetManager").GetComponent<InitNetManager>().m_team_num % 2 == 1) // 사파이어일 경우
        {
            // 20% 확률로 보라색 총알 나옴.
            // 보라색 총알은 12의 데미지를 줌.
            if (Random.Range(0, 5) == 0)
            {
                kind = 1;
            }
        }

        // 디폴트 총알
        if (kind == 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation); // 총알 생성
            bullet.transform.LookAt(hit.point);
        }
        // 보라색 총알
        if (kind == 1)
        {
            GameObject bullet = Instantiate(SapphirebulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation); // 총알 생성
            bullet.transform.LookAt(hit.point);
        }

        // 총알 발사 네트워크 전송
        GameObject.Find("NetManager").GetComponent<InitNetManager>().GetShoot(hit.point.x, hit.point.y, hit.point.z, kind);

        fireTimer = 0.0f; // 시간 리셋

        audioSource_fire.Play(); // 발사 소리

        currentBullets--; // 총알 초기화
        UI_Controller.ui_instance.bulletsText.text = currentBullets + " / " + bulletsTotal; // UI 총알 개수 반영

    }

    private void DoReload() // 장전 시작
    {
        if (!isReloading && currentBullets < bulletsPerMag && bulletsTotal > 0)
        {
            anim.CrossFadeInFixedTime("Reload", 0.01f); // 장전 애니메이션
            audioSource_fire.PlayOneShot(reloadSound); // 장전 사운드
        }
    }

    public void Reload() // 탄약 최종 반영
    {
        int bulletsToReload = bulletsPerMag - currentBullets; // 장전될 탄약의 수
        if (bulletsToReload > bulletsTotal) // 전체 탄약보다 클 경우
        {
            bulletsToReload = bulletsTotal; // 전체 값과 동일하게
        }
        currentBullets += bulletsToReload; // 현재에 더하고
        bulletsTotal -= bulletsToReload; // 전체는 지우고
        UI_Controller.ui_instance.bulletsText.text = currentBullets + " / " + bulletsTotal; // UI에 반영

    }

    public void Item(int bullets) // 아이템 적용
    {
        // 플레이어의 총알을 늘린다.
        bulletsTotal += bullets;
        UI_Controller.ui_instance.bulletsText.text = currentBullets + " / " + bulletsTotal; // UI 총알 개수 반영
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

        //Debug.Log(" cam : " + vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y);
        //Debug.Log("mouse y : " + playerInput.mouseY);
    }
}
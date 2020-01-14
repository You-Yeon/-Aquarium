using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Nettention.Proud;

public class InitNetManager : MonoBehaviour
{
    // RMI 호출 및 받기 위한 모듈 생성
    S2C2S.Proxy m_proxy = new S2C2S.Proxy();
    S2C2S.Stub m_stub = new S2C2S.Stub();

    // 프로토콜 버전과 서버 포트 번호
    public static System.Guid Version = new System.Guid("{ 0xe71e4cec, 0x204e, 0x4374, { 0xb1, 0x63, 0x18, 0x6d, 0xc5, 0xc8, 0x4f, 0x5 } }");
    public static ushort ServerPort = 35475;

    // 클라이언트 객체
    NetClient m_Client;

    // 게임 방 유저 UI
    public GameObject GameRoom_canvas;
    
    public GameObject m_Ruby_PlayerPrefab;
    public GameObject m_Sapphire_PlayerPrefab;

    // 캐릭터 이미지
    public Sprite[] m_S_character_imgs = new Sprite[18];
    public Sprite[] m_R_character_imgs = new Sprite[18];

    // 아이디와 비밀번호
    string m_userID;
    string m_password;

    // 팀 번호
    public int m_team_num;

    // 클라이언트 연결 상태
    enum MyState
    {
        Disconnected,
        Connecting,
        Connected
    }
    MyState m_state = MyState.Disconnected;

    // 클라이언트 캐릭터 종류
    enum MyCharacter
    {
        NONE            = 0,
        C_FastFoodGuy,  // 1
        C_Biker,        // 2
        C_FireFighter,  // 3
        C_GamerGirl,    // 4
        C_Gangster,     // 5
        C_Grandma,      // 6
        C_Grandpa,      // 7
        C_HipsterGirl,  // 8
        C_HipsterGuy,   // 9
        C_Hobo,         // 10
        C_Jock,         // 11
        C_Paramedic,    // 12
        C_PunkGirl,     // 13
        C_PunkGuy,      // 14
        C_RoadWorker,   // 15
        C_ShopKeeper,   // 16
        C_SummerGirl,   // 17
        C_Tourist      // 18
    }
    MyCharacter m_Character = MyCharacter.NONE;

    // Start is called before the first frame update
    private void Start()
    {
        // 클라이언트 객체 생성
        m_Client = new NetClient();

        // 서버 접속 시도 결과 핸들러
        m_Client.JoinServerCompleteHandler =
            (ErrorInfo info, ByteArray replyFromServer) =>
            {
                if (info.errorType == ErrorType.Ok)
                {
                    // 서버에 암호화된 메시지를 보냄
                    // 입력받은 아이디와 비밀번호를 전송
                    m_proxy.RequestLogin(HostID.HostID_Server,
                        RmiContext.SecureReliableSend, m_userID, m_password);
                    m_state = MyState.Connected;
                }
                else
                {
                    Debug.Log(info.ToString());
                    m_state = MyState.Disconnected;
                }
            };

        // 서버 접속 끊길 시 핸들러 
        m_Client.LeaveServerHandler = (ErrorInfo info) =>
        {
            Debug.Log(info.ToString());
            m_state = MyState.Disconnected;
        };

        // RMI 준비 과정
        InitRMI();

        // 클라이언트에 모듈 부착
        m_Client.AttachProxy(m_proxy);
        m_Client.AttachStub(m_stub);
    }

    // Update is called once per frame
    private void Update()
    {
        // 이벤트나 메시지를 수신할 경우 처리해야 함.
        // 이를 하는 함수가 FrameMove()
        m_Client.FrameMove();

        // 연결 해제 처리
        if (m_disconnectNow)
        {
            m_Client.Disconnect(); // 연결 해제
            m_state = MyState.Disconnected; // 연결 상태 변환
            m_disconnectNow = false; // 리셋
        }
    }

    // 로그인 시도
    public void GetLogin(string _id, string _password)
    {
        Debug.Log("GetLogin");

        // 로그인 캔버스에서 로딩 캔버스로 전환
        GameObject.Find("UIManager").GetComponent<SceneChange>().I_LoginToLoading();

        // 아이디 비밀번호 업데이트
        m_userID = _id;
        m_password = _password;

        if (m_state == MyState.Disconnected) // 연결되지 않은상태이면
        {
            var connectParam = new NetConnectionParam(); // 연결은 위한 config 작성
            connectParam.protocolVersion = new Guid();
            connectParam.protocolVersion.Set(Version); // 프로토콜 버전은 서버와 동일하게
            connectParam.serverIP = "localhost"; // 서버 IP
            connectParam.serverPort = ServerPort; // 서버 포트 값

            m_Client.Connect(connectParam); // 연결 시도.
            m_state = MyState.Connecting; // 연결 중으로 변경.

        }
        else
        {
            Debug.Log("서버 연결 오류");
        }
    }

    public void GetRoom(int _character_num)
    {
        // 캐릭터 번호 업데이트
        m_Character = (MyCharacter)_character_num;

        // 서버에 암호화된 메시지를 보냄
        // 입력받은 캐릭터 정보를 보내서 방을 요청 함.
        m_proxy.JoinGameRoom(HostID.HostID_Server,
            RmiContext.SecureReliableSend, (int)m_Character);
    }

    public void LeaveRoom()
    {
        // 서버에 암호화된 메시지를 보냄
        // P2P 방 퇴장함.
        m_proxy.LeaveGameRoom(HostID.HostID_Server,
            RmiContext.SecureReliableSend);

    }

    // 연결 해체를 위한 플래그
    bool m_disconnectNow = false;

    private void InitRMI()
    {
        // "로그인 성공" 원격 함수 호출을 받으면, MyState.Connected로 전환하여 로그인 GUI를 감추자.
        m_stub.NotifyLoginSuccess = (HostID remote, RmiContext rmiContext) =>
        {
            Debug.Log("로그인 성공");

            // 로딩 캔버스에서 인트로 메인 캔버스로
            GameObject.Find("UIManager").GetComponent<SceneChange>().I_LoadingToMain();

            m_state = MyState.Connected;
            return true;
        };

        // "로그인 실패"를 받으면, 실패 사유를 출력하고 연결 해제를 하자.
        m_stub.NotifyLoginFailed = (HostID remote, RmiContext rmiContext, System.String reason) =>
        {
            Debug.Log("로그인 실패");

            // 로딩 캔버스에서 로그인 캔버스로
            GameObject.Find("UIManager").GetComponent<SceneChange>().I_LoadingToLogin();

            Debug.Log(reason);
            m_disconnectNow = true;
            return true;
        };

        // 게임 방 플레이어 입장
        m_stub.Room_Appear = (HostID remote, RmiContext rmiContext, int hostID, System.String user_id, int character_num, System.String team_color, int team_num) =>
        {
            Debug.Log("방 입장");

            Debug.Log(" user_id : " + user_id);
            Debug.Log(" character_num : " + character_num);
            Debug.Log(" team_color : " + team_color);
            Debug.Log(" team_num : " + team_num);


            // 본인이 입장하는 경우
            if (hostID == (int)m_Client.GetLocalHostID())
            {
                // 캐릭터 선택 창에서 게임 방 캔버스로 이동한다.
                GameObject.Find("UIManager").GetComponent<SceneChange>().CharacterToGameRoom();

                // 캐릭터 선택 자식들은 처음 상태로 리셋한다.
                GameObject.Find("UIManager").GetComponent<SceneChange>().C_LoadingToMain();

                // 팀 번호 저장
                m_team_num = team_num;
            }

            // 사파이어 팀인 경우 
            if (team_color == "Sapphire")
            {
                // 플레이어 팀 번호가 1인 경우
                if (team_num == 1)
                {
                    var new_Player_UI = (GameObject)Instantiate(m_Sapphire_PlayerPrefab, UnityEngine.Vector3.zero, Quaternion.identity);
                    new_Player_UI.name = "Team_num/" + team_num.ToString(); // 오브젝트 이름 설정
                    new_Player_UI.transform.SetParent(GameRoom_canvas.transform); // 오브젝트 부모 설정
                    new_Player_UI.transform.localScale = new UnityEngine.Vector3(1, 1, 1); // 오브젝트 스케일 설정
                    new_Player_UI.transform.localPosition = new UnityEngine.Vector3(-300, 80, 0); // 오브젝트 위치 설정
                    new_Player_UI.transform.GetChild(0).GetComponent<Text>().text = user_id; // 아이디 값으로 변경
                    new_Player_UI.transform.GetComponent<Image>().sprite = m_S_character_imgs[character_num - 1]; // 오브젝트 캐릭터 이미지 설정
                }

                // 플레이어 팀 번호가 3인 경우
                if (team_num == 3)
                {
                    var new_Player_UI = (GameObject)Instantiate(m_Sapphire_PlayerPrefab, UnityEngine.Vector3.zero, Quaternion.identity);
                    new_Player_UI.name = "Team_num/" + team_num.ToString(); // 오브젝트 이름 설정
                    new_Player_UI.transform.SetParent(GameRoom_canvas.transform); // 오브젝트 부모 설정
                    new_Player_UI.transform.localScale = new UnityEngine.Vector3(1, 1, 1); // 오브젝트 스케일 설정
                    new_Player_UI.transform.localPosition = new UnityEngine.Vector3(-300, -30, 0); // 오브젝트 위치 설정
                    new_Player_UI.transform.GetChild(0).GetComponent<Text>().text = user_id; // 아이디 값으로 변경
                    new_Player_UI.transform.GetComponent<Image>().sprite = m_S_character_imgs[character_num - 1]; // 오브젝트 캐릭터 이미지 설정
                }
            }

            // 루비 팀인 경우
            if (team_color == "Ruby")
            {
                // 플레이어 팀 번호가 2인 경우
                if (team_num == 2)
                {
                    var new_Player_UI = (GameObject)Instantiate(m_Ruby_PlayerPrefab, UnityEngine.Vector3.zero, Quaternion.identity);
                    new_Player_UI.name = "Team_num/" + team_num.ToString(); // 오브젝트 이름 설정
                    new_Player_UI.transform.SetParent(GameRoom_canvas.transform); // 오브젝트 부모 설정
                    new_Player_UI.transform.localScale = new UnityEngine.Vector3(1, 1, 1); // 오브젝트 스케일 설정
                    new_Player_UI.transform.localPosition = new UnityEngine.Vector3(300, 80, 0); // 오브젝트 위치 설정
                    new_Player_UI.transform.GetChild(0).GetComponent<Text>().text = user_id; // 아이디 값으로 변경
                    new_Player_UI.transform.GetComponent<Image>().sprite = m_R_character_imgs[character_num - 1]; // 오브젝트 캐릭터 이미지 설정
                }

                // 플레이어 팀 번호가 4인 경우
                if (team_num == 4)
                {
                    var new_Player_UI = (GameObject)Instantiate(m_Ruby_PlayerPrefab, UnityEngine.Vector3.zero, Quaternion.identity);
                    new_Player_UI.name = "Team_num/" + team_num.ToString(); // 오브젝트 이름 설정
                    new_Player_UI.transform.SetParent(GameRoom_canvas.transform); // 오브젝트 부모 설정
                    new_Player_UI.transform.localScale = new UnityEngine.Vector3(1, 1, 1); // 오브젝트 스케일 설정
                    new_Player_UI.transform.localPosition = new UnityEngine.Vector3(300, -30, 0); // 오브젝트 위치 설정
                    new_Player_UI.transform.GetChild(0).GetComponent<Text>().text = user_id; // 아이디 값으로 변경
                    new_Player_UI.transform.GetComponent<Image>().sprite = m_R_character_imgs[character_num - 1]; // 오브젝트 캐릭터 이미지 설정
                }
            }
            

            return true;
        };

        // 게임 방 플레이어 퇴장
        m_stub.Room_Disappear = (HostID remote, RmiContext rmiContext, int _team_num) =>
        {
            // 본인의 경우가 아닐 때만, 본인은 클라단에서 이미 지움
            if (_team_num != m_team_num)
            {
                // 해당 플레이어의 UI 삭제
                var ui = GameObject.Find("Team_num/" + _team_num.ToString());
                if (ui != null)
                {
                    Debug.Log("삭제");
                    Destroy(ui);
                }
            }
            return true;
        };
    }

}

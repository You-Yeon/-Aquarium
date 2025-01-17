﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Nettention.Proud;
using UnityEngine.SceneManagement;

public class InitNetManager : MonoBehaviour
{
    // RMI 호출 및 받기 위한 모듈 생성
    S2C2S.Proxy m_proxy = new S2C2S.Proxy();
    S2C2S.Stub m_stub = new S2C2S.Stub();

    // 프로토콜 버전과 서버 포트 번호
    public static System.Guid Version = new System.Guid("{ 0x75204ab8, 0x2e3, 0x4dee, { 0xb5, 0xb1, 0x8e, 0x7e, 0x17, 0x36, 0xfb, 0x48 } }");
    public static ushort ServerPort = 35475;

    // 클라이언트 객체
    public NetClient m_Client;

    // 플레이어의 캐릭터 정보
    public int[] r_chr_num = new int[4];

    // 플레이어의 리스폰 위치
    public float[] r_posX = new float[4];
    public float[] r_posY = new float[4];
    public float[] r_posZ = new float[4];

    // 플레이어의 리스폰 방향
    public float[] r_rotX = new float[4];
    public float[] r_rotY = new float[4];
    public float[] r_rotZ = new float[4];

    // 플레이어의 hostID 값
    public int[] m_hostID = new int[4];

    // 게임 방 유저 UI
    public GameObject GameRoom_canvas;
    
    public GameObject m_Ruby_PlayerPrefab;
    public GameObject m_Sapphire_PlayerPrefab;

    // 방 날씨
    public string m_weather;

    // 캐릭터 이미지
    public Sprite[] m_S_character_imgs = new Sprite[18];
    public Sprite[] m_R_character_imgs = new Sprite[18];

    // 아이디와 비밀번호
    string m_userID;
    string m_password;

    // 팀 번호
    public int m_team_num;

    // hp 최대 값
    public int max_hp;

    // 습도량
    public int m_humidity;

    // 아이템 정보
    public float[] m_items_x = new float[5];
    public float[] m_items_y = new float[5];
    public float[] m_items_z = new float[5];

    // 플레이어 입력을 알려주는 컴포넌트
    public PlayerInput playerInput;

    // 게임 시작하기 전 카운트 다운
    public bool Get_Ready = false;

    // 게임 시작
    public bool Get_Start = false;

    // 임시 게임 방 아이디 저장
    public int temp_room_id;

    // 새로운 플레이어 여부
    public bool new_player = false;
    public int new_Min;
    public int new_Sec;
    public int new_S_score;
    public int new_R_score;

    // 클라이언트 연결 상태
    enum MyState
    {
        Disconnected,
        Connecting,
        Connected
    }
    MyState m_state = MyState.Disconnected;

    // 클라이언트 캐릭터 종류
    public enum MyCharacter
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
                    m_state = MyState.Disconnected;
                }
            };

        // 서버 접속 끊길 시 핸들러 
        m_Client.LeaveServerHandler = (ErrorInfo info) =>
        {
            m_state = MyState.Disconnected;
        };

        // RMI 준비 과정
        InitRMI();

        // 클라이언트에 모듈 부착
        m_Client.AttachProxy(m_proxy);
        m_Client.AttachStub(m_stub);

        // P2P 그룹의 HostID 저장
        m_Client.P2PMemberJoinHandler =
            (HostID memberHostID, HostID groupHostID,
        int memberCount, ByteArray customField) =>
        {
            m_playerP2PGroup = groupHostID;
        };
    }

    float m_lastSendTime = -1;

    // P2P 방 HostID
    public HostID m_playerP2PGroup = HostID.HostID_None;

    // Update is called once per frame
    private void FixedUpdate()
    {
        // 연결 중이고
        if (m_Client != null)
        {
            // P2P 그룹이 생성되었다면
            if (m_Client.GetLocalHostID() != HostID.HostID_None)
            {
                // 그리고 게임이 시작되었다면
                if (Get_Start)
                {
                    // 0.01 sec 간격으로 전송
                    if (m_lastSendTime < 0 || Time.time - m_lastSendTime > 0.01)
                    {

                        // 채팅치고 있을 경우에는 return
                        if (GameObject.Find("Team_num/" + m_team_num).GetComponent<PlayerController>().FocusChat)
                        {
                            return;
                        }

                        var sendOption = new RmiContext();
                        sendOption.reliability = MessageReliability.MessageReliability_Unreliable; // UDP 
                        sendOption.maxDirectP2PMulticastCount = 30; // 트래픽 카운트
                        sendOption.enableLoopback = false;

                        var pc = GameObject.Find("Team_num/" + m_team_num);

                        m_proxy.Player_Move(m_playerP2PGroup, sendOption,
                            m_team_num,
                            playerInput.move,
                            playerInput.rotate,
                            playerInput.reload,
                            pc.transform.position.x,
                            pc.transform.position.y,
                            pc.transform.position.z,
                            pc.transform.rotation.eulerAngles.x,
                            pc.transform.rotation.eulerAngles.y,
                            pc.transform.rotation.eulerAngles.z
                            );

                        m_lastSendTime = Time.time;

                    }
                }
            }

            // 이벤트나 메시지를 수신할 경우 처리해야 함.
            // 이를 하는 함수가 FrameMove()
            m_Client.FrameMove();
        }

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

        // 로그인 캔버스에서 로딩 캔버스로 전환
        GameObject.Find("UIManager").GetComponent<SceneChange>().I_LoginToLoading();

        // 아이디 비밀번호 업데이트
        m_userID = _id;
        m_password = _password;

        m_proxy.RequestLogin(HostID.HostID_Server,
            RmiContext.SecureReliableSend, m_userID, m_password);

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
            //Debug.Log("서버 연결 오류");
        }
    }

    public void GetRoom(int _character_num)
    {
        // 캐릭터 번호 업데이트
        m_Character = (MyCharacter)_character_num;

        // 서버에서 방을 검색한다.
        m_proxy.Get_selete(HostID.HostID_Server, RmiContext.SecureReliableSend);
    }

    public void SetRoom(int _result)
    {
        // 0 : no, 1 : yes
        // NO는 그냥 게임 방 생성
        // YES는 진행 중인 게임에 입장

        if (_result == 0)
        {
            // 캐릭터 선택 자식들은 처음 상태로 리셋한다.
            GameObject.Find("UIManager").GetComponent<SceneChange>().C_SeleteToMain();

            // 메인에서 로딩으로
            GameObject.Find("UIManager").GetComponent<SceneChange>().C_MainToLoading();

            m_proxy.JoinGameRoom(HostID.HostID_Server,
                RmiContext.SecureReliableSend, (int)m_Character);
        }

        if (_result == 1)
        {
            m_proxy.JoinInGame(HostID.HostID_Server,
                RmiContext.SecureReliableSend, (int)m_Character, temp_room_id);
        }
    }

    public void LeaveRoom()
    {
        // 서버에 암호화된 메시지를 보냄
        // P2P 방 퇴장함.
        m_proxy.LeaveGameRoom(HostID.HostID_Server,
            RmiContext.SecureReliableSend);
    }

    public void GetChat(System.String _text)
    {
        var sendOption = new RmiContext();
        sendOption.reliability = MessageReliability.MessageReliability_Reliable; // TCP 
        sendOption.maxDirectP2PMulticastCount = 30; // 트래픽 카운트
        sendOption.enableLoopback = false;

        // P2P 방에 전송
        m_proxy.Player_Chat(m_playerP2PGroup, sendOption, m_userID, _text);

        // 본인 채팅창도 업데이트
        string ChatText = GameObject.Find("ChatText").GetComponent<Text>().text;

        // 채팅 창의 텍스트를 업데이트
        ChatText += "[" + System.DateTime.Now.ToString("HH:mm:ss") + "] " + m_userID + " : " + _text + System.Environment.NewLine;
        GameObject.Find("ChatText").GetComponent<Text>().text = ChatText;

        // 입력 창 비우기
        GameObject.Find("Chat_Input").GetComponent<InputField>().Select();
        GameObject.Find("Chat_Input").GetComponent<InputField>().text = "";

        // 스크롤바의 위치를 가장 아래로 내려준다. 
        // 1.0f는 맨 위, 0.0f는 맨 아래
        GameObject.Find("Chat_Scrollbar").GetComponent<Scrollbar>().value = 0.0f;

    }

    public void GetShoot(float bx, float by, float bz, int kind)
    {
        var sendOption = new RmiContext();
        sendOption.reliability = MessageReliability.MessageReliability_Unreliable; // UDP 
        sendOption.maxDirectP2PMulticastCount = 30; // 트래픽 카운트
        sendOption.enableLoopback = false;

        m_proxy.Player_Shoot(m_playerP2PGroup, sendOption, m_team_num, bx, by, bz, kind);
    }

    public void SetHP(int team_num, int damage)
    {
        // TCP로 체력에 입힌 데미지 전송
        m_proxy.Player_SetHP(HostID.HostID_Server, RmiContext.SecureReliableSend, team_num, damage);
    }

    public void GameStart()
    {
        // TCP로 서버에 게임준비가 완료 되었음을 알린다.
        m_proxy.Player_SetReady(HostID.HostID_Server, RmiContext.SecureReliableSend);

        // 임시 플레이어 컨트롤 잠금
        GameObject.Find("Team_num/" + m_team_num).GetComponent<PlayerController>().Dead = true;

        // 본인의 색깔 변경
        GameObject.Find("Team_num/" + m_team_num).GetComponent<PlayerController>().transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color", new Color32(255, 215, 100, 255));

    }

    public void SetResponse(bool value)
    {
        // TCP로 서버에 플레이어 리스폰 상태를 알린다
        m_proxy.Player_SetResponse(HostID.HostID_Server, RmiContext.SecureReliableSend, m_team_num, value);
    }

    public void GetEnd()
    {
        // TCP로 시간이 종료되었음을 알린다.
        m_proxy.Get_END(HostID.HostID_Server, RmiContext.SecureReliableSend);
    }

    public void GetIntro()
    {
        m_Client.Disconnect(); // 서버와의 연결 해제
        m_state = MyState.Disconnected; // 연결 상태 변환
        m_disconnectNow = false; // 리셋

        SceneManager.LoadScene("Intro"); // 인트로씬으로 이동

        Destroy(gameObject); // 본인 네트워크 삭제
    }

    // 연결 해체를 위한 플래그
    bool m_disconnectNow = false;

    private void InitRMI()
    {
        // "로그인 성공" 원격 함수 호출을 받으면, MyState.Connected로 전환하여 로그인 GUI를 감추자.
        m_stub.NotifyLoginSuccess = (HostID remote, RmiContext rmiContext) =>
        {

            // 로딩 캔버스에서 인트로 메인 캔버스로
            GameObject.Find("UIManager").GetComponent<SceneChange>().I_LoadingToMain();

            m_state = MyState.Connected;
            return true;
        };

        // "로그인 실패"를 받으면, 실패 사유를 출력하고 연결 해제를 하자.
        m_stub.NotifyLoginFailed = (HostID remote, RmiContext rmiContext, System.String reason) =>
        {

            // 로딩 캔버스에서 로그인 캔버스로
            GameObject.Find("UIManager").GetComponent<SceneChange>().I_LoadingToLogin();

            //m_disconnectnow = true;
            return true;
        };

        // 방 검색 결과
        m_stub.Set_selete = (HostID remote, RmiContext rmiContext, int _result, int _Room_id, int _Min, int _Sec) =>
        {
            // 입장할 조건이 되는 방이 없는 경우
            if (_result == 0)
            {
                // 서버에 암호화된 메시지를 보냄
                // 입력받은 캐릭터 정보를 보내서 방을 요청 함.
                m_proxy.JoinGameRoom(HostID.HostID_Server,
                    RmiContext.SecureReliableSend, (int)m_Character);
            }

            // 5분 이상 남은 게임 방이 존재할 경우
            if (_result == 1)
            {
                // 플레이어에게 선택권을 부여한다.

                // 방 아이디 임시 저장
                temp_room_id = _Room_id;

                // 캐릭터 선택 창에서 방 선택 캔버스로 이동한다.
                GameObject.Find("UIManager").GetComponent<SceneChange>().C_LoadingToSelete(_Min, _Sec);

            }

            return true;
        };

        // 게임 방 플레이어 입장
        m_stub.Room_Appear = (HostID remote, RmiContext rmiContext, int hostID, System.String user_id, int character_num, System.String team_color, int team_num) =>
        {


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
                    Destroy(ui);
                }

                // 게임이 아직 시작하지 않았는데
                if (!Get_Start)
                {
                    // 만약 카운트 중이였다면 종료 
                    if (Get_Ready)
                    {
                        // 카운트 종료
                        Get_Ready = false;

                        // 카운트 캔버스 종료
                        GameObject.Find("UIManager").GetComponent<SceneChange>().OffCountcanvas();
                    }
                }
            }
            return true;
        };

        // 진행 중인 게임 입장
        m_stub.Game_Appear = (HostID remote, RmiContext rmiContext, int value, int _team_num, int _min, int _sec, int _S_score, int _R_score) =>
        {
            // 본인이 입장하는 경우
            if (value == 1)
            {
                // 팀 번호 저장
                m_team_num = _team_num;

                // 자신이 새로운 플레이어임을 알림.
                new_player = true;

                // 게임 시작
                Get_Start = true;

                // 시간 업데이트
                new_Min = _min;
                new_Sec = _sec;

                // 팀 점수 업데이트
                new_S_score = _S_score;
                new_R_score = _R_score;

                // 캐릭터 선택 자식들은 처음 상태로 리셋한다.
                GameObject.Find("UIManager").GetComponent<SceneChange>().C_SeleteToMain();

                // 캐릭터 선택 창에서 게임 선택을 위해 카운트를 킨다.
                GameObject.Find("UIManager").GetComponent<SceneChange>().OnCountcanvas();

                // 자신이 새로 생성됨을 알림
                var sendOption = new RmiContext();
                sendOption.reliability = MessageReliability.MessageReliability_Reliable; // TCP 
                sendOption.maxDirectP2PMulticastCount = 30; // 트래픽 카운트  
                sendOption.enableLoopback = false;

                m_proxy.Game_Appear(m_playerP2PGroup, sendOption, 0, _team_num, 0, 0, 0, 0);

            }
            else // 다른 사람의 경우
            {
                // 플레이어 생성
                GameObject.Find("GameManager").GetComponent<GameManager>().GetNewPlayer(_team_num);
            }
            


            return true;
        };

        // 게임 카운트 시작
        m_stub.CountStart = (HostID remote, RmiContext rmiContext) =>
        {
            // 카운트 시작
            Get_Ready = true;

            // 카운트 캔버스 시작
            GameObject.Find("UIManager").GetComponent<SceneChange>().OnCountcanvas();

            return true;
        };

        // 플레이어 리스폰 설정
        m_stub.PlayerInfo = (HostID remote, RmiContext rmiContext, int _player_num, int _chr_num, float _px, float _py, float _pz, float _rx, float _ry, float _rz) =>
        {

            // 플레이어의 정보 세팅
            r_chr_num[_player_num - 1] = _chr_num;

            r_posX[_player_num - 1] = _px;
            r_posY[_player_num - 1] = _py;
            r_posZ[_player_num - 1] = _pz;

            r_rotX[_player_num - 1] = _rx;
            r_rotY[_player_num - 1] = _ry;
            r_rotZ[_player_num - 1] = _rz;

            return true;
        };

        // 게임 시작
        m_stub.GameStart = (HostID remote, RmiContext rmiContext) =>
        {

            // 타이머 시작
            GameObject.Find("Time_text").GetComponent<timer>().TimerStart();

            // 무적 5초 후 해제
            GameObject.Find("Team_num/" + m_team_num).GetComponent<ResponsePlayer>().FirstResponse();

            var sendOption = new RmiContext();
            sendOption.reliability = MessageReliability.MessageReliability_Reliable; // TCP 
            sendOption.maxDirectP2PMulticastCount = 30; // 트래픽 카운트
            sendOption.enableLoopback = false;

            // P2P 방에 무적 상태 전송
            m_proxy.Player_SetResponse(m_playerP2PGroup, sendOption, m_team_num, true);

            return true;
        };

        // 플레이어 동기화
        m_stub.Player_Move = (HostID remote, RmiContext rmiContext, int _team_num, float _move, float _rotate, bool _reload, float _px, float _py, float _pz, float _rx, float _ry, float _rz) =>
        {
            // 플레이어 정보 갱신
            var control = GameObject.Find("Team_num/" + _team_num).GetComponent<OthersController>();

            control.m_move = _move;
            control.m_rotate = _rotate;
            control.m_reload = _reload;

            control.m_px = _px;
            control.m_py = _py;
            control.m_pz = _pz;

            control.m_rx = _rx;
            control.m_ry = _ry;
            control.m_rz = _rz;

            return true;
        };
    
        // 플레이어 채팅
        m_stub.Player_Chat = (HostID remote, RmiContext rmiContext, System.String _id, System.String _text) =>
        {

            string ChatText = GameObject.Find("ChatText").GetComponent<Text>().text;

            // 채팅 창의 텍스트를 업데이트
            ChatText += "[" + System.DateTime.Now.ToString("HH:mm:ss") + "] " + _id + " : " + _text + System.Environment.NewLine;
            GameObject.Find("ChatText").GetComponent<Text>().text = ChatText;

            // 스크롤바의 위치를 가장 아래로 내려준다. 
            // 1.0f는 맨 위, 0.0f는 맨 아래
            GameObject.Find("Chat_Scrollbar").GetComponent<Scrollbar>().value = 0.0f;

            return true;
        };

        // 플레이어 총알
        m_stub.Player_Shoot = (HostID remote, RmiContext rmiContext, int _team_num, float _bx, float _by, float _bz, int kind) =>
        {
            // 레이캐스트 위치 전송 후 총알 발사 시작
            var control = GameObject.Find("Team_num/" + _team_num).GetComponent<OthersController>();
            control.fire(_bx, _by, _bz, kind);

            return true;
        };

        // 플레이어의 체력
        m_stub.Player_GetHP = (HostID remote, RmiContext rmiContext, int _m_humidity) =>
        {
            // 체력 초기화
            m_humidity = _m_humidity;

            // 본인의 색깔 변경
            GameObject.Find("Team_num/" + m_team_num).GetComponent<PlayerController>().playerRenderer.material.SetColor("_Color", new Color32((byte)(255 - ((float)m_humidity / max_hp) * 155.0), 255, 255, 255));

            // P2P로 체력을 전송하여 다른 사람에게도 본인의 색깔 변경
            var sendOption = new RmiContext();
            sendOption.reliability = MessageReliability.MessageReliability_Reliable; // TCP 
            sendOption.maxDirectP2PMulticastCount = 30; // 트래픽 카운트  
            sendOption.enableLoopback = false;

            m_proxy.Show_Player_Color(m_playerP2PGroup, sendOption, m_team_num, m_humidity, max_hp);

            return true;
        };

        // 플레이어 색깔 변경
        m_stub.Show_Player_Color = (HostID remote, RmiContext rmiContext, int _m_team_num, int _m_humidity, float _max_hp) =>
        {
            // 플레이어의  색깔 변경
            GameObject.Find("Team_num/" + _m_team_num).GetComponent<OthersController>().playerRenderer.material.SetColor("_Color", new Color32((byte)(255 - ((float)_m_humidity / _max_hp) * 155.0), 255, 255, 255));

            return true;
        };

        // 방 날씨 변경
        m_stub.Room_weather = (HostID remote, RmiContext rmiContext, System.String _weather) =>
        {

            // 날씨 가져오기
            m_weather = _weather;

            return true;
        };

        // 방 날씨 변경
        m_stub.Room_Item = (HostID remote, RmiContext rmiContext, int _idx, float _ix, float _iy, float _iz) =>
        {
            // 아이템 위치 세팅
            m_items_x[_idx] = _ix;
            m_items_y[_idx] = _iy;
            m_items_z[_idx] = _iz;

            return true;
        };

        // 다른 플레이어의 처음 리스폰 상태 업데이트
        m_stub.Player_SetResponse = (HostID remote, RmiContext rmiContext, int _m_team_num, bool _value) =>
        {
            if (_value)
            {
                // 임시 플레이어 컨트롤 잠금
                GameObject.Find("Team_num/" + _m_team_num).GetComponent<OthersController>().Dead = true;

                // 플레이어의 색깔 변경
                GameObject.Find("Team_num/" + _m_team_num).GetComponent<OthersController>().transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color", new Color32(255, 215, 100, 255));

                // 무적 5초 후 해제
                GameObject.Find("Team_num/" + _m_team_num).GetComponent<ResponseOtherPlayer>().FirstResponse();
            }

            return true;
        };

        // 플레이어 죽음..
        m_stub.Player_Kill = (HostID remote, RmiContext rmiContext, int _m_team_num) =>
        {
            // 본인의 경우
            if (m_team_num == _m_team_num)
            {
                // 체력 전부 채우기
                m_humidity = max_hp;

                // 본인의 색깔 변경
                GameObject.Find("Team_num/" + m_team_num).GetComponent<PlayerController>().playerRenderer.material.SetColor("_Color", new Color32(100, 255, 255, 255));

                // 죽음 여부 on ( 플레이어 컨트롤 잠금 )
                GameObject.Find("Team_num/" + m_team_num).GetComponent<PlayerController>().Dead = true;

                // 애니메이션 바꾸기
                GameObject.Find("Team_num/" + m_team_num).GetComponent<PlayerController>().playerAnimator.runtimeAnimatorController = GameObject.Find("Team_num/" + m_team_num).GetComponent<PlayerController>().dead_controller;

                // P2P로 자신이 죽었음을 알림.
                var sendOption = new RmiContext();
                sendOption.reliability = MessageReliability.MessageReliability_Reliable; // TCP 
                sendOption.maxDirectP2PMulticastCount = 30; // 트래픽 카운트  
                sendOption.enableLoopback = false;

                m_proxy.Player_Kill(m_playerP2PGroup, sendOption, m_team_num);

                // 리스폰 처리
                GameObject.Find("Team_num/" + m_team_num).GetComponent<ResponsePlayer>().AfterResponse();

                // 플레이어 팀 점수 갱신
                if (m_team_num % 2 == 0) // 루비
                {
                    // 루비팀이 죽었으므로 사파이어팀 점수 갱신
                    int score = int.Parse(GameObject.Find("Sapphire_Score_text").GetComponent<Text>().text);

                    score++;

                    if (score < 10) // 점수가 10보다 작은 경우
                    {
                        GameObject.Find("Sapphire_Score_text").GetComponent<Text>().text = "0" + score.ToString();
                    }
                    else // 큰 경우
                    {
                        GameObject.Find("Sapphire_Score_text").GetComponent<Text>().text = score.ToString();
                    }

                }

                if (m_team_num % 2 == 1) // 사파이어
                {
                    // 사파이어팀이 죽었으므로 루비팀 점수 갱신
                    int score = int.Parse(GameObject.Find("Ruby_Score_text").GetComponent<Text>().text);

                    score++;

                    if (score < 10) // 점수가 10보다 작은 경우
                    {
                        GameObject.Find("Ruby_Score_text").GetComponent<Text>().text = "0" + score.ToString();
                    }
                    else // 큰 경우
                    {
                        GameObject.Find("Ruby_Score_text").GetComponent<Text>().text = score.ToString();
                    }

                }

            }
            // 다른 플레이어의 경우
            else
            {
                // 플레이어의 색깔 변경
                GameObject.Find("Team_num/" + _m_team_num).GetComponent<OthersController>().playerRenderer.material.SetColor("_Color", new Color32(100, 255, 255, 255));

                // 죽음 여부 on ( 플레이어 컨트롤 잠금 )
                GameObject.Find("Team_num/" + _m_team_num).GetComponent<OthersController>().Dead = true;

                // 애니메이션 바꾸기
                GameObject.Find("Team_num/" + _m_team_num).GetComponent<OthersController>().playerAnimator.runtimeAnimatorController = GameObject.Find("Team_num/" + _m_team_num).GetComponent<OthersController>().dead_controller;

                // 리스폰 처리
                GameObject.Find("Team_num/" + _m_team_num).GetComponent<ResponseOtherPlayer>().AfterResponse(_m_team_num);

                // 플레이어 팀 점수 갱신
                if (_m_team_num % 2 == 0) // 루비
                {
                    // 루비팀이 죽었으므로 사파이어팀 점수 갱신
                    int score = int.Parse(GameObject.Find("Sapphire_Score_text").GetComponent<Text>().text);

                    score++;

                    if (score < 10) // 점수가 10보다 작은 경우
                    {
                        GameObject.Find("Sapphire_Score_text").GetComponent<Text>().text = "0" + score.ToString();
                    }
                    else // 큰 경우
                    {
                        GameObject.Find("Sapphire_Score_text").GetComponent<Text>().text = score.ToString();
                    }

                }

                if (_m_team_num % 2 == 1) // 사파이어
                {
                    // 사파이어팀이 죽었으므로 루비팀 점수 갱신
                    int score = int.Parse(GameObject.Find("Ruby_Score_text").GetComponent<Text>().text);

                    score++;

                    if (score < 10) // 점수가 10보다 작은 경우
                    {
                        GameObject.Find("Ruby_Score_text").GetComponent<Text>().text = "0" + score.ToString();
                    }
                    else // 큰 경우
                    {
                        GameObject.Find("Ruby_Score_text").GetComponent<Text>().text = score.ToString();
                    }

                }
            }

            // 죽으면 무적하고 플레이어 컨트롤 막고 애니메이터를 바꿔서 죽음 표시하기, 서버에서 무적 만듬.
            // 3초 뒤에 플레이어의 chlid 0번과 1번의 Raygun, 2번을 setActive false 처리하기.
            // 애니메이터 원 위치로 바꾸고, 플레이어 인포 작성. 노란색으로 변경
            // 2초 뒤에 리스폰 장소로 옮기고 setActive true 처리하기, 죽음 false
            // 5초 뒤 리스폰 해제처리. 무적 해제
            // Player_GetHP 처리

            return true;
        };

        // 게임 종료
        m_stub.Set_END = (HostID remote, RmiContext rmiContext, string _Team) =>
        {

            // 플레이어 컨트롤 잠금
            GameObject.Find("Team_num/" + m_team_num).GetComponent<PlayerController>().Dead = true;

            // 종료 캔버스를 활성화
            GameObject.Find("UI_canvas").transform.Find("END_canvas").gameObject.SetActive(true);


            if (_Team == "Sapphire") // 사파이어 승리
            {
                GameObject.Find("UI_canvas").transform.Find("END_canvas").transform.Find("Sapphire").gameObject.SetActive(true);
            }
            else if (_Team == "Ruby") // 루비 승리
            {
                GameObject.Find("UI_canvas").transform.Find("END_canvas").transform.Find("Ruby").gameObject.SetActive(true);
            }
            else if (_Team == "Draw") // 무승부
            {
                GameObject.Find("UI_canvas").transform.Find("END_canvas").transform.Find("Draw").gameObject.SetActive(true);
            }

            return true;
        };
    }

}
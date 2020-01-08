using UnityEngine;
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
    NetClient m_netClient;

    // 아이디와 비밀번호
    string m_userID;
    string m_password;

    enum MyState
    {
        Disconnected,
        Connecting,
        Connected,
    }
    MyState m_state = MyState.Disconnected;

    enum MyCharacter
    {
    }


    // Start is called before the first frame update
    private void Start()
    {
        // 클라이언트 객체 생성
        m_netClient = new NetClient();

        // 서버 접속 시도 결과 핸들러
        m_netClient.JoinServerCompleteHandler =
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
        m_netClient.LeaveServerHandler = (ErrorInfo info) =>
        {
            Debug.Log(info.ToString());
            m_state = MyState.Disconnected;
        };

        // RMI 준비 과정
        InitRMI();

        // 클라이언트에 모듈 부착
        m_netClient.AttachProxy(m_proxy);
        m_netClient.AttachStub(m_stub);
    }

    // Update is called once per frame
    private void Update()
    {
        // 이벤트나 메시지를 수신할 경우 처리해야 함.
        // 이를 하는 함수가 FrameMove()
        m_netClient.FrameMove();

        // 연결 해제 처리
        if (m_disconnectNow)
        {
            m_netClient.Disconnect(); // 연결 해제
            m_state = MyState.Disconnected; // 연결 상태 변환
            m_disconnectNow = false; // 리셋
        }
    }

    public void GetLogin(string _id, string _password)
    {
        // 로그인 캔버스에서 로딩 캔버스로 전환
        GameObject.Find("UIManager").GetComponent<SceneChange>().LoginToLoading();

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

            m_netClient.Connect(connectParam); // 연결 시도.
            m_state = MyState.Connecting; // 연결 중으로 변경.

        }
        else
        {
            Debug.Log("서버 연결 오류");
        }
    }

    // 연결 해체를 위한 플래그
    bool m_disconnectNow = false;

    private void InitRMI()
    {
        // "로그인 성공" 원격 함수 호출을 받으면, MyState.Connected로 전환하여 로그인 GUI를 감추자.
        m_stub.NotifyLoginSuccess = (HostID remote, RmiContext rmiContext) =>
        {
            // 로딩 캔버스에서 인트로 메인 캔버스로
            GameObject.Find("UIManager").GetComponent<SceneChange>().LoadingToMain();

            m_state = MyState.Connected;
            return true;
        };

        // "로그인 실패"를 받으면, 실패 사유를 출력하고 연결 해제를 하자.
        m_stub.NotifyLoginFailed = (HostID remote, RmiContext rmiContext, System.String reason) =>
        {
            // 로딩 캔버스에서 로그인 캔버스로
            GameObject.Find("UIManager").GetComponent<SceneChange>().LoadingToLogin();

            Debug.Log(reason);
            m_disconnectNow = true;
            return true;
        };
    }

}

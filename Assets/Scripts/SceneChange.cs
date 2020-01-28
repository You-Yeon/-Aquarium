using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public GameObject Intro_canvas; // 인트로 캔버스
    public GameObject Character_canvas; // 캐릭터 캔버스
    public GameObject GameRoom_canvas; // 게임 방 캔버스

    // * 인트로 자식 캔버스
    public GameObject I_Login_canvas; // 로그인 캔버스
    public GameObject I_Loading_canvas; // 로딩 캔버스
    public GameObject I_Main_canvas; // 인트로 메인 캔버스

    // * 캐릭터 자식 캔버스
    public GameObject C_Loading_canvas; // 로딩 캔버스
    public GameObject C_Main_canvas; // 메인 캔버스
    public GameObject C_Selete_canvas; // 선택 캔버스

    // * 게임 방 자식 캔버스
    public GameObject G_Count_canvas; // 카운트 캔버스

    public void ChangeGameScene()
    {
        SceneManager.LoadScene("Main"); // 게임씬으로 이동
    }

    public void ExitGame()
    {
        Application.Quit();  // 게임 종료
    }

    public void IntroToCharacter()
    {
        Intro_canvas.SetActive(false); // 인트로 캔버스 off
        Character_canvas.SetActive(true);  // 캐릭터 캔버스 on
    }

    public void CharacterToIntro()
    {
        Intro_canvas.SetActive(true); // 인트로 캔버스 on
        Character_canvas.SetActive(false);  // 캐릭터 캔버스 off
    }

    public void GameRoomToIntro()
    {
        // 플레이어의 UI들
        GameObject[] objects = GameObject.FindGameObjectsWithTag("User_UI");
        for (int i = 0; i < objects.Length; i++)
        {
            Debug.Log("삭제");
            Destroy(objects[i]);
        }

        Intro_canvas.SetActive(true); // 인트로 캔버스 on
        GameRoom_canvas.SetActive(false);  // 게임 방 캔버스 off
    }

    public void CharacterToGameRoom()
    {
        GameRoom_canvas.SetActive(true); // 게임 방 캔버스 on
        Character_canvas.SetActive(false);  // 캐릭터 캔버스 off
    }

    public void GameRoomToCharacter()
    {
        GameRoom_canvas.SetActive(false); // 게임 방 캔버스 off
        Character_canvas.SetActive(true);  // 캐릭터 캔버스 on
    }

    // * 인트로 자식 캔버스

    public void I_LoginToLoading()
    {
        I_Login_canvas.SetActive(false); // 로그인 캔버스 off
        I_Loading_canvas.SetActive(true); // 로딩 캔버스 on
    }

    public void I_LoadingToLogin()
    {
        I_Login_canvas.SetActive(true); // 로그인 캔버스 on
        I_Loading_canvas.SetActive(false); // 로딩 캔버스 off
    }

    public void I_LoadingToMain()
    {
        I_Main_canvas.SetActive(true); // 인트로 메인 캔버스 on
        I_Loading_canvas.SetActive(false); // 로딩 캔버스 off
    }

    // * 캐릭터 자식 캔버스

    public void C_MainToLoading()
    {
        C_Loading_canvas.SetActive(true); // 로딩 캔버스 on
        C_Main_canvas.SetActive(false); // 메인 캔버스 off
    }

    public void C_LoadingToMain()
    {
        C_Loading_canvas.SetActive(false); // 로딩 캔버스 off
        C_Main_canvas.SetActive(true); // 메인 캔버스 on
    }

    public void C_LoadingToSelete(int Min, int Sec)
    {
        C_Loading_canvas.SetActive(false); // 로딩 캔버스 off
        C_Selete_canvas.SetActive(true); // 선택 캔버스 on
        C_Main_canvas.SetActive(true); // 메인 캔버스 on

        // 캔버스 타이머 시작
        C_Selete_canvas.transform.GetChild(2).GetComponent<canvas_timer>().timer(Min, Sec);
    }

    public void C_SeleteToMain()
    {
        C_Selete_canvas.SetActive(false); // 선택 캔버스 off
    }

    // * 게임 방 자식 캔버스

    public void OnCountcanvas()
    {
        G_Count_canvas.SetActive(true); // 카운트 캔버스 on
    }

    public void OffCountcanvas()
    {
        G_Count_canvas.SetActive(false); // 카운트 캔버스 off
    }
}

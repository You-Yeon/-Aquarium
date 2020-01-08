using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public GameObject Intro_canvas; // 인트로 캔버스
    public GameObject Character_canvas; // 캐릭터 캔버스

    // * 인트로 자식 캔버스
    public GameObject Login_canvas; // 로그인 캔버스
    public GameObject Loading_canvas; // 로딩 캔버스
    public GameObject Main_canvas; // 인트로 메인 캔버스

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

    public void LoginToLoading()
    {
        Login_canvas.SetActive(false); // 로그인 캔버스 off
        Loading_canvas.SetActive(true); // 로딩 캔버스 on
    }

    public void LoadingToLogin()
    {
        Login_canvas.SetActive(true); // 로그인 캔버스 on
        Loading_canvas.SetActive(false); // 로딩 캔버스 off
    }

    public void LoadingToMain()
    {
        Main_canvas.SetActive(true); // 인트로 메인 캔버스 on
        Loading_canvas.SetActive(false); // 로딩 캔버스 off
    }
}

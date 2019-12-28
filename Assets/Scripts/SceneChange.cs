using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public GameObject Intro_canvas;
    public GameObject Character_canvas;

    public void ChangeGameScene()
    {
        SceneManager.LoadScene("Main"); // 게임씬으로 이동
    }

    public void ExitGame()
    {
        Application.Quit();  // 게임 종료
    }

    public void SetCanvas()
    {
        Intro_canvas.SetActive(false); // 인트로 캔버스 off
        Character_canvas.SetActive(true);  // 캐릭터 캔버스 on
    }
}

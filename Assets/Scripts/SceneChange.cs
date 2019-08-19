using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public void ChangeGameScene()
    {
        SceneManager.LoadScene("Main"); // 게임씬으로 이동
    }

    public void ExitGame()
    {
        Application.Quit();  // 게임 종료
    }
}

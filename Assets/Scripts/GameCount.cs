using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCount : MonoBehaviour
{
    // game count
    private float TimeLeft = 1.0f;
    private float nextTime = 0.0f;
    private int img_index = 0;

    public Sprite[] m_Count_img = new Sprite[4];


    void OnEnable()
    {
        img_index = 0;
    }

    void Start()
    {
        // 다음 씬을 로드한다.
        StartCoroutine(LoadScene("Main"));
    }

    void Update()
    {
        //1.0초 마다 실행
        if (Time.time > nextTime)
        {
            // 카운트 끝나면 게임 넘기기
            if (img_index == 4)
            {                
                // 씬 시작
                GameObject.Find("UIManager").GetComponent<SceneChange>().ChangeGameScene();

                // 게임 시작 여부 
                GameObject.Find("NetManager").GetComponent<InitNetManager>().Get_Start = true;
            }
            else
            {
                // 이미지 변경
                nextTime = Time.time + TimeLeft;
                transform.GetComponent<Image>().sprite = m_Count_img[img_index]; // 오브젝트 캐릭터 이미지 설정
                img_index++;
            } 
        }
    }

    IEnumerator LoadScene(string sceneName)
    {
        // 씬을 로드
        AsyncOperation asyncOper = SceneManager.LoadSceneAsync(sceneName);

        // 모든 데이터를 가져와도 바로 장면을 활성화하지 않는다.
        asyncOper.allowSceneActivation = false;

        while (!asyncOper.isDone)
        {
            yield return null;
            //Debug.Log(asyncOper.progress);
        }
    }
}

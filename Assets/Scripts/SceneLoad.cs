using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


/*
     제공하는 변수들
     isDone: 작업의 완료유무를 boolean 값으로 반환 
     progress: 진행정도를 float 형 0, 1을 반환 (0 - 진행중, 1 - 진행완료)
     allowSceneActivation : true면 로딩이 완료되면 바로 scene을 넘기고 false면 0.9f에서 멈춤 
*/
public class SceneLoad : MonoBehaviour
{

    [SerializeField] private Slider progressbar;
    [SerializeField] private TextMeshProUGUI loadText;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync("Stage01");
        operation.allowSceneActivation = false;         // 로딩이 다 끝나도 멈춤 

        // 로딩이 다 끝나서 isDone 변수가 true가 될때 까지 반복 
        while (!operation.isDone)
        {
            yield return null;
            if (progressbar.value < 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime);
            }
            else if (operation.progress >= 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
            }

            if (progressbar.value >= 1f)
            {
                loadText.text = "Please Press Enter Key";
            }

            // 모바일에서 화면을 터치하면 넘어감 
            if (Input.GetKeyDown(KeyCode.Return) && progressbar.value >= 1f && operation.progress >= 0.9f)
            {
                Debug.Log("다음 창으로 넘어가버려!!");
                operation.allowSceneActivation = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// 메인 화면 - 버튼 이벤트용 스크립트 
public class ButtonEvent : MonoBehaviour
{
    public void SceneLoader(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

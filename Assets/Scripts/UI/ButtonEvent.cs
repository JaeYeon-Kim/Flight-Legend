using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// 버튼 이벤트용 스크립트 
public class ButtonEvent : MonoBehaviour
{
    public void SceneLoader(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}

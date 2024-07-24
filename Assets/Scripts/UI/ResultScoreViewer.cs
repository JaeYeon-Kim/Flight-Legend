using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


// 결과 점수를 보여주는 스크립트 
public class ResultScoreViewer : MonoBehaviour
{
    private TextMeshProUGUI textResultScore;

    private void Awake() {
        textResultScore = GetComponent<TextMeshProUGUI>();
        // Stage에서 저장한 점수를 불러와서 score 변수에 저장 
        int score = PlayerPrefs.GetInt("Score");
        // textResultScore UI에 점수 갱신 
        textResultScore.text = "Result Score " + score;
    }
}

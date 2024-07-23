using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScoreViewer : MonoBehaviour
{

    [SerializeField]
    private PlayerController playerController;
    private TextMeshProUGUI textScore;

    private void Awake() {
        textScore = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // Text UI에 점수 정보 업데이트 
        textScore.text = "Score " + playerController.Score;
    }
}

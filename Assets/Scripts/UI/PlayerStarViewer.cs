using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


// 플레이어의 별 갯수를 가져오는 뷰어 
public class PlayerStarViewer : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI starCountText;
    [SerializeField] private PlayerData playerData;

    // Update is called once per frame
    void Update()
    {
        starCountText.text = playerData.StarCount.ToString();
    }
}

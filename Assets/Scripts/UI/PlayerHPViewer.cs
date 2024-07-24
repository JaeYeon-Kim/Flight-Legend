using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 플레이어 체력을 UI로 표현해주는 스크립트 
public class PlayerHPViewer : MonoBehaviour
{
    [SerializeField]
    private PlayerHP playerHP;
    private Slider sliderHP;

    private void Awake() {
        sliderHP = GetComponent<Slider>();
    }

    // 이 방법으 사용하거나 이벤트를 사용해 체력 정보가 바뀔때만 UI 정보 갱신 
    void Update()
    {
        // Slider UI에 현재 체력 정보를 업데이트 
        sliderHP.value = playerHP.CurrentHP / playerHP.MaxHP;
    }
}

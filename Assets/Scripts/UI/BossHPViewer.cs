using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 보스 체력 UI 스크립트 
public class BossHPViewer : MonoBehaviour
{
    [SerializeField]
    private BossData bossData;
    private Slider sliderHP;

    private void Awake() {
        sliderHP = GetComponent<Slider>();
    }


    private void Update() {
        sliderHP.value = bossData.CurrentHP / bossData.MaxHP;
    }
}

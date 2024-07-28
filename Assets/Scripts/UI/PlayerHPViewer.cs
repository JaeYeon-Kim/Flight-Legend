using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 플레이어 체력을 UI로 표현해주는 스크립트 
public class PlayerHPViewer : MonoBehaviour
{
    [SerializeField] private Vector3 distance = Vector3.down * 10.0f;
    private Transform targetTransform;
    private RectTransform rectTransform;

    private PlayerData playerData;
    private Slider hpSlider;

    public void Setup(Transform target, PlayerData playerData)
    {
        this.playerData = playerData;
        hpSlider = GetComponent<Slider>();
        // Slider UI가 따라다닐 target을 설정해줌
        targetTransform = target;

        // RectTransform 컴포넌트 정보 받아오기
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // 비율로 넣어서 정의해줌 
        hpSlider.value = playerData.CurrentHP / playerData.MaxHP;
    }


    private void LateUpdate()
    {
        // 플레이어가 죽었을 경우 따라다닐 target이 사라지면 Slider UI 삭제 
        if (targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        // 플레이어의 위치를 중심으로 체력바의 위치를 갱신해야하므로 LateUpdate에서 진행 
        // 오브젝트의 월드 좌표를 기준으로 화면에서의 좌표 값을 구하고 그 좌표로 부터 일정 거리 만큼 떨어진 위치에 HP UI 설정 
        Vector3 hpBarPosition = Camera.main.WorldToScreenPoint(targetTransform.position);
        rectTransform.position = hpBarPosition + distance;
    }
}

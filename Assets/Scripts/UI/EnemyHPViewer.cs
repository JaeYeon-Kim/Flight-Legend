using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 적체력바가 적 기체에 붙어있게 하기 위한 스크립트 
public class EnemyHPViewer : MonoBehaviour
{
    [SerializeField] private Vector3 distance = Vector3.up * 20.0f;
    private Transform targetTransform;
    private RectTransform rectTransform;

    // 적 체력 정보와 표시할 Slider
    private EnemyHP enemyHP;
    private Slider hpSlider;

    public void Setup(Transform target, EnemyHP enemyHP)
    {
        this.enemyHP = enemyHP;
        hpSlider = GetComponent<Slider>();
        // Slider UI가 따라다닐 target 설정
        targetTransform = target;
        // RectTransform 컴포넌트 정보 받아오기 
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update() {
        // 체력바 초기값 설정 
        hpSlider.value = enemyHP.CurrentHP / enemyHP.MaxHP;
    }

    private void LateUpdate()
    {
        // 적이 파괴되어 따라다닐 target이 사라지면 Slider UI 삭제 
        if (targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        // 적 오브젝트의 위치 갱신 후에 체력바 위치 갱신을 위해 LateUpdate에서 실행 
        // 오브젝트의 월드 좌표를 기준으로 화면에서의 좌표 값을 구하고 그 좌표로 부터 일정 거리 만큼 떨어지 위치에 HP UI 설정
        Vector3 hpBarPosition = Camera.main.WorldToScreenPoint(targetTransform.position);
        rectTransform.position = hpBarPosition + distance;
    }
}

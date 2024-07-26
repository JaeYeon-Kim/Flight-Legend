using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 보스의 여러 정보 들을 담고있는 데이터용 클래스 
public class BossData : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 50; // 최대 체력
    private float currentHP;    // 현재 체력
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        currentHP = maxHP;      // 현재 체력을 최대 체력과 같게 설정 
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}

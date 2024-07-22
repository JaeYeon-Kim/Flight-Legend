using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 플레이어 체력 관리 스크립트 
public class PlayerHP : MonoBehaviour
{

    [SerializeField]
    private float maxHp = 10;       // 최대 체력 
    private float currentHP; // 현재 체력 
    private SpriteRenderer spriteRenderer;

    public float MaxHP => maxHp;            // maxHP 변수에 접근할 수 있는 프로퍼티(Get만 가능)
    public float CurrentHP => currentHP;    // currentHP 변수에 접근할 수 잇는 프로퍼티 (Get만 가능);
    private void Awake()
    {
        currentHP = maxHp;      // 현재 체력을 최대 체력으로 초기에 설정 
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        // 현재 체력을 damage 만큼 감소 
        currentHP -= damage;

        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        // 체력이 0이하 = 플레이어 캐릭터 사망 
        if (currentHP <= 0)
        {
            Debug.Log("PlayerHP : 0..Die");
        }
    }


    // 플레이어가 체력을 입었을 경우 색상을 변경 시켜주는 코루틴 선언 
    IEnumerator HitColorAnimation()
    {
        // 색상 빨간색으로
        spriteRenderer.color = Color.red;

        // 0.1초간 대기 
        yield return new WaitForSeconds(0.1f);

        // 플레이어의 색상을 원래 색상인 하얀색으로 
        spriteRenderer.color = Color.white;
    }
}

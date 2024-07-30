using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 보스의 여러 정보 들을 담고있는 데이터용 클래스 
public class BossData : MonoBehaviour
{
    [SerializeField] private float maxHP = 50; // 최대 체력
    private float currentHP;    // 현재 체력
    private SpriteRenderer spriteRenderer;

    private Boss boss;      // Boss 객체 

    // 보스 체력 프로퍼티 
    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP = maxHP;      // 현재 체력을 최대 체력과 같게 설정 
        spriteRenderer = GetComponent<SpriteRenderer>();
        boss = GetComponent<Boss>();
    }


    public void TakeDamage(float damage)
    {
        // 현재 체력을 damage 만큼 감소
        currentHP -= damage;

        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        // 체력이 0 이하일 경우 사망 처리
        if (currentHP <= 0)
        {
            // Die 처리 
            boss.OnDie();
        }
    }

    private IEnumerator HitColorAnimation()
    {
        // 잠깐 색상을 변경
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.05f);

        spriteRenderer.color = Color.white;
    }
}

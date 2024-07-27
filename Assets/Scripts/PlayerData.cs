using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 플레이어 데이터 관리 스크립트
public class PlayerData : MonoBehaviour
{

    [SerializeField]
    private float maxHp = 10; // 최대 체력
    private float currentHP;    // 현재 체력

    private float currentExperience; // 현재 경험치
    private float maxExperience; // 최대 경험치 

    private int level;

    private int attackLevel;        // 공격 레벨 
    private float attackRate;       // 공격 속도(총알이 나가는 텀)

    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;

    public float MaxHP => maxHp;        // maxHP 변수에 접근할 수 있는 프로퍼티 (Get만 가능)
    public float CurrentHP => currentHP;    // currenHP 변수에 접근할 수 있는 프로퍼티 (Get만 가능)

    public float MaxExperience => maxExperience;

    public float CurrentExperience => currentExperience;

    public int Level => level;



    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        currentHP = maxHp;
        currentExperience = 0;
        maxExperience = 100;
        level = 0;
    }

    // 플레이어가 데미지를 입는 메서드 
    public void TakeDamage(float damage)
    {
        // 현재 체력을 damage 만큼 감소     
        currentHP -= damage;

        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        // 체력이 0이하 = 플레이어 캐릭터 사망
        if (currentHP <= 0)
        {
            // 체력이 0이면 OnDie() 메서드를 호출해서 죽었을 때 처리를 해준다. 
            playerController.OnDie();
        }
    }

    // 플레이어가 경험치를 얻는 메서드 
    public void GainExperience(int amount) {
        currentExperience += amount;
        if (currentExperience >= maxExperience) {
            LevelUp();
        }
    }


    // 레벨업시 호출되는 메서드 
    void LevelUp() {
        level++;

        currentExperience = 0;
        maxExperience *= 2;
        maxHp += 10;
        currentHP = maxHp;
        
    }

    // 플레이어가 체력을 입었을 경우 색상을 변경 해주는 코루틴 선언 
    IEnumerator HitColorAnimation()
    {
        // 색상 빨간색으로
        spriteRenderer.color = Color.red;

        // 0.1초간 대기 
        yield return new WaitForSeconds(0.1f);

        // 플레이어 색상을 원래 색상인 하얀색으로
        spriteRenderer.color = Color.white;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BossState { MoveToAppearPoint = 0, Phase01 }


// 보스 구현 스크립트 
public class Boss : MonoBehaviour
{

    [SerializeField]
    private float bossAppearPoint = 2f;
    private BossState bossState = BossState.MoveToAppearPoint;
    private Movement2D movement2D;
    private BossWeapon bossWeapon;


    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        bossWeapon = GetComponent<BossWeapon>();
    }

    public void ChangeState(BossState newState)
    {
        // State를 코루틴 메서드 이름과 일치시켜 실행
        StopCoroutine(bossState.ToString());

        // 상태 변경
        bossState = newState;

        // 새로운 상태 재생
        StartCoroutine(bossState.ToString());
    }

    IEnumerator MoveToAppearPoint()
    {
        Debug.Log("보스 이동 코루틴 타니?");
        // 이동 방향 설정 
        movement2D.MoveTo(Vector3.down);

        while (true)
        {
            if (transform.position.y <= bossAppearPoint)
            {
                // 이동 방향을 (0, 0, 0)으로 설정해 멈추도록 한다. 
                movement2D.MoveTo(Vector3.zero);

                // 보스의 첫번째 공격 상태로 변경
                ChangeState(BossState.Phase01);
            }

            yield return null;
        }
    }

    // 첫번째 공격 상태 코루틴
    IEnumerator Phase01()
    {
        // 원 형태의 방사 공격 시작
        bossWeapon.StartFiring(AttackType.CircleFire);

        while (true)
        {
            yield return null;
        }
    }
}

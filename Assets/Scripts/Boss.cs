using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BossState { MoveToAppearPoint = 0, Phase01, Phase02, Phase03, Idle }


// 보스 구현 스크립트 
public class Boss : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;            // 맵의 스테이지 정보 

    [SerializeField]
    private float bossAppearPoint = 2f;
    private BossState bossState = BossState.MoveToAppearPoint;
    private Movement2D movement2D;
    private BossWeapon bossWeapon;
    private BossData bossData;

    private SpriteRenderer spriteRenderer;

    private float fadeDuration = 2.0f;          // 페이드 아웃에 걸리는 시간 

    [SerializeField] private GameObject bossExplosionPrefab;        // 보스 사망시 폭발효과 프리팹

    [SerializeField] private GameObject[] bossExplosionPointList;  // 보스가 사망할 경우 폭발 효과를 나타내는 Point 리스트 


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement2D = GetComponent<Movement2D>();
        bossWeapon = GetComponent<BossWeapon>();
        bossData = GetComponent<BossData>();
    }

    // 보스 상태 변경 메서드 
    public void ChangeState(BossState newState)
    {
        // State를 코루틴 메서드 이름과 일치시켜 실행
        StopCoroutine(bossState.ToString());

        // 상태 변경
        bossState = newState;

        // 새로운 상태 재생
        StartCoroutine(bossState.ToString());
    }


    // 보스의 사망 관리 메서드 
    public void OnDie()
    {
        ChangeState(BossState.Idle);
        // 서서히 흐려지면서 폭발 효과를 줌 
        StartCoroutine(BossFadeOut());
    }


    // 서서히 흐려지는 효과 
    private IEnumerator BossFadeOut()
    {
        Color startColor = spriteRenderer.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);       // startColor와 동일하며 알파값만 0f;
        float timeElapsed = 0f;


        // 폭발 이펙트를 포인트 리스트의 각 포인트에 생성
        foreach (GameObject explosionPoint in bossExplosionPointList)
        {
            GameObject cloneExplosionPrefab = Instantiate(bossExplosionPrefab, explosionPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1.0f);
            Destroy(cloneExplosionPrefab);
        }

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(startColor, endColor, timeElapsed / fadeDuration);
            yield return null;
        }

        spriteRenderer.color = endColor;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
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
    private IEnumerator Phase01()
    {
        // 원 형태의 방사 공격 시작
        bossWeapon.StartFiring(AttackType.CircleFire);
        Debug.Log("원 형태의 방사 공격 시작");

        while (true)
        {
            // 보스의 현재 체력이 일정 % 이하가 되면 상태 변경 
            if (bossData.CurrentHP <= bossData.MaxHP * 0.8f)
            {
                // 현재 공격 중지 
                bossWeapon.StopFiring(AttackType.CircleFire);

                // 새로운 상태로 변경 
                ChangeState(BossState.Phase02);
            }
            yield return null;
        }
    }

    // 두번째 공격 상태 코루틴 
    private IEnumerator Phase02()
    {
        // 플레이어 위치를 기준으로 단일 발사체 공격 시작
        bossWeapon.StartFiring(AttackType.SingleFireToCenterPosition);

        // 처음 이동 방향을 오른쪽으로 설정
        Vector3 direction = Vector3.right;
        movement2D.MoveTo(direction);

        while (true)
        {
            // 좌-우 이동 중 양쪽 끝에 도달하게 되면 방향을 반대로 설정
            if (transform.position.x <= stageData.LimitMin.x ||
                transform.position.x >= stageData.LimitMax.x)
            {
                direction *= -1;
                movement2D.MoveTo(direction);
            }

            // 보스의 현재 체력이 30% 이하가 되면 상태 변경
            if (bossData.CurrentHP <= bossData.MaxHP * 0.3f)
            {
                bossWeapon.StopFiring(AttackType.SingleFireToCenterPosition);

                ChangeState(BossState.Phase03);
            }

            yield return null;
        }
    }

    private IEnumerator Phase03()
    {
        // 원 방사 형태의 공격 시작 
        bossWeapon.StartFiring(AttackType.CircleFire);

        // 단일 발사 공격
        bossWeapon.StartFiring(AttackType.SingleFireToCenterPosition);

        // 처음 이동 방향을 오른쪽으로 설정        
        Vector3 direction = Vector3.right;
        movement2D.MoveTo(direction);

        while (true)
        {
            // 좌-우 이동 중 양쪽 끝에 도달하게 되면 방향을 반대로 설정
            if (transform.position.x <= stageData.LimitMin.x ||
                transform.position.x >= stageData.LimitMax.x)
            {
                direction *= -1;
                movement2D.MoveTo(direction);
            }

            yield return null;
        }

    }


    // 정지 상태 코루틴
    private IEnumerator Idle()
    {
        // 현재 공격하는 상태를 모두 멈춤
        bossWeapon.StopFiring(AttackType.CircleFire);
        bossWeapon.StopFiring(AttackType.SingleFireToCenterPosition);

        // 보스가 움직이고 있다면 제자리에 멈추게함
        movement2D.enabled = false;

        yield return new WaitForSeconds(0.5f);
    }
}

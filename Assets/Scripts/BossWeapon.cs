using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum AttackType { CircleFire = 0, SingleFireToCenterPosition }
// 보스의 공격 패턴 정의 
public class BossWeapon : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab; // 보스의 총알 프리팹 

    public void StartFiring(AttackType attackType)
    {
        // 상태에 따른 공격 코루틴 실행
        StartCoroutine(attackType.ToString());
    }

    public void StopFiring(AttackType attackType)
    {
        // 상태에 따른 공격 코루틴 중지
        StopCoroutine(attackType.ToString());
    }

    // 원 형태로 발사하는 코루틴 : 패턴 1
    private IEnumerator CircleFire()
    {
        float attackRate = 0.8f;    // 공격 주기 
        int count = 30;     // 발사체 생성 개수 
        float intervalAngle = 360 / count;  // 발사체 사이의 각도
        float weightAngle = 0;  // 가중되는 각도 (항상 같은 위치로 발사하지 않도록 설정)

        // 원 형태로 방사하는 발사체 생성 (count 개수 만큼)
        while (true)
        {
            for (int i = 0; i < count; i++)
            {
                // 발사체 생성 
                GameObject cloneProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                // 발사체 이동 방향(각도)
                float angle = weightAngle + intervalAngle * i;
                // 발사체 이동 방향 (벡터)
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);

                // 발사체 이동방향 설정
                cloneProjectile.GetComponent<Movement2D>().MoveTo(new Vector2(x, y));
            }

            // 발사체의 각도 변경을 위함
            weightAngle++;

            // attacKRate 시간 만큼 대기
            yield return new WaitForSeconds(attackRate);
        }
    }

    // 일렬로 발사하는 코루틴 : 패턴 2
    private IEnumerator SingleFireToCenterPosition()
    {
        Vector3 targetPosition = Vector3.zero;
        float attackRate = 0.3f;

        while (true)
        {
            // 발사체 생성
            GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // 발사체 이동 방향
            Vector3 direction = (targetPosition - clone.transform.position).normalized;

            // 발사체 이동방향 지정
            clone.GetComponent<Movement2D>().MoveTo(direction);

            // attackRate 만큼 시간 대기
            yield return new WaitForSeconds(attackRate);
        }
    }

}

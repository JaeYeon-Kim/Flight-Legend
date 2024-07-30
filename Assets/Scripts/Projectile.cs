using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


// 발사체 스크립트 
public class Projectile : MonoBehaviour
{
    private int damage = 1;   // 총알 데미지 
    public IObjectPool<GameObject> Pool { get; set; }

    [SerializeField] private StageData stageData;
    private float destroyWeight = 2.0f;

    private Weapon weapon;

    private void Awake()
    {
        weapon = GetComponent<Weapon>();
    }

    // 오브젝트 풀로 가져온 Projectitle에 새로운 데미지를 설정하는 메서드
    public void SetDamage(int weaponDamage)
    {
        this.damage = weaponDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!gameObject.activeSelf) return; // 이미 비활성화된 경우 처리 중단

        Debug.Log("변화한 공격력" + this.damage);

        // 일반 몬스터 충돌처리 
        if (collision.CompareTag("Enemy"))
        {
            // 부딪힌 오브젝트 데미지 주는 처리 
            collision.GetComponent<EnemyHP>().TakeDamage(this.damage);
        }
        // 보스 몬스터 충돌처리
        else if (collision.CompareTag("Boss"))
        {
            collision.GetComponent<BossData>().TakeDamage(this.damage);
        }
        else
        {
            return;
        }

        // 오브젝트 풀에 반환
        if (Pool != null)
        {
            Pool.Release(this.gameObject);
        }

    }


    // LateUpdate에서 특정 오브젝트가 제한한 범위를 일정 거리만큼 벗어나면 오브젝트 풀로 반환 
    private void LateUpdate()
    {
        if (!gameObject.activeSelf) return; // 이미 비활성화된 경우 처리 중단
        
        if (transform.position.y < stageData.LimitMin.y - destroyWeight ||
            transform.position.y > stageData.LimitMax.y + destroyWeight ||
            transform.position.x < stageData.LimitMin.x - destroyWeight ||
            transform.position.x > stageData.LimitMax.x + destroyWeight)
        {
            // 오브젝트 풀에 반환
            if (Pool != null)
            {
                Pool.Release(this.gameObject);
            }
        }
    }
}

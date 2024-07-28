using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


// 발사체 스크립트 
public class Projectile : MonoBehaviour
{
    private int damage = 0;   // 총알 데미지 
    public IObjectPool<GameObject> Pool { get; set; }

    [SerializeField]
    private StageData stageData;
    private float destroyWeight = 2.0f;

    private Weapon weapon;

    private void Awake() {
        weapon = GetComponent<Weapon>();
    }

    private void Start() {
        damage = weapon.Damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 일반 몬스터 충돌처리 
        if (collision.CompareTag("Enemy"))
        {

            // 부딪힌 오브젝트 데미지 주는 처리 
            collision.GetComponent<EnemyHP>().TakeDamage(damage);

            // 오브젝트 풀에 반환
            Pool.Release(this.gameObject);
        }
        // 보스 몬스터 충돌처리
        else if (collision.CompareTag("Boss"))
        {
            collision.GetComponent<BossData>().TakeDamage(damage);

            // 오브젝트 풀에 반환
            Pool.Release(this.gameObject);
        }
        else
        {
            return;
        }

    }


    // LateUpdate에서 특정 오브젝트가 제한한 범위를 일정 거리만큼 벗어나면 오브젝트 풀로 반환 
    private void LateUpdate()
    {
        if (transform.position.y < stageData.LimitMin.y - destroyWeight ||
            transform.position.y > stageData.LimitMax.y + destroyWeight ||
            transform.position.x < stageData.LimitMin.x - destroyWeight ||
            transform.position.x > stageData.LimitMax.x + destroyWeight)
        {
            Pool.Release(this.gameObject);
        }
    }
}

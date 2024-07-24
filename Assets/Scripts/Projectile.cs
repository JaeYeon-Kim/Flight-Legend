using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


// 발사체 스크립트 
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;         // 총알의 공격력 
    public IObjectPool<GameObject> Pool { get; set; }

    [SerializeField]
    private StageData stageData;
    private float destroyWeight = 2.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {

            // 부딪힌 오브젝트 데미지 주는 처리 
            collision.GetComponent<EnemyHP>().TakeDamage(damage);

            // 오브젝트 풀에 반환
            Pool.Release(this.gameObject);
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

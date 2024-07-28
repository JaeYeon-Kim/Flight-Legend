using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 일반 몬스터 및 보스가 사용하는 총알 스크립트 
public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision) {
        // 발사체에 부딪힌 오브젝트가 player 일 경우에만
        if(collision.CompareTag("Player")) {
            // 부딪힌 오브젝트 체력 감소 (플레이어)
            collision.GetComponent<PlayerData>().TakeDamage(damage);
            // 발사체 오브젝트 삭제 
            Destroy(gameObject);
        }
    }
}

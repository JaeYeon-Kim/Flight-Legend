using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 발사체 스크립트 
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;         // 총알의 공격력 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // 부딪힌 적 데미지 주는 처리 
            // collision.GetComponent<>

            // 내 오브젝트 삭제(발사체)
            Destroy(gameObject);
        }
    }
}

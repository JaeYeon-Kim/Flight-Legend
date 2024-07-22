using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 발사체 스크립트 
public class Projectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 부딪힌 오브젝트 사망처리 (적)
            Destroy(collision.gameObject);

            // 내 오브젝트 삭제(발사체)
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 적 개체용 스크립트 
public class Enemy : MonoBehaviour
{

    [SerializeField]
    private int damage = 1;         // 적 공격력 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적에게 부딪힌 오브젝트의 태그가 "Player"이면
        if (collision.CompareTag("Player"))
        {
            // 적 공격력만큼 플레이어 체력 감소 
            collision.GetComponent<PlayerHP>().TakeDamage(damage);


            Destroy(gameObject);
        }
    }
}

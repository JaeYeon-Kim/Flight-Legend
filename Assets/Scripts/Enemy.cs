using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 적 개체용 스크립트 
public class Enemy : MonoBehaviour
{

    [SerializeField]
    private int damage = 1;         // 적 공격력 
    [SerializeField]
    private int scorePoint = 100;   // 적 처치시 획득 점수 
    private PlayerController playerController;

    private void Awake() {
        // 한번만 Find를 이용해 가져온다. 
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }


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

    // 적개체 (본인)이 죽을 경우 호출되는 메서드 
    public void OnDie() {
        // 플레이어의 점수를 scorePoint 만큼 증가시킨다. 
        playerController.Score += scorePoint;
        // 적 오브젝트 삭제 
        Destroy(gameObject);
    }
}

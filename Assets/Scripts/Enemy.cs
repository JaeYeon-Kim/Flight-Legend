using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 적 개체용 스크립트 
public class Enemy : MonoBehaviour
{

    [SerializeField]
    private GameObject explosionPrefab;

    [SerializeField]
    private int damage = 1;         // 적 공격력 
    [SerializeField]
    private int scorePoint = 100;   // 적 처치시 획득 점수 
    private PlayerController playerController;

    private void Awake()
    {
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
    public void OnDie()
    {


        // 적 개체가 사망할경우 그자리에 폭발 이펙트 생성 
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);

        // 플레이어의 점수 scorePoint 만큼 증가 시킴 
        playerController.Score += scorePoint;


    }
}

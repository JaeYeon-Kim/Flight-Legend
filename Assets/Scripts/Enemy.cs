using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;


// 적 개체용 스크립트 
public class Enemy : MonoBehaviour
{

    // 적 개체 유형 지형 
    public enum EnemyType
    {
        Idle, Attack
    };

    [SerializeField] private GameObject bulletPrefab; // 발사할 총알 프리팹

    [SerializeField]
    private GameObject explosionPrefab;

    [SerializeField]
    private int damage = 1;         // 적 공격력 
    [SerializeField]
    private int scorePoint = 100;   // 적 처치시 획득 점수

    [SerializeField]
    private float attackRate = 1f;    // 공격 속도 

    [SerializeField]
    private float detectionRange = 5f;  // 플레이어를 감지할 범위 

    private PlayerController playerController;
    private Transform player;
    private Vector2 shootDirection;


    private void Awake()
    {
        // 한번만 Find를 이용해 가져온다. 
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player = playerController.gameObject.transform;
    }


    private void Start()
    {
        StartCoroutine(ShootBullet());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적에게 부딪힌 오브젝트의 태그가 "Player"이면
        if (collision.CompareTag("Player"))
        {
            // 적 공격력만큼 플레이어 체력 감소 
            collision.GetComponent<PlayerData>().TakeDamage(damage);


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

    private void Shoot()
    {
        shootDirection = (player.position - transform.position).normalized;
        // 총알 발사 
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Movement2D>().MoveTo(shootDirection);
    }


    // 적개체 총알 발사 
    IEnumerator ShootBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackRate);

            if (player != null)
            {

                Debug.Log("플레이어 맞아!!!");
                // 총알을 발사할 방항 계산
                float distancePlayer = Vector2.Distance(transform.position, player.position);

                // 플레이어가 일정 범위 안에 있으면 총알 발사 
                if (distancePlayer <= detectionRange)
                {
                    Debug.Log("범위 안으로 들어왔다구!!");
                    Shoot();
                }
            }
        }
    }
}

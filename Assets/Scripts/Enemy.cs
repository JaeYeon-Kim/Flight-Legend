
using System;
using System.Collections;
using Unity.VisualScripting;
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

    [SerializeField] private GameObject explosionPrefab;     // 적 파괴시 폭발 효과를 나타내는 프리팹

    [SerializeField] private GameObject[] itemPrefabs;      // 적 처치시 획득 가능한 아이템 리스트들 

    [SerializeField] private int damage = 1;         // 적 공격력 
    [SerializeField] private int exp = 20;

    [SerializeField] private float attackRate = 1f;    // 공격 속도 

    [SerializeField] private float detectionRange = 5f;  // 플레이어를 감지할 범위 

    private PlayerController playerController;

    private PlayerData playerData;
    private Transform playerTrasform;

    private GameObject player;
    private Vector2 shootDirection;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // 한번만 Find를 이용해 가져온다. 
        playerController = player.GetComponent<PlayerController>();
        playerTrasform = player.transform;
        playerData = player.GetComponent<PlayerData>();
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

        // 플레이어의 경험치 증가시킴 
        playerData.GainExperience(exp);

        // 적 개체가 사망할경우 그자리에 폭발 이펙트 생성 
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // 적 처치시 일정 확률로 아이템을 생성 해준다. 
        SpawnItem();

        // 오브젝트 파괴 
        Destroy(gameObject);
    }

    // 아이템을 생성하는 메서드
    private void SpawnItem() {

        // 필살기 아이템 
        int bombItemPercentage = UnityEngine.Random.Range(0, 100);

        if(bombItemPercentage < 5) {
            GameObject cloneBombItem = Instantiate(itemPrefabs[0], transform.position, Quaternion.identity);
            cloneBombItem.GetComponent<Item>().playerTransform = this.playerTrasform;
        }
        
    }

    private void Shoot()
    {
        shootDirection = (playerTrasform.position - transform.position).normalized;
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
                // 총알을 발사할 방항 계산
                float distancePlayer = Vector2.Distance(transform.position, playerTrasform.position);

                // 플레이어가 일정 범위 안에 있으면 총알 발사 
                if (distancePlayer <= detectionRange)
                {
                    Shoot();
                }
            }
        }
    }
}

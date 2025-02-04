
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


// 적 개체용 스크립트 
public class Enemy : MonoBehaviour
{

    // 적 개체 유형 지형 : 공격하는적 or 공격하지 않고 몸통박치기 하는적 
    public enum EnemyType
    {
        Idle, Attack, SpinAttack
    };

    [SerializeField] private EnemyType enemyType;

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
        StartShooting();    // EnemyType에 따라 공격할지 말지 결정해줌 
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
    private void SpawnItem()
    {

        // 필살기 아이템 
        int spawnItemPercentage = UnityEngine.Random.Range(0, 100);

        if (spawnItemPercentage < 30)           // 30% 확률로 별 아이템 획득, 1 ~ 5개 랜덤 
        {
            int randomStarCount = UnityEngine.Random.Range(1, 6);   // 1 ~ 5까지 갯수 랜덤

            for (int i = 0; i < randomStarCount; i++)
            {
                float x = UnityEngine.Random.Range(-0.4f, 0.4f);
                float y = UnityEngine.Random.Range(-0.4f, 0.4f);
                GameObject cloneStarItem = Instantiate(itemPrefabs[0], new Vector3(x, y, 0), Quaternion.identity);
                cloneStarItem.GetComponent<Item>().playerTransform = this.playerTrasform;
            }

        }
        else if (spawnItemPercentage < 35)      // 5% 확률로 플레이어 필살기 아이템 획득 
        {
            GameObject cloneBombItem = Instantiate(itemPrefabs[1], transform.position, Quaternion.identity);
            cloneBombItem.GetComponent<Item>().playerTransform = this.playerTrasform;
        }
    }

    // 플레이어 쪽으로 총알을 발사하는 개체 
    private void Shoot()
    {
        shootDirection = (playerTrasform.position - transform.position).normalized;
        // 총알 발사 
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Movement2D>().MoveTo(shootDirection);
    }

    // 자신 주위로 원형으로 발사하는 개체 
    private void ShootSpinAttack(int bulletCount)
    {
        float intervalAngle = 360 / bulletCount;  // 발사체 사이의 각도
        float weightAngle = 0;  // 가중되는 각도 (항상 같은 위치로 발사하지 않도록 설정)

        for (int i = 0; i < bulletCount; i++)
        {
            // 발사체 생성 
            GameObject cloneProjectile = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            // 발사체 이동 방향(각도)
            float angle = weightAngle + intervalAngle * i;
            // 발사체 이동 방향 (벡터)
            float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
            float y = Mathf.Sin(angle * Mathf.PI / 180.0f);

            // 발사체 이동방향 설정
            cloneProjectile.GetComponent<Movement2D>().MoveTo(new Vector2(x, y));
        }

        // 발사체의 각도 변경을 위함
        weightAngle++;

    }

    private void StartShooting()
    {
        switch (enemyType)
        {
            case EnemyType.Idle:
                break;

            case EnemyType.Attack:
                // 적 개체
                StartCoroutine(ShootBullet());
                break;
            case EnemyType.SpinAttack:
                StartCoroutine(ShootBullet());
                break;
        }
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
                    if (enemyType == EnemyType.Attack)
                    {
                        Shoot();
                    }
                    else if (enemyType == EnemyType.SpinAttack)
                    {
                        ShootSpinAttack(8);
                    }
                }
            }
        }
    }
}

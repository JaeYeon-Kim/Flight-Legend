using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 플레이어 필살기 구현을 위한 스크립트 
public class PlayerBomb : MonoBehaviour
{

    [SerializeField] private float bombDamage = 20;              // 필살기 데미지 

    [SerializeField] private float moveSpeed = 2f;               // 이동 속도 

    [SerializeField] GameObject[] bombPointList;        // 폭발하는 프리팹이 보여지는 위치 리스트

    [SerializeField] GameObject bombPrefab;             // 폭발 프리팹 


    private float destroyTime = 5f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateExplosionEffect());
        
        Destroy(gameObject, 6f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }


    // 적개체가 닿는 동안에는 계속 피해를 입음 
    private void OnTriggerStay2D(Collider2D collision)
    {
        // 닿고 있는게 적개체면 데미지를 입힘
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHP>().TakeDamage(bombDamage);
        }
    }

    private IEnumerator CreateExplosionEffect()
    {
        while (true)
        {
            foreach (var bombPoint in bombPointList)
            {
                GameObject cloneBomb = Instantiate(bombPrefab, bombPoint.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(1f);
                Destroy(cloneBomb);
            }

            yield break;
        }
    }
}

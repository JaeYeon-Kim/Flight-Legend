using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;


public enum ItemType { PlayerBomb = 0, Star = 0 }
public class Item : MonoBehaviour
{
    [SerializeField] private ItemType itemType;
    private Movement2D movement2D;

    public Transform playerTransform;

    [SerializeField] private float magnetMoveSpeed = 2f;       // 자석 처럼 끌려갈때 아이템의 이동속도 
    [SerializeField] private float magnetDistance = 5f;        // 자석이 작용 하는 거리 


    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
    }

    private void Start()
    {
        movement2D.MoveTo(Vector3.down);
    }

    private void Update() {
        // 아이템과 플레이어 사이의 거리 계산
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        // 거리가 magnetDistance 이내일 경우 아이템을 플레이어 쪽으로 이동 
        if (distance <= magnetDistance) {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, magnetMoveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 아이템 획득 할 경우 나타나는 효과
            UseItem(collision.gameObject);

            // 플레이어가 획득하였을 경우 오브젝트 삭제 
            Destroy(gameObject);
        }
    }

    private void UseItem(GameObject player)
    {
        switch (itemType)
        {
            case ItemType.PlayerBomb:
                player.GetComponent<Weapon>().CreateBombCount();
                break;
        }
    }
}

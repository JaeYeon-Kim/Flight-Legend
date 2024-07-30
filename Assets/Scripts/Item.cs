using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;


public enum ItemType { PlayerBomb = 0, Star = 0 }
public class Item : MonoBehaviour
{
    [SerializeField] private ItemType itemType;
    private Movement2D movement2D;

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
    }

    private void Start()
    {
        movement2D.MoveTo(Vector3.down);
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

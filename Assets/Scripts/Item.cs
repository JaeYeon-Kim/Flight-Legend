using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;


public enum ItemType { PlayerBomb = 0, Star = 1 }
public class Item : MonoBehaviour
{
    [SerializeField] private ItemType itemType;     // Item Type을 구분하는 Enum 변수 
    [SerializeField] private float magnetMoveSpeed = 2f;       // 자석 처럼 끌려갈때 아이템의 이동속도 
    [SerializeField] private float magnetDistance = 5f;        // 자석이 작용 하는 거리 

    [SerializeField] private AudioClip audioClip;       // 효과음 재생용 음악

    private AudioSource audioSource;   // 효과음 재생을 위한 변수 


    private Movement2D movement2D;                          // 아이템의 이동

    public Transform playerTransform;                       // 플레이어 position을 가져오기 위함 


    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        movement2D.MoveTo(Vector3.down);
    }

    private void Update()
    {
        // 아이템과 플레이어 사이의 거리 계산
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        // 거리가 magnetDistance 이내일 경우 아이템을 플레이어 쪽으로 이동 
        if (distance <= magnetDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, magnetMoveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 아이템 획득 할 경우 나타나는 효과
            UseItem(collision.gameObject);

            Destroy(gameObject);

            PlaySound();
        }
    }

    // 아이템을 획득했을 경우 타입으로 분리 
    private void UseItem(GameObject player)
    {
        switch (itemType)
        {
            // 플레이어 폭탄 횟수 증가 
            case ItemType.PlayerBomb:
                player.GetComponent<Weapon>().BombCountUp();
                break;

            // 플레이어 별 갯수 증가 
            case ItemType.Star:
                player.GetComponent<PlayerData>().StarCountUp();
                break;
        }
    }

    private void PlaySound()
    {
        if (audioClip != null) {
            SoundManager.Instance.PlayEffectSound(audioClip);
        }
    }
}

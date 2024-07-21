using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 배경 스크롤링 스크립트 
public class PositionScroller : MonoBehaviour
{
    [SerializeField]
    private Transform target;  // 게임에서 두개의 배경이 서로가 서로의 타겟
    [SerializeField]
    private float scrollRange = 20f;
    [SerializeField]
    private float moveSpeed = 3.0f;     // 배경 스크롤링 속도 
    [SerializeField]
    private Vector3 moveDirection = Vector3.down;       // 배경이 스크롤링 되는 방향 

    // Update is called once per frame
    void Update()
    {
        // 배경이 moveDirection 방향으로 moveSpeed의 속도로 이동 
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // 배경이 설정된 범위를 벗어나면 위치 재설정
        if (transform.position.y <= -scrollRange)
        {
            transform.position = target.position + Vector3.up * scrollRange;
        }

    }
}

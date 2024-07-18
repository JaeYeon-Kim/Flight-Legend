using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 실제 이동을 반영하는 class 
public class Movement2D : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 0.0f;     // 이동 스피드 
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;       // 이동하는 방향 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
    // 외부에서 호출해 이동 방향을 설정 
    public void MoveTo(Vector3 direction) {
        moveDirection = direction;
    }
}

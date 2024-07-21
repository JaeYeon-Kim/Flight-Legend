using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{

    private Movement2D movement2D;

    private void Awake() {
        movement2D = GetComponent<Movement2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 방향키를 눌러 이동 방향 설정 
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");


        // MoveTo() 메소드에 값 입력
        movement2D.MoveTo(new Vector3(x, y, 0));
    }

    private void LateUpdate() {
        // 플레이어 캐릭터가 화면 범위 바깥으로 나가지 못하게 함 
        transform.position = new Vector3()    
    }
}

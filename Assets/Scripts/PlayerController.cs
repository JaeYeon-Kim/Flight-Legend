using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private StageData stageData;

    [SerializeField]
    private KeyCode keyCodeAttack = KeyCode.Space;      // 공격키 설정 
    private Movement2D movement2D;

    private Weapon weapon;

    // 점수 
    private int score;
    public int Score
    {
        // score 값이 음수가 되면 안됨
        set => score = Mathf.Max(0, value);
        get => score;
    }


    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        weapon = GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        // 방향키를 눌러 이동 방향 설정 
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");


        // MoveTo() 메소드에 값 입력
        movement2D.MoveTo(new Vector3(x, y, 0));

        // 공격키를 Down/up으로 공격 시작 / 종료 
        if (Input.GetKeyDown(keyCodeAttack))
        {
            weapon.StartFiring();
        }
        else if (Input.GetKeyUp(keyCodeAttack))
        {
            weapon.StopFiring();
        }
    }

    private void LateUpdate()
    {
        // 플레이어 캐릭터가 화면 범위 바깥으로 나가지 못하게 함 
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x),
                                Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y));
    }
}

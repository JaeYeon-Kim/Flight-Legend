using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private string nextSceneName;

    [SerializeField] private StageData stageData;

    [SerializeField] private KeyCode keyCodeAttack = KeyCode.Space;      // 공격키 설정 

    [SerializeField] private KeyCode keyCodeBomb = KeyCode.B;            // 플레이어 필살기 
    private Movement2D movement2D;

    private Weapon weapon;


    // 테스트를 위한 변수 
    private Vector3 touchStartPos;
    private Vector3 touchEndPos;
    private Vector3 direction;



    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        weapon = GetComponent<Weapon>();
    }

    private void Start()
    {
        // 플레이어 생성시 총알을 발사하는 코루틴 시작 
        weapon.StartFiring();
    }

    // Update is called once per frame
    void Update()
    {
        // 방향키를 눌러 이동 방향 설정 
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // MoveTo() 메소드에 값 입력
        movement2D.MoveTo(new Vector3(x, y, 0));

        // // 공격키를 Down/up으로 공격 시작 / 종료 
        // if (Input.GetKeyDown(keyCodeAttack))
        // {
        //     weapon.StartFiring();
        // }
        // else if (Input.GetKeyUp(keyCodeAttack))
        // {
        //     weapon.StopFiring();
        // }

        // 모바일용 이동 로직 추가 
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    touchStartPos = touch.position;
                }

                else if (touch.phase == TouchPhase.Moved)
                {
                    touchEndPos = touch.position;
                    direction = (touchEndPos - touchStartPos).normalized;
                    movement2D.MoveTo(direction);
                }
            }
        }

        // 플레이어 폭탄 발사 
        if (Input.GetKeyDown(keyCodeBomb))
        {
            weapon.CreateBoomAttack();
        }
    }

    private void LateUpdate()
    {
        // 플레이어 캐릭터가 화면 범위 바깥으로 나가지 못하게 함 
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x),
                                Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y));
    }

    public void OnDie()
    {
        // 플레이어 사망 시 nextSceneName 씬으로 이동 
        SceneManager.LoadScene(nextSceneName);
    }
}

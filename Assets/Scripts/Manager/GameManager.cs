using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// GameManager
public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject LevelUpPopup;
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        LevelUpPopup.SetActive(false);      // 레벨업 팝업 비활성화 
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // 플레이어가 레벨업 되었을때 호출 되어 팝업창을 띄우는 메서드 
    public void LevelPopupOn() {
        // 게임 일시 정지
        Time.timeScale = 0f;

        // 레벨업 선택 창 띄우기 
        LevelUpPopup.SetActive(true);
    }

    // 플레이어가 선택했을경우 해당 창을 다시 닫고 게임을 다시 시작
    public void LevelPopupOff() {
        // 게임 재 시작 
        Time.timeScale = 1f;

        // 레벨업 선택 창 없애기
        LevelUpPopup.SetActive(false);

    }
}

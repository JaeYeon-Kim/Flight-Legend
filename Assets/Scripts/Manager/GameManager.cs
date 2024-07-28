using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// GameManager
public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject LevelUpPopup;
    public static GameManager instance;

    // 게임이 현재 일시정지 상태인지 확인 
    private bool isPause = false;

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

    // 일시정지 
    public void Pause()
    {
        if (!isPause)
        {
            Time.timeScale = 0; // 일시 정지 
            LevelUpPopup.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;     // 다시 게임 시작
            LevelUpPopup.SetActive(false);
        }
    }
}

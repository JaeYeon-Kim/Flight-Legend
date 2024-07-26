using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 적 생성기 Script
public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject[] enemyPrefabList;      // 일반 몬스터 리스트 
    [SerializeField] private GameObject boss;                   // 보스 프리팹 

    [SerializeField] private StageData stageData;        // 적 생성을 위한 스테이지 정보 
    [SerializeField] private GameObject enemyHPSliderPrefab; // 적 체력을 나타내는 Slider UI 프리팹
    [SerializeField] private Transform canvasTransform;  // UI를 표현하는 Canvas 오브젝트의 Transform
    [SerializeField] private GameObject sliderBossHP;    // 보스 체력 슬라이더바 
    [SerializeField] private float spawnTime;            // 생성 주기 
    [SerializeField] private int maxEnemyCount = 100; // 현재 스테이지의 최대 적 생성 숫자 

    [SerializeField] private SoundManager soundManager;

    // Start is called before the first frame update
    private void Awake()
    {
        // 보스 오브젝트 비활성화 
        boss.SetActive(false);


        // 보스 체력바 비활성화 
        sliderBossHP.SetActive(false);

        // 적 기체(일반 몬스터 생성 시작)
        StartCoroutine("SpawnEnemy");
    }


    // 적 생성 코루틴 
    IEnumerator SpawnEnemy()
    {
        int currentEnemyCount = 0; // 적 생성 숫자 카운트용 변수 

        while (true)
        {
            // x 위치는 스테이지 현재 크기 범위 내에서 임의로 값 선택 
            float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);

            // 랜덤으로 적을 뽑음 
            int randomEnemy = Random.Range(0, 4);

            // 뽑은 적을 생성 
            GameObject enemyClone = Instantiate(enemyPrefabList[randomEnemy], new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);

            // 적 체력바 생성
            SpawnEnemyHPSlider(enemyClone);

            // 적 생성 숫자 증가 
            currentEnemyCount++;

            // 적을 최대 숫자까지 생성하면 적 생성 코루틴 중지, 보스를 생성하는 코루틴 실행 
            if (currentEnemyCount == maxEnemyCount)
            {
                StartCoroutine("SpawnBoss");
                break;
            }

            // spawnTime만큼 대기 
            yield return new WaitForSeconds(spawnTime);
        }
    }


    // 체력바 생성 
    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        // 적 체력을 나타내는 Slider UI 프리팹 생성
        GameObject hpBarClone = Instantiate(enemyHPSliderPrefab);

        // 캔버스의 자식으로 설정 후에 크기 설정
        hpBarClone.transform.SetParent(canvasTransform);
        // (1, 1, 1)로 설정 
        hpBarClone.transform.localScale = Vector3.one;

        // Slider UI가 따라다닐 대상을 해당 객체로 설정
        hpBarClone.GetComponent<EnemyHPViewer>().Setup(enemy.transform, enemy.GetComponent<EnemyHP>());
    }


    // 보스를 등장 시키는 코루틴 
    private IEnumerator SpawnBoss()
    {
        // 보스 등장 BGM 설정
        soundManager.ChangeBgm(BGMType.Boss);

        // 2초 대기 
        yield return new WaitForSeconds(2.0f);

        // 보스 체력바 활성화 
        sliderBossHP.SetActive(true);

        // 보스 오브젝트 활성화 
        boss.SetActive(true);

        // 보스가 첫번째 상태를 실행할 수 있도록 한다. 
        boss.GetComponent<Boss>().ChangeState(BossState.MoveToAppearPoint);
    }
}

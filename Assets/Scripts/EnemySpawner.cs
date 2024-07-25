using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 적 생성기 Script
public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject[] enemyPrefabList;

    [SerializeField] private StageData stageData;        // 적 생성을 위한 스테이지 정보 
    [SerializeField] private GameObject enemyHPSliderPrefab; // 적 체력을 나타내는 Slider UI 프리팹
    [SerializeField] private Transform canvasTransform;  // UI를 표현하는 Canvas 오브젝트의 Transform
    [SerializeField] private float spawnTime;            // 생성 주기 
    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine("SpawnEnemy");
    }


    // 적 생성 코루틴 
    IEnumerator SpawnEnemy()
    {
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
}

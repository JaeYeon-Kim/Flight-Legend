using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 적 생성기 Script
public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    private StageData stageData;        // 적 생성을 위한 스테이지 정보 
    [SerializeField]
    private GameObject enemyPrefab;     // 복제해서 생성할 적 개체 프리팹 
    [SerializeField]
    private float spawnTime;            // 생성 주기 
    // Start is called before the first frame update
    private void Awake() {
        StartCoroutine("SpawnEnemy");
    }

    
    // 적 생성 코루틴 
    IEnumerator SpawnEnemy() {
        while (true) {
            // x 위치는 스테이지 현재 크기 범위 내에서 임의로 값 선택 
            float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);

            // 적 캐릭터 생성 
            Instantiate(enemyPrefab, new Vector3(positionX, stageData.LimitMax.y+1.0f, 0.0f), Quaternion.identity);

            // spawnTime만큼 대기 
            yield return new WaitForSeconds(spawnTime);
        }
    }
}

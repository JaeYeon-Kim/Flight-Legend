using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


// 게임 화면 밖으로 나가는 오브젝트들 삭제 
public class PositionAutoDestroyer : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    private float destroyWeight = 2.0f;

    // LateUpdate에서 특정 오브젝트가 제한한 범위를 일정 거리만큼 벗어나면 오브젝트를 삭제 시킴 
    private void LateUpdate()
    {
        if (transform.position.y < stageData.LimitMin.y - destroyWeight ||
            transform.position.y > stageData.LimitMax.y + destroyWeight ||
            transform.position.x < stageData.LimitMin.x - destroyWeight ||
            transform.position.x > stageData.LimitMax.x + destroyWeight)
        {
            Destroy(gameObject);
        }
    }
}

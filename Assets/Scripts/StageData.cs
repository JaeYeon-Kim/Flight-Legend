using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 영역 제한을 위한 ScriptableObject : 현재 스테이지의 화면 내 범위, 에셋 데이터로 저장해두고 사용 
// 공유 데이터 저장 가능
// 게임에 사용되는 데이터를 저장 후 게임 중간에 불러오는 것이 가능 
// 게임 도중 데이터 변경 가능 
[CreateAssetMenu]
public class StageData : ScriptableObject
{

    [SerializeField]
    private Vector2 limitMin;

    [SerializeField]
    private Vector2 limitMax;

    public Vector2 LimitMin => limitMin;
    public Vector2 LimitMax => limitMax;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;    // 공격할 때 생성되는 발사체 프리팹 
    [SerializeField]
    private float attackRate = 0.1f;    // 공격 속도 


    // 공격을 시작하는 코루틴 
    public void StartFiring()
    {
        StartCoroutine("TryAttack");
    }

    // 공격을 멈추는 코루틴 
    public void StopFiring()
    {
        StopCoroutine("TryAttack");
    }


    // 발사용 코루틴
    IEnumerator TryAttack()
    {
        while (true)
        {
            // 발사체 오브젝트 생성
            Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);

            // attackRate 시간만큼 대기 
            yield return new WaitForSeconds(attackRate);
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

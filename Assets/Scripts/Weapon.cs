using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Video;


// 무기 스크립트 
public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;    // 공격할 때 생성되는 발사체 프리팹 
    [SerializeField]
    private float attackRate = 0.1f;    // 공격 속도 

    [SerializeField]
    private int attackLevel = 1;    // 공격 레벨 

    // 공격 사운드 출력을 위한 변수 
    [SerializeField]
    private AudioClip audioClip;
    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


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
            SetAttackLevel();

            // 공격 사운드 
            audioSource.PlayOneShot(audioClip);

            // attackRate 시간만큼 대기 
            yield return new WaitForSeconds(attackRate);
        }
    }

    // // 공격 레벨 설정 
    // private void SetAttackLevel()
    // {

    //     GameObject cloneProjectile = null;

    //     switch (attackLevel)
    //     {
    //         // Level1 : 발사체 1개 생성 
    //         case 1:
    //             Instantiate(projectilePrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
    //             break;
    //         // Level2: 간격을 두고 전방으로 발사체 3개 생성 
    //         case 2:
    //             Instantiate(projectilePrefab, transform.position + Vector3.left * 0.2f + Vector3.up * 0.5f, Quaternion.identity);
    //             Instantiate(projectilePrefab, transform.position + Vector3.right * 0.2f + Vector3.up * 0.5f, Quaternion.identity);
    //             break;

    //         // Level3: 전방으로 발사체 1개, 좌우 대각선 방향으로 발사체 각 1개  
    //         case 3:
    //             Instantiate(projectilePrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
    //             // 왼쪽 대각선 방향으로 발사되는 발사체
    //             cloneProjectile = Instantiate(projectilePrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
    //             cloneProjectile.GetComponent<Movement2D>().MoveTo(new Vector3(-0.2f, 1, 0));

    //             // 오른쪽 대각선 방향으로 
    //             cloneProjectile = Instantiate(projectilePrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
    //             cloneProjectile.GetComponent<Movement2D>().MoveTo(new Vector3(0.2f, 1, 0));
    //             break;
    //     }
    // }


    // 공격 레벨 설정 
    private void SetAttackLevel()
    {

        switch (attackLevel)
        {
            // Level1 : 발사체 1개 생성 
            case 1:
                var cloneBullet = ObjectPoolManager.instance.Pool.Get();
                cloneBullet.transform.position = transform.position + Vector3.up * 0.5f;
                break;
            // Level2: 간격을 두고 전방으로 발사체 3개 생성 
            case 2:
                var cloneBulletSecond = ObjectPoolManager.instance.Pool.Get();
                cloneBulletSecond.transform.position = transform.position + Vector3.up * 0.5f + Vector3.left * 0.2f;
                var cloneBulletSecond2 = ObjectPoolManager.instance.Pool.Get();
                cloneBulletSecond2.transform.position = transform.position + Vector3.up * 0.5f + Vector3.right * 0.2f;
                break;
            // Level3: 전방으로 발사체 1개, 좌우 대각선 방향으로 발사체 각 1개     
            case 3:
                var cloneBulletThird = ObjectPoolManager.instance.Pool.Get();
                cloneBulletThird.transform.position = transform.position + Vector3.up * 0.5f;
                cloneBulletThird.GetComponent<Movement2D>().MoveTo(new Vector3(0f, 1, 0));

                var cloneBulletThird2 = ObjectPoolManager.instance.Pool.Get();
                cloneBulletThird2.transform.position = transform.position + Vector3.up * 0.5f;
                cloneBulletThird2.GetComponent<Movement2D>().MoveTo(new Vector3(-0.2f, 1, 0));

                var cloneBulletThird3 = ObjectPoolManager.instance.Pool.Get();
                cloneBulletThird3.transform.position = transform.position + Vector3.up * 0.5f;
                cloneBulletThird3.GetComponent<Movement2D>().MoveTo(new Vector3(0.2f, 1, 0));
                break;
        }
    }
}

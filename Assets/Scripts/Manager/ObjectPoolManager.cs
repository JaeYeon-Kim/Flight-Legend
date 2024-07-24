using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.U2D;


// 오브젝트 풀링 구현을 위한 Manager 
public class ObjectPoolManager : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;
    public static ObjectPoolManager instance;

    public int defaultCapacity = 10;        // 기본으로 생성해 놓을 양 
    public int maxPoolSize = 100;            // 풀의 최대 사이즈 

    public IObjectPool<GameObject> Pool { get; private set; }

    private void Awake()
    {
        // 싱글톤 선언 
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        InitPool();
    }

    private void InitPool()
    {
        Pool = new ObjectPool<GameObject>(CreatePool, OnTakeFromPool, OnReturnedToPool, 
        OnDestroyPool, true, defaultCapacity, maxPoolSize);

        // 오브젝트 풀 생성 
        for (int i = 0; i < defaultCapacity; i++)
        {
            Projectile bullet = CreatePool().GetComponent<Projectile>();
            bullet.Pool.Release(bullet.gameObject);
        }
    }

    // 생성 
    private GameObject CreatePool()
    {
        GameObject poolItem = Instantiate(bulletPrefab);
        poolItem.GetComponent<Projectile>().Pool = this.Pool;
        return poolItem;
    }
    // 사용
    private void OnTakeFromPool(GameObject poolItem)
    {
        poolItem.SetActive(true);
    }

    // 반환
    private void OnReturnedToPool(GameObject poolItem)
    {
        poolItem.SetActive(false);
    }

    // 삭제
    private void OnDestroyPool(GameObject poolItem)
    {
        Destroy(poolItem);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAutoDestroyer : MonoBehaviour
{

    private Animator animator;

    private float explosionTime = 0;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        explosionTime += Time.deltaTime;
        if(explosionTime > 1.0f) Destroy(gameObject);
        /*
        // 폭발 애니메이션 재생이 끝나면 삭제 
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Explosion") == true)
        {
            Debug.Log("원하는 애니메이션이야!!");
            // 원하는 애니메이션이 맞다면?
            float animTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (animTime >= 1.0f)
            {
                // 애니메이션 재생이 끝나면 현재 자신 오브젝트 삭제 
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.Log("원하는 애니메이션이 아님");
        }
        */
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGMType { Stage = 0, Boss = 1 }
// 사운드 관리 매니저 
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClip[] bgmClips; // 배경음악 파일 목록
    private AudioSource audioSource;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }


    public void ChangeBgm(BGMType type)
    {
        // 현재 재생중인 배경음악 중지 
        audioSource.Stop();

        // 배경음악 파일 목록에서 Type의 해당 번째를 가져와 교체해준다. 
        audioSource.clip = bgmClips[(int)type];

        // 바뀐 배경음악 재생 
        audioSource.Play();
    }

    public void PlayEffectSound(AudioClip audioClip)
    {
        if (audioSource != null && audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGMType { Stage = 0, Boss }
// 사운드 관리 매니저 
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] bgmClips; // 배경음악 파일 목록
    private AudioSource audioSource;


    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }


    public void ChangeBgm(BGMType type) {
        // 현재 재생중인 배경음악 중지 
        audioSource.Stop();

        // 배경음악 파일 목록에서 Type의 해당 번째를 가져와 교체해준다. 
        audioSource.clip = bgmClips[(int) type];

        // 바뀐 배경음악 재생 
        audioSource.Play();
    }

}

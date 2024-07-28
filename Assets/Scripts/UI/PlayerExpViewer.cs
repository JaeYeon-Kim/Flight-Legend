using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 플레이어 경험치바 UI용 
public class PlayerExpViewer : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private Slider expSlider;       // 경험치바 

    private void Awake() {
        expSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        expSlider.value = playerData.CurrentExperience / playerData.MaxExperience;
    }
}

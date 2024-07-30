using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


// 플레이어의 폭탄 카운트를 보여주는 클래스 
public class PlayerBombViewer : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI bombCountText;
    [SerializeField] private Weapon weapon;

    // Update is called once per frame
    void Update()
    {
        bombCountText.text = weapon.BombCount.ToString();
    }
}

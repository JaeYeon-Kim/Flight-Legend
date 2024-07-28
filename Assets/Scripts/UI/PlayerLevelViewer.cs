using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerLevelViewer : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    private TextMeshProUGUI textLevel;      // 레벨 표시를 위한 TextPro

    private void Awake() {
        textLevel = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textLevel.text = "Level " + playerData.Level;
    }
}

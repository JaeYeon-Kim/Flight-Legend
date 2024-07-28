using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPopupSetting : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SelectPopUp() {
        weapon.setWeaponStat(3);

        GameManager.instance.LevelPopupOff();
    }
}

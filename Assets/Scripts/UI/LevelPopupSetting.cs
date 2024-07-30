using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


// Level업 시 나오는 능력 강화 창을 관리하는 스크립트 
public class LevelPopupSetting : MonoBehaviour
{
    public event Action onPlayerSelectAbility;
    [SerializeField] GameObject[] abilityList;      // 능력을 담을 리스트 : 공격속도, 공격력, 공격레벨 
    [SerializeField] private Weapon weapon;         // 무기 관련 스크립트

    private void OnEnable()
    {
        LevelPopUpSetting();
    }

    // Max 레벨이 안찼을 경우 UI를 보여줌 
    private void LevelPopUpSetting()
    {
        // 리스트를 뽑아와서 GameObject 활성화 
        foreach (var abilityObject in abilityList)
        {

            Debug.Log("활성화할 오브젝트들" + abilityObject.name);

            abilityObject.gameObject.SetActive(true);
        }

        // 강화 수치가 MAX면 비활성화 
        if (weapon.AttackRate <= weapon.MaxAttackRate)
        {
            abilityList[0].gameObject.SetActive(false);
        }
        else if (weapon.Damage >= weapon.MaxAttackDamage)
        {
            abilityList[1].gameObject.SetActive(false);
        }
        else if (weapon.AttackLevel >= weapon.MaxAttackLevel)
        {
            abilityList[2].gameObject.SetActive(false);
        }
        else
        {
            return;
        }
    }

    // 각 버튼에 따라 타입을 다르게 하여 처리 
    public void AbilityLevelUp(int type)
    {
        switch (type)
        {
            case 1:
                {
                    Debug.Log("공격 속도 강화");
                    weapon.SetWeaponStat(type);
                    onPlayerSelectAbility?.Invoke();
                }
                break;

            case 2:
                {
                    Debug.Log("공격력 강화");
                    weapon.SetWeaponStat(type);
                    onPlayerSelectAbility?.Invoke();

                }
                break;

            case 3:
                {
                    Debug.Log("공격 레벨 강화");
                    weapon.SetWeaponStat(type);
                    onPlayerSelectAbility?.Invoke();

                }
                break;
        }
    }
}

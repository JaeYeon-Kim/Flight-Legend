using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// 후에 업그레이드할 플레이어의 능력을 위한 ScriptableObject
[CreateAssetMenu(fileName = "Item", menuName ="Scriptable Object/ItemData")]
public class PlayerAbility : ScriptableObject
{
    public enum ItemType { AttackLevel, AttackDamage, AttackSpeed, Health}

    [Header("# Main Info")]

    public ItemType itemType;
    public int itemId;
    public string itemName;
    public string itemDesc;
    public Sprite itemIcon;

    [Header("# Level Data")]
    public float baseAttackDamage;
    public int baseAttackLevel;
    public float baseAttackSpeed;

    public float baseHealth;

    public float[] damages;
    public int[] counts;

    [Header("# Weapon")]
    public GameObject projectile;
}

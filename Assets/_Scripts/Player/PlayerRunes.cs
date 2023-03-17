using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunes : MonoBehaviour
{
    [SerializeField]
    private WeaponBonusStats weaponBonusStats;
    public void UpdateStats(RuneDataSO runeData)
    {
        weaponBonusStats.SpreadAngleBonus += runeData.SpreadAngleBonus;
        weaponBonusStats.WeaponDelayBonus += runeData.WeaponDelayBonus;
        weaponBonusStats.BulletCountBonus += runeData.BulletCountBonus;
        weaponBonusStats.BulletSizeBonus += runeData.BulletSizeBonus;
        weaponBonusStats.DamageBonus += runeData.DamageBonus;
    }
}

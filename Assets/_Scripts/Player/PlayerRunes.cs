using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunes : MonoBehaviour
{
    [SerializeField]
    private WeaponBonusStats weaponBonusStats;

    [SerializeField]
    private RuneDataSO[] runesEquiped = new RuneDataSO[3];
    private int slotIndex = 0;

    public void UpdateStats(RuneDataSO runeData)
    {
        int currentIndex = slotIndex % runesEquiped.Length;

        if (runesEquiped[currentIndex] != null)
        {
            weaponBonusStats.SpreadAngleBonus -= runesEquiped[currentIndex].SpreadAngleBonus;
            weaponBonusStats.WeaponDelayBonus -= runesEquiped[currentIndex].WeaponDelayBonus;
            weaponBonusStats.BulletCountBonus -= runesEquiped[currentIndex].BulletCountBonus;
            weaponBonusStats.BulletSizeBonus -= runesEquiped[currentIndex].BulletSizeBonus;
            weaponBonusStats.DamageBonus -= runesEquiped[currentIndex].DamageBonus;
        }

        runesEquiped[currentIndex] = runeData;
        slotIndex++;

        weaponBonusStats.SpreadAngleBonus += runeData.SpreadAngleBonus;
        weaponBonusStats.WeaponDelayBonus += runeData.WeaponDelayBonus;
        weaponBonusStats.BulletCountBonus += runeData.BulletCountBonus;
        weaponBonusStats.BulletSizeBonus += runeData.BulletSizeBonus;
        weaponBonusStats.DamageBonus += runeData.DamageBonus;
    }
}

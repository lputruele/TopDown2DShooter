using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunes : MonoBehaviour
{
    [SerializeField]
    private WeaponBonusStats weaponBonusStats;
    [SerializeField]
    private BulletBonusStats bulletBonusStats;

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

            bulletBonusStats.BounceBonus -= runesEquiped[currentIndex].BounceBonus;
            bulletBonusStats.DamageBonus -= runesEquiped[currentIndex].DamageBonus;
            bulletBonusStats.FrictionBonus -= runesEquiped[currentIndex].FrictionBonus;
            bulletBonusStats.PiercingBonus -= runesEquiped[currentIndex].PiercingBonus;
            bulletBonusStats.SpeedBonus -= runesEquiped[currentIndex].SpeedBonus;
            bulletBonusStats.KnockbackBonus -= runesEquiped[currentIndex].KnockbackBonus;
        }

        runesEquiped[currentIndex] = runeData;
        slotIndex++;

        weaponBonusStats.SpreadAngleBonus += runeData.SpreadAngleBonus;
        weaponBonusStats.WeaponDelayBonus += runeData.WeaponDelayBonus;
        weaponBonusStats.BulletCountBonus += runeData.BulletCountBonus;
        weaponBonusStats.BulletSizeBonus += runeData.BulletSizeBonus;

        bulletBonusStats.BounceBonus += runesEquiped[currentIndex].BounceBonus;
        bulletBonusStats.DamageBonus += runesEquiped[currentIndex].DamageBonus;
        bulletBonusStats.FrictionBonus += runesEquiped[currentIndex].FrictionBonus;
        bulletBonusStats.PiercingBonus += runesEquiped[currentIndex].PiercingBonus;
        bulletBonusStats.SpeedBonus += runesEquiped[currentIndex].SpeedBonus;
        bulletBonusStats.KnockbackBonus += runesEquiped[currentIndex].KnockbackBonus;
    }
}

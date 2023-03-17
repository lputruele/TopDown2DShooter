using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBonusStats : MonoBehaviour
{

    [field: SerializeField]
    [field: Range(0, 2f)]
    public float WeaponDelayBonus { get; set; } = 0;

    [field: SerializeField]
    [field: Range(0, 30)]
    public float SpreadAngleBonus { get; set; } = 0;

    [field: SerializeField]
    [field: Range(0, 30)]
    public float BulletCountBonus { get; set; } = 0;

    [field: SerializeField]
    [field: Range(0, 10f)]
    public float BulletSizeBonus { get; set; } = 0;

    [field: SerializeField]
    [field: Range(0, 10f)]
    public float DamageBonus { get; set; } = 0;
}

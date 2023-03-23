using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBonusStats : MonoBehaviour
{
    [field: SerializeField]
    [field: Range(0, 2f)]
    public float KnockbackBonus { get; set; } = 0;

    [field: SerializeField]
    [field: Range(0, 30)]
    public float PiercingBonus { get; set; } = 0;

    [field: SerializeField]
    [field: Range(0, 30)]
    public float BounceBonus { get; set; } = 0;

    [field: SerializeField]
    [field: Range(0, 30)]
    public float SpeedBonus { get; set; } = 0;

    [field: SerializeField]
    [field: Range(0, 30)]
    public float FrictionBonus { get; set; } = 0;

    [field: SerializeField]
    [field: Range(0, 10)]
    public int DamageBonus { get; set; } = 0;

    [field: SerializeField]
    [field: Range(0, 10)]
    public int SplashDamageBonus { get; set; } = 0;

    [field: SerializeField]
    [field: Range(0, 10f)]
    public float SplashRadiusBonus { get; set; } = 0;

    [field: SerializeField]
    [field: Range(0, 10f)]
    public float ImpactRadiusBonus { get; set; } = 0;
}

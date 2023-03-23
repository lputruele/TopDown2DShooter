using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Runes/RuneData")]
public class RuneDataSO : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; set; }

    [field: SerializeField]
    public Sprite Icon { get; set; }

    [field: SerializeField]
    [field: Range(0, 2f)]
    public float WeaponDelayBonus { get; set; } = .1f;

    [field: SerializeField]
    [field: Range(0, 30)]
    public float SpreadAngleBonus { get; set; } = 5;

    [field: SerializeField]
    [field: Range(0, 30)]
    public float BulletCountBonus { get; set; } = 1;

    [field: SerializeField]
    [field: Range(0, 10f)]
    public float BulletSizeBonus { get; set; } = .1f;

    [field: SerializeField]
    [field: Range(0, 20f)]
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

    [field: SerializeField]
    [field: Range(0, 10f)]
    public float PlayerSpeedBonus { get; set; } = 0;

    [field: SerializeField]
    [field: Range(0, 10f)]
    public float PlayerAccelerationBonus { get; set; } = 0;

    [field: SerializeField]
    [field: Range(0, 10f)]
    public float PlayerVisionBonus { get; set; } = 0;
}

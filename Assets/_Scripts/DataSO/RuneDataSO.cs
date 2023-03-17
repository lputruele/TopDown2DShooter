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
    [field: Range(0, 10f)]
    public float DamageBonus { get; set; } = .1f;
}

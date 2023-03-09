using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Weapons/BulletData")]
public class BulletDataSO : ScriptableObject
{
    [field:SerializeField]
    public GameObject BulletPrefab { get; set; }

    [field: SerializeField]
    [field: Range(1, 100)]
    public float BulletSpeed { get; set; } = 1;

    [field: SerializeField]
    [field: Range(1, 10)]
    public int Damage { get; set; } = 1;

    [field: SerializeField]
    [field: Range(0, 100)]
    public float Friction { get; set; } = 0;

    [field: SerializeField]
    public bool Bounce { get; set; }

    [field: SerializeField]
    public bool GoThroughHittable { get; set; }

    [field: SerializeField]
    public bool IsRaycast { get; set; }

    [field: SerializeField]
    [field: Range(1, 20)]
    public float KnockbackPower { get; set; } = 5;

    [field: SerializeField]
    [field: Range(0.01f, 1f)]
    public float KnockbackTime { get; set; } = 0.1f;

    [field: SerializeField]
    public GameObject ImpactObstaclePrefab { get; set; }

    [field: SerializeField]
    public GameObject ImpactEnemyPrefab { get; set; }

    [field: SerializeField]
    public LayerMask BulletLayerMask { get; set; }
}

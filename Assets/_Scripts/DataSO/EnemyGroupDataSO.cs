using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/EnemyGroupData")]
public class EnemyGroupDataSO : ScriptableObject
{
    [field: SerializeField]
    public List<GameObject> Enemies { get; set; } 

    [field: SerializeField]
    public int EnemyCountMin { get; set; }

    [field: SerializeField]
    public int EnemyCountMax { get; set; }
}

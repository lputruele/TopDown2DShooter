using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level/LevelData")]
public class LevelDataSO : ScriptableObject
{
    [field: SerializeField]
    public List<EnemyGroupDataSO> LevelEnemyGroups { get; set; }

    [field: SerializeField]
    public DungeonDataSO DungeonData { get; set; }

    [field: SerializeField]
    public GameObject LevelBoss { get; set; }

    [field: SerializeField]
    public AudioClip LevelMusic { get; set; }
}

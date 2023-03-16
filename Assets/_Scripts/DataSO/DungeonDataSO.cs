using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dungeon/DungeonData")]
public class DungeonDataSO : ScriptableObject
{
    [field:SerializeField]
    public int MinRoomHeight { get; set; }

    [field: SerializeField]
    public int MinRoomWidth { get; set; }

    [field: SerializeField]
    public int DungeonHeight { get; set; }

    [field: SerializeField]
    public int DungeonWidth { get; set; }

    [field: SerializeField]
    public int Offset { get; set; }

    [field: SerializeField]
    public bool RandomWalkRooms { get; set; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    [field: SerializeField]
    public HashSet<Vector2Int> Floor { get; set; } = new HashSet<Vector2Int>();
    [SerializeField]
    public Vector2Int Center { get; set; } = new Vector2Int();

    [field:SerializeField]
    public RoomType RoomType { get; set; } = RoomType.None;


}

public enum RoomType
{
    None,
    Player,
    Enemy
}

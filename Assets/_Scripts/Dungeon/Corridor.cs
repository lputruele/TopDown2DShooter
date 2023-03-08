using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corridor : MonoBehaviour
{
    [field: SerializeField]
    public HashSet<Vector2Int> Floor { get; set; } = new HashSet<Vector2Int>();
}

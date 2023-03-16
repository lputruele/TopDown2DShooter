using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rooms/RoomData")]
public class RoomDataSO : ScriptableObject
{
    [field: SerializeField]
    public EnemyGroupDataSO EnemyGroupData { get; set; }
}

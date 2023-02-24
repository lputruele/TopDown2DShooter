using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    [field: SerializeField]
    public int MaxHealth { get; set; } = 5;
}

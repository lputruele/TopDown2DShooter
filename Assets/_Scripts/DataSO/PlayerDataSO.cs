using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    [field: SerializeField]
    public int MaxHealth { get; private set; } = 5;

    [field: SerializeField]
    public int MaxKeys { get; private set; } = 20;

    [field: SerializeField]
    public float GracePeriodDelay { get; private set; } = .2f;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMultipleAttacks : EnemyAttack
{
    [SerializeField]
    private List<EnemyAttack> enemyAttacks;
    public override void Attack(int damage)
    {
        enemyAttacks[Random.Range(0, enemyAttacks.Count)].Attack(damage);
    }
}

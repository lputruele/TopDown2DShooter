using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedSpawnAttack : EnemyAttack
{
    [SerializeField]
    private GameObject enemyObjectToSpawn;

    public override void Attack(int damage)
    {
        if (!waitBeforeNextAttack)
        {
            Instantiate(enemyObjectToSpawn, GetTarget().transform.position, Quaternion.identity);
            StartCoroutine(WaitBeforeAttackCoroutine());
        }
    }
}

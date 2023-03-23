using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : EnemyAttack
{
    [SerializeField]
    private EnemyWeapon enemyWeapon;

    [SerializeField]
    private bool isAimed;

    public override void Attack(int damage)
    {
        if (!waitBeforeNextAttack)
        {
            if (isAimed)
                enemyWeapon.AimWeapon(GetTarget().transform.position);
            enemyWeapon.Shoot();
            StartCoroutine(WaitBeforeAttackCoroutine());
        }
    }

}

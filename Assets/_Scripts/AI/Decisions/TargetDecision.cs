using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDecision : AIDecision
{
    Player playerTarget;

    private void Start()
    {
        playerTarget = enemyBrain.Target.GetComponent<Player>();
    }
    public override bool MakeDecision()
    {
        return !(playerTarget != null && playerTarget.IsDead);
    }

}

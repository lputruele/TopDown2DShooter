using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitDecision : AIDecision
{

    [field: SerializeField]
    public UnityEvent OnPlayerSpotted { get; set; }

    public override bool MakeDecision()
    {
        var direction = enemyBrain.Target.transform.position - transform.position;
        if (enemy.IsBeingHit)
        {
            OnPlayerSpotted?.Invoke();
            return true;
        }
        return false;
    }

}

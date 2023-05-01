using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TimeDecision : AIDecision
{
    private bool waiting;
    private bool decision;

    [SerializeField]
    private float minDecisionTime, maxDecisionTime;
    public override bool MakeDecision()
    {
        if (!waiting)
        {
            StartCoroutine(WaitBeforeDecision());
        }
        return decision;
    }

    IEnumerator WaitBeforeDecision()
    {
        waiting = true;
        yield return new WaitForSeconds(Random.Range(minDecisionTime, maxDecisionTime));
        decision = !decision;
        waiting = false;
    }
}

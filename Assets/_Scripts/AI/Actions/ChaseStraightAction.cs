using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChaseStraightAction : AIAction
{
    private int chaseState;

    [SerializeField]
    private float chaseDuration, waitDuration;

    [field: SerializeField]
    public UnityEvent OnPlayerSpotted { get; set; }

    public override void TakeAction()
    {
        if (chaseState == 0)
        {
            chaseState = 1;
            OnPlayerSpotted?.Invoke();
            StartCoroutine(ChooseDestinationCoroutine());
        }
        if (chaseState == 2)
        {
            chaseState = 1;
            StartCoroutine(WaitCoroutine());
        }
        if (chaseState == 1)
        {
            aiMovementData.PointOfInterest = (Vector2)transform.position + aiMovementData.Direction; // keep updating point of interest
        }
        enemyBrain.Move(aiMovementData.Direction, aiMovementData.PointOfInterest);
    }

    IEnumerator ChooseDestinationCoroutine()
    {
        var direction = enemyBrain.Target.transform.position - transform.position;
        aiMovementData.Direction = direction.normalized;
        aiMovementData.PointOfInterest = (Vector2)transform.position + aiMovementData.Direction;
        yield return new WaitForSeconds(chaseDuration);
        chaseState = 2;
    }

    IEnumerator WaitCoroutine()
    {
        aiMovementData.Direction = Vector2.zero;
        yield return new WaitForSeconds(waitDuration);
        chaseState = 0;
    }
}

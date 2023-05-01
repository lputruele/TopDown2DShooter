using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleAction : AIAction
{
    private int patrolState;

    [SerializeField]
    private float minWalkTime, maxWalkTime;
    [SerializeField]
    private float minStillTime, maxStillTime;

    public override void TakeAction()
    {
        if (patrolState == 0)
        {
            patrolState = 1;
            StartCoroutine(ChooseDestinationCoroutine());
        }
        if (patrolState == 2)
        {
            patrolState = 1;
            StartCoroutine(WaitCoroutine());
        }
        if (patrolState == 1)
        {
            aiMovementData.PointOfInterest = (Vector2)transform.position + aiMovementData.Direction; // keep updating point of interest
        }
        enemyBrain.Move(aiMovementData.Direction, aiMovementData.PointOfInterest);
    }

    IEnumerator ChooseDestinationCoroutine()
    {
        aiMovementData.Direction = Random.insideUnitCircle;
        aiMovementData.PointOfInterest = (Vector2)transform.position + aiMovementData.Direction;
        yield return new WaitForSeconds(Random.Range(minWalkTime, maxWalkTime));
        patrolState = 2;
    }

    IEnumerator WaitCoroutine()
    {
        aiMovementData.Direction = Vector2.zero;
        yield return new WaitForSeconds(Random.Range(minStillTime, maxStillTime));
        patrolState = 0;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeGameObject == gameObject)
        {
            Gizmos.color = Color.green;
            if (aiMovementData != null)
            {
                Gizmos.DrawLine((Vector2)transform.position, aiMovementData.PointOfInterest);
            }
            Gizmos.color = Color.white;
        }
    }
#endif
}

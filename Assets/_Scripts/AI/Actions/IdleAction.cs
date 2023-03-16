using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleAction : AIAction
{
    private int patrolState;

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
        yield return new WaitForSeconds(Random.Range(.2f,.3f));
        patrolState = 2;
    }

    IEnumerator WaitCoroutine()
    {
        aiMovementData.Direction = Vector2.zero;
        yield return new WaitForSeconds(Random.Range(1f, 2f));
        patrolState = 0;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeGameObject == gameObject)
        {
            Gizmos.color = Color.green;
            Debug.Log((Vector2)transform.position);
            Debug.Log(aiMovementData.PointOfInterest);
            Gizmos.DrawLine((Vector2)transform.position, aiMovementData.PointOfInterest);
            Gizmos.color = Color.white;
        }
    }
#endif
}

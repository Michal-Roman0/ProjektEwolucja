using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateGoingToMate : IState
{
    private Vector2 followingVector = Vector2.zero;

    public void OnEnter(StateController sc)
    {
        sc.StartCoroutine(fleeingTimer(sc));
        sc.rb.velocity *= 0;
    }

    public void UpdateState(StateController sc)
    {
        bool isSafe = CheckForMates(sc);
        if (!isSafe)
        {
            sc.ChangeState(sc.stateFleeing);
            return;
        }
        
        if (sc.detectedTargets.Count > 0)
        {
            GoToNearestMate(sc);
        }
        else
        {
            sc.ChangeState(sc.stateWandering);
        }
    }

    public void OnExit(StateController sc)
    {
        sc.detectedTargets.Clear();
    }

    private IEnumerator fleeingTimer(StateController sc)
    {
        yield return new WaitForSeconds(4);
        sc.ChangeState(sc.stateWandering);
    }

    private bool CheckForMates(StateController sc)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(sc.transform.position, sc.detectionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Herbivore"))
            {
                sc.detectedTargets.Add(collider.gameObject.transform.position);
            }
            if (collider.gameObject.CompareTag("Carnivore"))
            {
                return false;
            }
        }

        return true;
    }

    private void GoToNearestMate(StateController sc)
    {
        followingVector = sc.detectedTargets.OrderByDescending(
                mate => Vector2.Distance(mate, sc.rb.position))
            .First()
            .normalized;

        sc.rb.velocity = followingVector * sc.thisUnitController.maxSpeed; 
    }
    
}



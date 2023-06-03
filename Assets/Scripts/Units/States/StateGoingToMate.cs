using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateGoingToMate : IState
{
    private Vector2 escapeVector = Vector2.zero;

    public void OnEnter(StateController sc)
    {
        sc.StartCoroutine(fleeingTimer(sc));
        sc.rb.velocity *= 0;
    }

    public void UpdateState(StateController sc)
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
                sc.ChangeState(sc.stateFleeing);
                return;
            }
        }

        escapeVector = Vector2.zero;
        if (sc.detectedTargets.Count >= 1)
        {
            foreach (Vector2 enemy in sc.detectedTargets)
            {
                Vector2 difference = (enemy - sc.rb.position); 
                escapeVector += difference ; 
            }
            escapeVector.Normalize(); 
            sc.rb.velocity = escapeVector * sc.thisUnitController.maxSpeed; 
        }

    }

    public void OnExit(StateController sc)
    {
        sc.detectedTargets.Clear();
    }

    IEnumerator fleeingTimer(StateController sc)
    {
        yield return new WaitForSeconds(4);
        sc.ChangeState(sc.stateWandering);
    }
}



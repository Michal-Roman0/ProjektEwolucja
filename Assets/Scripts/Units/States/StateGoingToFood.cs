using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateGoingToFood : IState
{
    private Vector2 escapeVector = Vector2.zero;
    private const float R = 3f; // Maximum speed for fleeing

    public void OnEnter(StateController sc)
    {
        // Entry state logic
        sc.StartCoroutine(fleeingTimer(sc));
        sc.rb.velocity *= 0;


    }

    public void UpdateState(StateController sc)
    {
        // Check for carnivores
        Collider2D[] colliders = Physics2D.OverlapCircleAll(sc.transform.position, sc.detectionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Plant"))
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
                escapeVector += difference;
            }
            escapeVector.Normalize();
            sc.rb.velocity = escapeVector * sc.thisUnitController.maxSpeed;
        }

    }



    public void OnExit(StateController sc)
    {
        // Exit state logic
        // Clear the detected enemies list
        sc.detectedTargets.Clear();
        return;
    }

    IEnumerator fleeingTimer(StateController sc)
    {
        yield return new WaitForSeconds(4);
        sc.ChangeState(sc.stateWandering);

    }
}

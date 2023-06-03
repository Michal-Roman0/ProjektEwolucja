using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFleeing : IState
{
    private Vector2 escapeVector = Vector2.zero;
    private const float R = 3f; // Maximum speed for fleeing

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
            if (collider.gameObject.CompareTag("Carnivore"))
            {
                sc.detectedEnemies.Add(collider.gameObject.transform.position);
            }
        }

        escapeVector = Vector2.zero;
        if (sc.detectedEnemies.Count >= 1)
        {
            foreach (Vector2 enemy in sc.detectedEnemies)
            {
                Vector2 difference = (enemy - sc.rb.position); // reverse direction
                escapeVector -= difference / (difference.sqrMagnitude * 2); // subtract instead of add to further reverse direction
                escapeVector -= (new Vector2(-difference.y, difference.x)) * 0.001f;
            }
            
            escapeVector.Normalize(); // ensure the escape vector is a unit vector
            sc.rb.velocity = escapeVector *sc.thisUnitController.maxSpeed; // velocity with direction of escape vector and magnitude R
        }
    }
    
    public void OnExit(StateController sc)
    {
        sc.detectedEnemies.Clear();
    }

    IEnumerator fleeingTimer(StateController sc)
    {
        yield return new WaitForSeconds(4);
        sc.ChangeState(sc.stateWandering);
    }
}




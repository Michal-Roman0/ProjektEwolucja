using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFleeing : IState
{
    private Vector2 escapeVector = Vector2.zero;

    public void OnEnter(StateController sc)
    {
        sc.StartCoroutine(fleeingTimer(sc));
        sc.rb.velocity *= 0;
    }

    public void UpdateState(StateController sc)
    {
        CheckForEnemies(sc);

        escapeVector = Vector2.zero;
        if (sc.detectedEnemies.Count > 0)
        {
            CalculateEscapeVector(sc);
        }
        else
        {
            sc.ChangeState(sc.stateWandering);
        }
    }
    
    public void OnExit(StateController sc)
    {
        sc.detectedEnemies.Clear();
    }

    private IEnumerator fleeingTimer(StateController sc)
    {
        yield return new WaitForSeconds(4);
        sc.ChangeState(sc.stateWandering);
    }

    private void CheckForEnemies(StateController sc)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(sc.transform.position, sc.detectionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Carnivore"))
            {
                sc.detectedEnemies.Add(collider.gameObject.transform.position);
            }
        }
    }

    private void CalculateEscapeVector(StateController sc)
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






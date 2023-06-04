using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFleeing : IState
{
    public void OnEnter(StateController sc)
    {
        Debug.Log("Fleeing");
        sc.StartCoroutine(FleeingTimer(sc));
        sc.rb.velocity *= 0;
    }

    public void UpdateState(StateController sc)
    {
        if (sc.visibleEnemies.Count > 0)
        {
            SetEscapeVector(sc);
        }
        else
        {
            // sc.ChangeState(sc.stateWandering);
        }
    }
    
    public void OnExit(StateController sc)
    {

    }

    private IEnumerator FleeingTimer(StateController sc)
    {
        yield return new WaitForSeconds(4);
        sc.ChangeState(sc.stateWandering);
    }

    private void SetEscapeVector(StateController sc)
    {
        var escapeVector = Vector2.zero;
        foreach (GameObject enemy in sc.visibleEnemies)
        {
            Vector2 difference = (Vector2)enemy.transform.position - sc.rb.position; // reverse direction
            escapeVector -= difference / (difference.sqrMagnitude * 2); // subtract instead of add to further reverse direction
            escapeVector -= new Vector2(-difference.y, difference.x) * 0.001f;
        }
            
        escapeVector.Normalize();
        sc.rb.velocity = escapeVector * sc.thisUnitController.maxSpeed;
    }
}
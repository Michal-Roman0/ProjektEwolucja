using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;

public class StateGoingToMate : IState
{
    public void OnEnter(StateController sc)
    {
        Debug.Log("Going to mate");
        sc.StartCoroutine(GoingToMateTimer(sc));
        sc.rb.velocity *= 0;
    }

    public void UpdateState(StateController sc)
    {
        if (sc.visibleEnemies.Any())
        {
            sc.ChangeState(sc.stateFleeing);
            return;
        }
        
        if (!sc.visibleMates.Any())
        {
            sc.ChangeState(sc.stateWandering);
            return;
        }
        
        Vector2 closestMate = sc.visibleMates
            .OrderBy(mate => 
                Vector2.Distance(mate.transform.position, sc.rb.position))
            .First().transform.position;

        Vector2 followDirection = (closestMate - sc.rb.position).normalized;

        sc.rb.velocity = followDirection * sc.thisUnitController.normalSpeed;
    }

    public void OnExit(StateController sc)
    {

    }

    private IEnumerator GoingToMateTimer(StateController sc)
    {
        yield return new WaitForSeconds(4);
        sc.ChangeState(sc.stateWandering);
    }
}



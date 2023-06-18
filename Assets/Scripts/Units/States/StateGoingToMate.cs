using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;
using Utils;

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
        if (sc.thisUnitController.hungry)
        {
            sc.ChangeState(sc.stateWandering);
        }
        
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

        if (sc.visibleMates.Any())
        {
            sc.ChangeState(sc.stateMating);
        }


        CalculateGoingToMateVector(sc);
    }

    public void OnExit(StateController sc)
    {

    }

    private IEnumerator GoingToMateTimer(StateController sc)
    {
        yield return new WaitForSeconds(4);
        sc.ChangeState(sc.stateWandering);
    }

    private void CalculateGoingToMateVector(StateController sc)
    {
        Vector2 closestMate = sc.visibleMates
            .OrderBy(mate => 
                Vector2.Distance(mate.transform.position, sc.rb.position))
            .First().transform.position;

        Vector2 followDirection = (closestMate - sc.rb.position).normalized;
        float speedFactor = MapInfoUtils.GetTileDifficulty(sc.transform.position.x, sc.transform.position.y);

        sc.rb.velocity = followDirection * sc.thisUnitController.normalSpeed * speedFactor;
    }

    public override string ToString()
    {
        return "Going to Mate";
    }
}



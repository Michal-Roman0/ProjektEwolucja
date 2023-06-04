using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateGoingToFood : IState
{
    public void OnEnter(StateController sc)
    {
        Debug.Log("Going to food");
        sc.StartCoroutine(GoingToFoodTimer(sc));
        sc.rb.velocity *= Vector2.zero;
    }

    public void UpdateState(StateController sc)
    {
        if (!sc.visibleTargets.Any())
        {
            sc.ChangeState(sc.stateWandering);
            return;
        }

        Vector2 closestFood = sc.visibleTargets
            .OrderBy(food =>
                Vector2.Distance(sc.rb.position, food.transform.position))
            .First().transform.position;

        Vector2 foodDirection = (closestFood - sc.rb.position).normalized;

        sc.rb.velocity = foodDirection * sc.thisUnitController.normalSpeed;
    }

    public void OnExit(StateController sc)
    {
        
    }

    IEnumerator GoingToFoodTimer(StateController sc)
    {
        yield return new WaitForSeconds(4);
        sc.ChangeState(sc.stateWandering);
    }
}

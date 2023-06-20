using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

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
        CalculateGoingToFoodVector(sc);
    }

    public void OnExit(StateController sc)
    {
        
    }

    private void CalculateGoingToFoodVector(StateController sc)
    {
        if(sc.visibleTargets.Any()){
            Vector2 closestFood = sc.visibleTargets
                .OrderBy(food =>
                    Vector2.Distance(sc.rb.position, food.transform.position))
                .First().transform.position;

            Vector2 foodDirection = (closestFood - sc.rb.position).normalized;
            float speedFactor = MapInfoUtils.GetTileDifficulty(sc.transform.position.x, sc.transform.position.y);

            sc.rb.velocity = foodDirection * (sc.thisUnitController.normalSpeed * speedFactor);
        }
    }

    IEnumerator GoingToFoodTimer(StateController sc)
    {
        yield return new WaitForSeconds(1);

        if (sc.foodToEat != null){
            sc.ChangeState(sc.stateEating);
        }
        else if (sc.visibleEnemies.Any())
        {
            sc.ChangeState(sc.stateFleeing);
        }
        else if (!sc.visibleTargets.Any())
        {
            sc.ChangeState(sc.stateWandering);
        }
        else sc.StartCoroutine(GoingToFoodTimer(sc));
    }

    public override string ToString()
    {
        return "Going to Food";
    }
}

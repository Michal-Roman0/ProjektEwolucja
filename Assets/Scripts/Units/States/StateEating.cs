using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateEating: IState
{
    public void OnEnter(StateController sc)
    {
        Debug.Log("Eating");
        sc.StartCoroutine(EatingTimer(sc));
    }
    
    public void UpdateState(StateController sc)
    {
    }
    
    public void OnExit(StateController sc)
    {
        sc.thisUnitController.Hunger += sc.foodToEat.nutrition;
        sc.visibleTargets.Remove(sc.foodToEat.gameObject);
        sc.foodToEat.Eat();
        sc.foodToEat = null;
    }

    IEnumerator EatingTimer(StateController sc)
    {
        yield return new WaitForSeconds(4);
        // TODO tutaj usuwanie zjedzonego jedzenia
        // z ziemii
        sc.ChangeState(sc.stateWandering);
    }

    public override string ToString()
    {
        return "Eating";
    }
}

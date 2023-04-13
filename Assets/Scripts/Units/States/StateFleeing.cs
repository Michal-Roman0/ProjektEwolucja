using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFleeing: IState
{
    public void OnEnter(StateController sc)
    {
        //wejscie w stan
        Debug.Log("Fleeing started");
        sc.StartCoroutine(fleeingTimer(sc));
    }
    public void UpdateState(StateController sc)
    {
        ///algorytm wandering
    }
    public void OnExit(StateController sc)
    {
        //wyjcie z tego stanu
    }

    IEnumerator fleeingTimer(StateController sc)
    {
        yield return new WaitForSeconds(4);
        // TODO tutaj usuwanie zjedzonego jedzenia
        // z ziemii
        sc.ChangeState(sc.stateWandering);
    }
}

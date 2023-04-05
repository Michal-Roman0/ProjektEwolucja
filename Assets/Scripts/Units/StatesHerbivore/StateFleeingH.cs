using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFleeingH : IStateH
{
    public void OnEnter(StateControllerH sc)
    {
        //wejscie w stan
        Debug.Log("Fleeing started");
        sc.StartCoroutine(fleeingTimer(sc));
    }
    public void UpdateState(StateControllerH sc)
    {
        ///algorytm wandering
    }
    public void OnExit(StateControllerH sc)
    {
        //wyjcie z tego stanu
    }

    IEnumerator fleeingTimer(StateControllerH sc)
    {
        yield return new WaitForSeconds(4);
        // TODO tutaj usuwanie zjedzonego jedzenia
        // z ziemii
        sc.ChangeState(sc.stateWandering);
    }
}

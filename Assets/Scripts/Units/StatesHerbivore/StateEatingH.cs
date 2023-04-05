using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEatingH : IStateH
{
    public void OnEnter(StateControllerH sc)
    {
        //wejscie w stan
        Debug.Log("Eating Started");
        sc.StartCoroutine(eatingTimer(sc));
    }
    public void UpdateState(StateControllerH sc)
    {
        ///algorytm wandering
    }
    public void OnExit(StateControllerH sc)
    {
        //wyjcie z tego stanu
    }

    IEnumerator eatingTimer(StateControllerH sc)
    {
        yield return new WaitForSeconds(4);
        // TODO tutaj usuwanie zjedzonego jedzenia
        // z ziemii
        sc.ChangeState(sc.stateWandering);
    }
}

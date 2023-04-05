using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMatingH : IStateH
{
    public void OnEnter(StateControllerH sc)
    {
        //wejscie w stanWandering
        Debug.Log("Mating Started");
    }
    public void UpdateState(StateControllerH sc)
    {
        ///algorytm wandering
    }
    public void OnExit(StateControllerH sc)
    {
        //wyjcie z tego stanu
    }
    
    IEnumerator matingTimer(StateControllerH sc)
    {
        yield return new WaitForSeconds(4);
        // TODO tutaj wywołanie algorytmu kopulacji
        sc.ChangeState(sc.stateWandering);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMating : IState
{
    public void OnEnter(StateController sc)
    {
        //wejscie w stanWandering
        Debug.Log("Mating Started");
    }
    public void UpdateState(StateController sc)
    {

    }
    public void OnExit(StateController sc)
    {
        // spawnowanie nowej jednostki
    }
    
    IEnumerator matingTimer(StateController sc)
    {
        yield return new WaitForSeconds(4);
        // TODO tutaj wywołanie algorytmu kopulacji
        sc.ChangeState(sc.stateWandering);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateGoingToMate : IState
{
    public void OnEnter(StateController sc)
    {
        //wejscie w stan
        Debug.Log("GoingToMate Started");
    }
    public void UpdateState(StateController sc)
    {
        ///algorytm wandering
    }
    public void OnExit(StateController sc)
    {
        //wyjcie z tego stanu
    }
}

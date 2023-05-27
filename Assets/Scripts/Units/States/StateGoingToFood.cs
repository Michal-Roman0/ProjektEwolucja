using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateGoingToFood : IState
{
    public void OnEnter(StateController sc)
    {
        //wejscie w stan
        Debug.Log("GoingToFood Started");
    }
    public void UpdateState(StateController sc)
    {

    }
    public void OnExit(StateController sc)
    {
        //wyjcie z tego stanu
    }
}

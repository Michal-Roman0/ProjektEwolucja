using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWanderingH : IStateH
{
    public void OnEnter(StateControllerH sc)
    {
        //wejscie w stan
        Debug.Log("Wandering Started");
    }
    public void UpdateState(StateControllerH sc)
    {
        ///algorytm wanderingnemy!
    }
    public void OnExit(StateControllerH sc)
    {
        //wyjcie z tego stanu
    }
}

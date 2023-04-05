using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEatingH : IStateH
{
    public void OnEnter(StateControllerH sc)
    {
        //wejscie w stan
    }
    public void UpdateState(StateControllerH sc)
    {
        ///algorytm wandering
    }
    public void OnExit(StateControllerH sc)
    {
        //wyjcie z tego stanu
    }
}

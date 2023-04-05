using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateGoingToFoodH : IStateH
{
    public void OnEnter(StateControllerH sc)
    {
        //wejscie w stan
        Debug.Log("GoingToFood Started");
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

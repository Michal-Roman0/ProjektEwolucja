using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWandering : IState
{
    public void OnEnter(StateController sc)
    {
        //wejscie w stan
        Debug.Log("Wandering Started");
    }
    public void UpdateState(StateController sc)
    {
        ///algorytm wandering i wybieranie celu z listy celów
        
    }
    public void OnExit(StateController sc)
    {
        //wyjcie z tego stanu
    }
}

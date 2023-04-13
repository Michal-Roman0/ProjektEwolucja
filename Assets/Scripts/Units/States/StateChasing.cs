using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChasing: IState
{
    public void OnEnter(StateController sc)
    {
        //wejscie w stan
        Debug.Log("Chasing Started");
        sc.StartCoroutine(chasingTimer(sc));
    }
    public void UpdateState(StateController sc)
    {
        ///algorytm wandering
    }
    public void OnExit(StateController sc)
    {
        //wyjcie z tego stanu
    }

    IEnumerator chasingTimer(StateController sc)
    {
        yield return new WaitForSeconds(4);
        // TODO tutaj usuwanie zjedzonego jedzenia
        // z ziemii
        sc.ChangeState(sc.stateWandering);
    }
}

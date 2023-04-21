using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEating: IState
{
    public void OnEnter(StateController sc)
    {
        //wejscie w stan
        Debug.Log("Eating Started");
        sc.StartCoroutine(eatingTimer(sc));
    }
    public void UpdateState(StateController sc)
    {
    }
    public void OnExit(StateController sc)
    {
        //wyjcie z tego stanu
        //isuniecie obiektu zjedzonego
    }

    IEnumerator eatingTimer(StateController sc)
    {
        yield return new WaitForSeconds(4);
        // TODO tutaj usuwanie zjedzonego jedzenia
        // z ziemii
        sc.ChangeState(sc.stateWandering);
    }
}

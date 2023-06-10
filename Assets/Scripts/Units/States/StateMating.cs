using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMating : IState
{
    public void OnEnter(StateController sc)
    {
        Debug.Log("Mating Started");
        sc.StartCoroutine(MatingTimer(sc));
    }
    public void UpdateState(StateController sc)
    {
        
    }
    public void OnExit(StateController sc)
    {
        // currentEnergy - X
        // spawnowanie nowej jednostki
    }
    
    IEnumerator MatingTimer(StateController sc)
    {
        yield return new WaitForSeconds(4);
        // TODO tutaj wywołanie algorytmu kopulacji
        sc.ChangeState(sc.stateWandering);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMating : IState
{
    private GameObject child = null;

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
        Instantiate(chlid, sc.transform.position, sc.transform.rotation)
    }
    
    IEnumerator MatingTimer(StateController sc)
    {
        yield return new WaitForSeconds(4);
        // TODO tutaj wywołanie algorytmu kopulacji
        child = Procrate(thisUnitController, selectedTarget)
        sc.ChangeState(sc.stateWandering);
    }

    public override string ToString()
    {
        return "Mating";
    }


    private GameObject Procrate(firtsParent, secondParent)
    {
        Random rnd = new Random();
        int start = rnd.Next(7);
        int end = rnd.Next(7);

        if (end < start)
        {
            int tmp = end;
            end = start;
            start = tmp;

            GameObject tmpParent = firstParent;
            secondParent = firtsParent;
            firstParent = tmpParent;
        }

        float[] newBaseStats = {
            firstParent.baseStats.agility,
            firstParent.baseStats.strength,
            firstParent.baseStats.stealth,
            firstParent.baseStats.sight,
            firstParent.baseStats.sense,
            firstParent.baseStats.size,
            firstParent.baseStats.morality,
        };
        float[] secondBaseStats = {
            secondParent.baseStats.agility,
            secondParent.baseStats.strength,
            secondParent.baseStats.stealth,
            secondParent.baseStats.sight,
            secondParent.baseStats.sense,
            secondParent.baseStats.size,
            secondParent.baseStats.morality,
        };
        for (int i = start; i <= end; i++)
        {
            baseStats = secondBaseStats[i];
        }

        eatsMeat = firstParent.baseStats.eatsMeat;
        eatsPlants = firstParent.baseStats.eatsPlants;



    }

}


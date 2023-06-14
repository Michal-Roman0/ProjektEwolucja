using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        
        //Bardzo na szybko zorbione wyszukanie partnera
        GameObject closestTarget = sc.visibleMates.OrderBy(herbivore => Vector2.Distance(sc.rb.position, herbivore.transform.position)).First();
        GameObject child = Procrate(closestTarget, sc.gameObject);
        sc.ChangeState(sc.stateWandering);
    }

    public override string ToString()
    {
        return "Mating";
    }


    private GameObject Procrate(GameObject secondParent, GameObject firstParent)
    {

        int start = UnityEngine.Random.Range(0, 7);
        int end = UnityEngine.Random.Range(0, 7);


        if (end < start)
        {
            int tmp = end;
            end = start;
            start = tmp;

            GameObject tmpParent = firstParent;
            secondParent = firstParent;
            firstParent = tmpParent;
        }


        GameObject newChild = GameObject.Instantiate(firstParent) as GameObject;

        string[] names = {
            "agility",
            "strength",
            "stealth",
            "sight",
            "sense",
            "size",
            "morality",
        };
        for (int i = start; i <= end; i++)
        {
            string childName = "newChild.GetComponent<UnitController>().baseStats." + names[i];
            string parentName = "secondParent.GetComponent<UnitController>().baseStats." + names[i];

            Type type = parentName.GetType();

            var parentInfo = type.GetField(parentName, BindingFlags.Public | BindingFlags.Static);
            var parentValue = parentInfo.GetValue(null);


            type = parentName.GetType();
            var childInfo = type.GetField(childName, BindingFlags.Public | BindingFlags.Static);
            childInfo.SetValue(null, parentValue);

        }
        // Trzeba dodać mutację

        return newChild;

    }

}


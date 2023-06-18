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
        int x = 10;
        sc.thisUnitController.Hunger -= x;
    }

    IEnumerator MatingTimer(StateController sc)
    {
        yield return new WaitForSeconds(4);
        // TODO tutaj wywołanie algorytmu kopulacji

        //Bardzo na szybko zorbione wyszukanie partnera
        GameObject closestTarget = null;
        if (sc.gameObject.CompareTag("Carnivore")) {
            closestTarget = sc.visibleMates.OrderBy(carnivore => Vector2.Distance(sc.rb.position, carnivore.transform.position)).First();
        }
        if (sc.gameObject.CompareTag("Herbivore"))
        {
            closestTarget = sc.visibleMates.OrderBy(herbivore => Vector2.Distance(sc.rb.position, herbivore.transform.position)).First();
        }
        GameObject child = Procrate(closestTarget, sc.gameObject);
        sc.ChangeState(sc.stateWandering);
    }

    public override string ToString()
    {
        return "Mating";
    }


    private GameObject Procrate(GameObject secondParent, GameObject firstParent)
    {
        string[] names = {
            "agility",
            "strength",
            "sight",
            "size",
        };

        int start = UnityEngine.Random.Range(0, 5);
        int end = UnityEngine.Random.Range(0, 5);


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

        UnitController secondParentController = secondParent.GetComponent<UnitController>();
        UnitController chidController = newChild.GetComponent<UnitController>();

        // agility
        float mutation = 0.4f;
        if (start <= 0 && 0 <= end)
        {
            chidController.baseStats.agility = secondParentController.baseStats.agility;
        }
        if (UnityEngine.Random.Range(0.0f, 1.0f) < mutation) {
            float change = 1.0f - UnityEngine.Random.Range(-mutation, mutation);
            chidController.baseStats.agility = chidController.baseStats.agility * change;
        }
        if (start <= 1 && 1<= end)
        {
            chidController.baseStats.strength = secondParentController.baseStats.strength;
        }
        if (UnityEngine.Random.Range(0.0f, 1.0f) < mutation)
        {
            float change = 1.0f - UnityEngine.Random.Range(-mutation, mutation);
            chidController.baseStats.strength = chidController.baseStats.strength * change;
        }
        if (start <= 2 && 2 <= end)
        {
            chidController.baseStats.sight = secondParentController.baseStats.sight;
        }
        if (UnityEngine.Random.Range(0.0f, 1.0f) < mutation)
        {
            float change = 1.0f - UnityEngine.Random.Range(-mutation, mutation);
            chidController.baseStats.sight = chidController.baseStats.sight * change;
        }
        if (start <= 3 && 3 <= end)
        {
            chidController.baseStats.size = secondParentController.baseStats.size;
        }
        if (UnityEngine.Random.Range(0.0f, 1.0f) < mutation)
        {
            float change = 1.0f - UnityEngine.Random.Range(-mutation, mutation);
            chidController.baseStats.size = chidController.baseStats.size * change;
        }
        return newChild;

        /* Tu była próba zrobienia ładnie, po redukcji ilości cech ify są pewnie szybsze i znacznie mniej zawodne
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

        float mutation = 0.4f;
        
        for (int i = 0; i < names.Length; i++)
        {
            float change = 1.0f - UnityEngine.Random.Range(-mutation, mutation);
            string childName = "newChild.GetComponent<UnitController>().baseStats." + names[i];
            Type type = childName.GetType();
            var childInfo = type.GetField(childName, BindingFlags.Public | BindingFlags.Static);
            var childValue = childInfo.GetValue(null);
            Debug.Log("to jest wartosc" + childValue);
            childInfo.SetValue(null, childValue);

        }
        return newChild;
        */
    }

}


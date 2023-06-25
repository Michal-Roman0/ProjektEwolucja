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

    }

    IEnumerator MatingTimer(StateController sc)
    {
        yield return new WaitForSeconds(4);
        // TODO tutaj wywołanie algorytmu kopulacji

        //Bardzo na szybko zorbione wyszukanie partnera
        GameObject closestTarget = null;
        if (sc.gameObject.CompareTag("Carnivore") && sc.visibleMates.Count > 0) {
            closestTarget = sc.visibleMates.OrderBy(carnivore => Vector2.Distance(sc.rb.position, carnivore.transform.position)).First();
        }
        else if (sc.gameObject.CompareTag("Herbivore") && sc.visibleMates.Count > 0)
        {
            closestTarget = sc.visibleMates.OrderBy(herbivore => Vector2.Distance(sc.rb.position, herbivore.transform.position)).First();
        }
        if (closestTarget != null) {
            GameObject child = Procreate(closestTarget, sc.gameObject);
            sc.thisUnitController.Hunger -= sc.thisUnitController.maxEnergy*30;
            sc.ChangeState(sc.stateWandering);
        }
    }

    public override string ToString()
    {
        return "Mating";
    }


    private GameObject Procreate(GameObject secondParent, GameObject firstParent)
    {
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
        UnitController childController = newChild.GetComponent<UnitController>();

        if (start <= 0 && 0 <= end) childController.baseStats.agility = secondParentController.baseStats.agility;
        if (start <= 1 && 1 <= end) childController.baseStats.strength = secondParentController.baseStats.strength;
        if (start <= 2 && 2 <= end) childController.baseStats.sight = secondParentController.baseStats.sight;
        if (start <= 3 && 3 <= end) childController.baseStats.size = secondParentController.baseStats.size;

        float mutationProbability = Simulation_Controller.instance.mutationProbability;
        float mutationFactor = 0.4f;
        float agility = childController.baseStats.agility;
        float strength = childController.baseStats.strength;
        float sight = childController.baseStats.sight;
        float size = childController.baseStats.size;

        if (UnityEngine.Random.Range(0.0f, 1.0f) < mutationProbability) agility *= 1.0f + UnityEngine.Random.Range(-mutationFactor, mutationFactor);
        if (UnityEngine.Random.Range(0.0f, 1.0f) < mutationProbability) strength *= 1.0f + UnityEngine.Random.Range(-mutationFactor, mutationFactor);
        if (UnityEngine.Random.Range(0.0f, 1.0f) < mutationProbability) sight *= 1.0f + UnityEngine.Random.Range(-mutationFactor, mutationFactor);
        if (UnityEngine.Random.Range(0.0f, 1.0f) < mutationProbability) size *= 1.0f + UnityEngine.Random.Range(-mutationFactor, mutationFactor);

        childController.ReloadStats(agility, strength, sight, size);

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


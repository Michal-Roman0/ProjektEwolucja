using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController: MonoBehaviour
{
    IState currentState;
    public StateWandering stateWandering;
    public StateGoingToFood stateGoingToFood;
    public StateGoingToMate stateGoingToMate;
    public StateEating stateEating;
    public StateMating stateMating;
    public StateFleeing stateFleeing;
    public StateChasing stateChasing;

    // referencje do celów, czyli jedzenia albo ofiary którą goni
    public HashSet<Transform> detectedTargets = new HashSet<Transform>();
    // referencje do pozycji wszyskich przeciwników (dla roślinożery)
    public HashSet<Transform> detectedEnemies = new HashSet<Transform>();
    // cel który jednostka wybrała z listy
    public Transform selectedTarget;

    void Start()
    {
        stateWandering = new StateWandering();
        stateGoingToFood = new StateGoingToFood();
        stateGoingToMate = new StateGoingToMate();
        stateEating = new StateEating();
        stateMating = new StateMating();
        stateFleeing = new StateFleeing();
        stateChasing = new StateChasing();

        // jednostka zaczyna w stanie wandering
        ChangeState(stateWandering);
    }

    void Update()
    {
        if(currentState != null){
            currentState.UpdateState(this);
        }
    }

    public void ChangeState(IState nextState)
    {
        if(currentState != nextState){
            if (currentState != null)
            {
                currentState.OnExit(this);
            }
            currentState = nextState;
            currentState.OnEnter(this);
        }
    }

    // Funkcja kontrolująca przechodzenie w stany
    // Uruchamia się gdy zagrożenie/cel wejdzie w zasięg wzroku
    private void OnTriggerEnter2D(Collider2D col)
    {
        // funcjonalnosc dla roślinożerców
        if(gameObject.tag=="Herbivore"){
            // Jeśli znajdzie mięsożercę, ucieka
            if(col.gameObject.tag=="Carnivore"){
                Debug.Log("Detected a Enemy!");
                detectedEnemies.Add(col.gameObject.transform);
                ChangeState(stateFleeing);
            }
        }

        // funkcjonalność dla mięsożerców
        if(gameObject.tag=="Carnivore"){
            // jeśli znajdzie rośliżercę, goni go
            if(col.gameObject.tag=="Herbivore"){
                Debug.Log("Detected a prey!");
                detectedTargets.Add(col.gameObject.transform);
                ChangeState(stateChasing);
            }
        }

        //wybieranie co jednostka chce zrobić
        foreach(Transform target in detectedTargets)
        {
            // selectedTarget = ?
        }
        
        
    } 
    private void OnTriggerExit2D(Collider2D col)
    {
        if(gameObject.tag=="Herbivore")
        {
            if(col.gameObject.tag=="Carnivore")
            {
                detectedEnemies.Remove(col.gameObject.transform);
            }
            else
            {
                detectedTargets.Remove(col.gameObject.transform);
            }
        }
        // żaden miesożerca nie ucieka, więc nie używa detectedEnemies
        if(gameObject.tag=="Carnivore")
        {
            detectedTargets.Remove(col.gameObject.transform);
        }
    }
}

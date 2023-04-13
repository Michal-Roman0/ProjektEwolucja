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
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = nextState;
        currentState.OnEnter(this);
    }

    // Funkcja wymuszająca ucieczkę niezależnie od sytuacji
    // Gdy zagrożenie wejdzie w zasięg wzroku
    void OnTriggerEnter2D(Collider2D col)
    {
        // funcjonalnosc dla roślinożerców
        if(gameObject.tag=="Herbivore"){

            // W tym miejscu powinna być podejmowana decyzja
            // na podstawie priorytetu

            // Jeśli znajdzie mięsożercę, ucieka
            if(col.gameObject.tag=="Carnivore"){
                Debug.Log("Detected a Enemy!");
                ChangeState(stateFleeing);
            }
        }

        // funkcjonalność dla mięsożerców
        if(gameObject.tag=="Carnivore"){

            // W tym miejscu powinna być podejmowana decyzja
            // na podstawie priorytetu

            // jeśli znajdzie rośliżercę, goni go
            if(col.gameObject.tag=="Herbivore"){
                Debug.Log("Detected a prey!");
                ChangeState(stateChasing);
            }
        }
        
    } 
}

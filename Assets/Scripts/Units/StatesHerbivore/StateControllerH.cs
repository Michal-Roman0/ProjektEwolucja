using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateControllerH : MonoBehaviour
{
    IStateH currentState;
    public StateWanderingH stateWandering;
    public StateGoingToFoodH stateGoingToFood;
    public StateGoingToMateH stateGoingToMate;
    public StateEatingH stateEating;
    public StateMatingH stateMating;
    public StateFleeingH stateFleeing;

    void Start()
    {
        stateWandering = new StateWanderingH();
        stateGoingToFood = new StateGoingToFoodH();
        stateGoingToMate = new StateGoingToMateH();
        stateEating = new StateEatingH();
        stateMating = new StateMatingH();
        stateFleeing = new StateFleeingH();

        ChangeState(stateWandering);
    }

    void Update()
    {
        if(currentState != null){
            currentState.UpdateState(this);
        }
    }

    public void ChangeState(IStateH nextState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = nextState;
        currentState.OnEnter(this);
    }
}

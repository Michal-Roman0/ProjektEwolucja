using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    IState currentState;

    public Rigidbody2D rb;
    public int detectionRadius = 10;
    
    public StateWandering stateWandering;
    public StateGoingToFood stateGoingToFood;
    public StateGoingToMate stateGoingToMate;
    public StateEating stateEating;
    public StateMating stateMating;
    public StateFleeing stateFleeing;
    public StateChasing stateChasing;

    // referencje do celou, czyli jedzenia albo ofiary którą goni
    public LinkedList<GameObject> visibleEnemies = new();
    public LinkedList<GameObject> visibleTargets = new();
    public LinkedList<GameObject> visibleMates = new();
    //  zapewnia dostęp do info o jednostce
    public UnitController thisUnitController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        thisUnitController = gameObject.GetComponent<UnitController>();

        stateWandering = new StateWandering();
        stateGoingToFood = new StateGoingToFood();
        stateGoingToMate = new StateGoingToMate();
        stateEating = new StateEating();
        stateMating = new StateMating();
        stateFleeing = new StateFleeing();
        stateChasing = new StateChasing();

        ChangeState(stateWandering);
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
    }

    public void ChangeState(IState nextState)
    {
        if (currentState != nextState)
        {
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
        if (gameObject.CompareTag("Herbivore"))
        {
            if (col.gameObject.CompareTag("Carnivore"))
            {
                visibleEnemies.AddLast(col.gameObject);
            }
            else if(col.gameObject.CompareTag("Herbivore") /* && isSuitableMate*/)
            {
                visibleMates.AddLast(col.gameObject);
                ChangeState(stateGoingToMate);
            }
            else if (col.gameObject.CompareTag("Plant"))
            {
                visibleTargets.AddLast(col.gameObject);
                ChangeState(stateGoingToFood);
            }
        }

        else if (gameObject.CompareTag("Carnivore"))
        {
            if (col.gameObject.CompareTag("Herbivore"))
            {
                visibleTargets.AddLast(col.gameObject);
            }

            else if (col.gameObject.CompareTag("Carnivore"))
            {
                if (true /* isSuitableMate */)
                {
                    visibleMates.AddLast(col.gameObject);
                }
                else if (true /*&& myThreat < otherTrhreat*/)
                {
                    visibleEnemies.AddLast(col.gameObject);
                }
                else
                {
                    visibleTargets.AddLast(col.gameObject);
                }
            }
        }
    }


    private void OnTriggerExit2D(Collider2D col)
    {
        if (gameObject.CompareTag("Herbivore"))
        {
            if (col.gameObject.CompareTag("Carnivore"))
            {
                visibleEnemies.Remove(col.gameObject);
            }
            else if(col.gameObject.CompareTag("Herbivore") /* && isSuitableMate*/)
            {
                visibleMates.Remove(col.gameObject);
                ChangeState(stateGoingToMate);
            }
            else if (col.gameObject.CompareTag("Plant"))
            {
                visibleTargets.Remove(col.gameObject);
                ChangeState(stateGoingToFood);
            }
        }

        else if (gameObject.CompareTag("Carnivore"))
        {
            if (col.gameObject.CompareTag("Herbivore"))
            {
                visibleTargets.Remove(col.gameObject);
            }

            else if (col.gameObject.CompareTag("Carnivore"))
            {
                if (true /* isSuitableMate */)
                {
                    visibleMates.Remove(col.gameObject);
                }
                else if (true /*&& myThreat < otherTrhreat*/)
                {
                    visibleEnemies.Remove(col.gameObject);
                }
                else
                {
                    visibleTargets.Remove(col.gameObject);
                }
            }
        }
    }
}

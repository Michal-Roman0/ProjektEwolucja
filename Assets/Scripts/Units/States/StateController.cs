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





    // referencje do celów, czyli jedzenia albo ofiary którą goni
    public HashSet<Vector2> detectedTargets = new HashSet<Vector2>();
    // referencje do pozycji wszyskich przeciwników (dla roślinożery)
    public HashSet<Vector2> detectedEnemies = new HashSet<Vector2>();
    // cel który jednostka wybrała z listy
    public Transform selectedTarget;
    //  zapewnia dostęp do info o jednostce
    public UnitController thisUnitController;



    public Tilemap_Controller tilemapController;

    void Start()
    {// selectedTarget = ?

        GameObject mapObject = GameObject.FindWithTag("Ground");

        Tilemap_Controller tilemapcontroller = mapObject.GetComponent<Tilemap_Controller>();

        MapTile[,] mapTiles = tilemapcontroller.mapTiles;



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

    //// Funkcja kontrolująca przechodzenie w stany
    //// Uruchamia się gdy zagrożenie/cel wejdzie w zasięg wzroku
    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    // funcjonalnosc dla roślinożerców
    //    if (gameObject.CompareTag("Herbivore"))
    //    {
    //        // Jeśli znajdzie mięsożercę, ucieka
    //        if (col.gameObject.CompareTag("Carnivore"))
    //        {

    //            detectedEnemies.Add(col.gameObject.transform.position); // Store the 2D position of the enemy

    //            // wymuszenie ucieczki
    //            ChangeState(stateFleeing);
    //        }
    //        else if(col.gameObject.CompareTag("Herbivore"))
    //        {
    //            // If it finds another Herbivore, move towards it
    //            if (col.gameObject.CompareTag("Herbivore"))
    //            {
    //                detectedTargets.Add(col.gameObject.transform.position);
    //                // transition to the new state
    //                ChangeState(stateGoingToMate);
    //            }

    //            detectedTargets.Add(col.gameObject.transform.position);
    //        }
    //    }

    //    // funkcjonalność dla mięsożerców
    //    else if (gameObject.CompareTag("Carnivore"))
    //    {
    //        // if it finds a herbivore, it chases it
    //        if (col.gameObject.CompareTag("Herbivore"))
    //        {

    //            detectedTargets.Add(col.gameObject.transform.position);
    //            // forcing to chase
    //            ChangeState(stateChasing);
    //        }
    //    }

    //    //wybieranie co jednostka chce zrobić


    //}



    //// Adjusted OnTriggerExit2D function
    //private void OnTriggerExit2D(Collider2D col)
    //{
    //    if (gameObject.CompareTag("Herbivore"))
    //    {
    //        if (col.gameObject.CompareTag("Carnivore"))
    //        {
    //            detectedEnemies.Remove(col.gameObject.transform.position);
    //        }
    //        //else
    //        //{
    //        //    detectedTargets.Remove(col.gameObject.transform.position);
    //        //}
    //    }

    //    else if(gameObject.CompareTag("Carnivore"))
    //    {
    //        if (col.gameObject.CompareTag("Herbivore"))
    //        {
    //            detectedTargets.Remove(col.gameObject.transform.position);
    //        }
    //        //else
    //        //{
    //        //    detectedTargets.Remove(col.gameObject.transform.position);
    //        //}
    //    }

    //    // target selection must be repeated if the current target just disappeared from the list:
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController: MonoBehaviour
{
    [SerializeField]
    string stateString;
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
    //  zapewnia dostęp do info o jednostce
    private UnitController thisUnitController;


    // ZMIENNE DO ATAKU
    public float attackCooldown = 2.0f;
    public bool attackAvailable = true;

    void Start()
    {// selectedTarget = ?
        thisUnitController = gameObject.GetComponent<UnitController>();

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
            stateString = nextState.ToString();
            currentState = nextState;
            currentState.OnEnter(this);
        }
    }
    
    //Funkcja sprawdza kolizje z innym obiektem i
    //wywołuje wszystkie funkcje które powinny się wywołać po kolizji.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AttackEnemy(collision);
    }

    // Funkcja kontrolująca przechodzenie w stany
    // Uruchamia się gdy zagrożenie/cel wejdzie w zasięg wzroku
    private void OnTriggerEnter2D(Collider2D col)
    {
        // funcjonalnosc dla roślinożerców
        if(gameObject.CompareTag("Herbivore")){
            // Jeśli znajdzie mięsożercę, ucieka
            if(col.gameObject.CompareTag("Carnivore")){
                Debug.Log("Detected an Enemy!");
                detectedEnemies.Add(col.gameObject.transform);
                // wymuszenie ucieczki
                ChangeState(stateFleeing);
            }
            else
            {
                Debug.Log("Detected food or potential mate!");
                detectedTargets.Add(col.gameObject.transform);
            }
        }

        // funkcjonalność dla mięsożerców
        else{
            // jeśli znajdzie rośliżercę, goni go
            if(col.gameObject.CompareTag("Herbivore")){
                Debug.Log("Detected a prey!");
                detectedTargets.Add(col.gameObject.transform);
            }
        }

        //wybieranie co jednostka chce zrobić
        SelectTarget();
        
        
    } 
    private void OnTriggerExit2D(Collider2D col)
    {
        if(gameObject.CompareTag("Herbivore"))
        {
            if(col.gameObject.CompareTag("Carnivore"))
            {
                detectedEnemies.Remove(col.gameObject.transform);
            }
            else
            {
                detectedTargets.Remove(col.gameObject.transform);
            }
        }
        // żaden miesożerca nie ucieka, więc nie używa detectedEnemies
        else
        {
            detectedTargets.Remove(col.gameObject.transform);
        }

        // wybor celu musi zostac powtorzony, gdyby aktualny cel wlasnie zniknął z listy:
        SelectTarget();
    }
    
    // wybieranie celu z listy celow
    private void SelectTarget()
    {
        string desiredTargetTag = "None";
        if(thisUnitController.hungry)
        {
            desiredTargetTag = "Food";
        }
        else if(thisUnitController.readyToMate)
        {
            desiredTargetTag= gameObject.tag;
        }
        foreach(Transform target in detectedTargets)
        {
            if(target.CompareTag(desiredTargetTag))
            {
                selectedTarget = target;
                break;
            }
        }
    }

    //Funkcja wykonująca atak na przeciwniku
    void AttackEnemy(Collision2D collision)
    {
        if (!attackAvailable) return;
        if (currentState == stateChasing)
        {
            if (selectedTarget == collision.gameObject.transform)
            {
                float dmg = gameObject.GetComponent<UnitController>().derivativeStats.Damage;
                collision.gameObject.GetComponent<Health>().Damage(Mathf.CeilToInt(dmg));
                StartCoroutine(CooldownAttack());
            }
        }
    }
    //Funkcja do odliczania cooldownu ataku
    IEnumerator CooldownAttack()
    {
        attackAvailable = false;
        yield return new WaitForSeconds(attackCooldown);
        attackAvailable = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Serialization;

public class StateController : MonoBehaviour
{
    IState currentState;
    public string currentStateName;

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
    public HashSet<GameObject> visibleEnemies = new();
    public HashSet<GameObject> visibleTargets = new();
    public HashSet<GameObject> visibleMates = new();
    public Foodcon foodToEat;
    
    //  zapewnia dostęp do info o jednostce
    public UnitController thisUnitController;


    // ZMIENNE DO ATAKU
    public float attackCooldown = 2.0f;
    public bool attackAvailable = true;
    public float knockbackForce = 3f;

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
        visibleTargets.RemoveWhere((GameObject obj) => obj == null);
        visibleEnemies.RemoveWhere((GameObject obj) => obj == null);
        visibleMates.RemoveWhere((GameObject obj) => obj == null);

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
        UpdateCurrentStateName();
    }
    
    //Funkcja sprawdza kolizje z innym obiektem i
    //wywołuje wszystkie funkcje które powinny się wywołać po kolizji.
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Herbivore")) AttackEnemy(collision);        
        else if(gameObject.CompareTag("Carnivore") && collision.gameObject.CompareTag("Meat")) 
        {
            foodToEat = collision.gameObject.GetComponent<Foodcon>();
            ChangeState(stateGoingToFood);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if (gameObject.CompareTag("Herbivore") && collision.gameObject.CompareTag("Plant"))
        // {
        //     foodToEat = collision.gameObject.GetComponent<Foodcon>();
        // }
        /* else */if (gameObject.CompareTag("Carnivore") && collision.gameObject.CompareTag("Meat"))
        {
            foodToEat = collision.gameObject.GetComponent<Foodcon>();
        }
        if (gameObject.CompareTag("Herbivore") && collision.gameObject.CompareTag("Herbivore"))
        {
            ChangeState(stateMating);
        }
        if (gameObject.CompareTag("Carnivore") && currentState == stateGoingToMate && collision.gameObject.CompareTag("Carnivore"))
        {
            ChangeState(stateMating);
        }
    }

    // Funkcja kontrolująca przechodzenie w stany
    // Uruchamia się gdy zagrożenie/cel wejdzie w zasięg wzroku
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (gameObject.CompareTag("Herbivore"))
        {
            if (col.gameObject.CompareTag("Herbivore") && IsSuitableMate(col.gameObject))
            {
                visibleMates.Add(col.gameObject);
                ChangeState(stateGoingToMate);
            }
            else if(col.gameObject.CompareTag("Carnivore"))
            {
                visibleEnemies.Add(col.gameObject);
            }
            
            // else if (col.gameObject.CompareTag("Plant"))
            // {
            //     visibleTargets.Add(col.gameObject);
            // }
        }

        else if (gameObject.CompareTag("Carnivore"))
        {
            // Dodać więcej tego typu warunków żeby nie było sytuacji, że lista posiada dużo kopii tego samego obiektu
            if (col.gameObject.CompareTag("Herbivore") && !visibleTargets.Contains(col.gameObject))
            {
                visibleTargets.Add(col.gameObject);
            }

            else if (col.gameObject.CompareTag("Meat"))
            {
                visibleTargets.Add(col.gameObject);
            }

            else if (col.gameObject.CompareTag("Carnivore"))
            {
                if (IsSuitableMate(col.gameObject))
                {
                    visibleMates.Add(col.gameObject);
                    ChangeState(stateGoingToMate);
                }
                else if (thisUnitController.derivativeStats.Threat < col.gameObject.GetComponent<UnitController>().derivativeStats.Threat)
                {
                    visibleEnemies.Add(col.gameObject);
                }
                else
                {
                    visibleTargets.Add(col.gameObject);
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
            else if(col.gameObject.CompareTag("Herbivore"))
            {
                visibleMates.Remove(col.gameObject);
            }
            // else if (col.gameObject.CompareTag("Plant"))
            // {
            //     visibleTargets.Remove(col.gameObject);
            //     ChangeState(stateGoingToFood);
            // }
        }

        else if (gameObject.CompareTag("Carnivore"))
        {
            if (col.gameObject.CompareTag("Herbivore"))
            {
                visibleTargets.Remove(col.gameObject);
            }

            else if (col.gameObject.CompareTag("Carnivore"))
            {
                if (IsSuitableMate(col.gameObject))
                {
                    visibleMates.Remove(col.gameObject);
                }
                else if (thisUnitController.derivativeStats.Threat < col.gameObject.GetComponent<UnitController>().derivativeStats.Threat)
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

    bool IsSuitableMate(GameObject potentialMate)
    {
        UnitController mateController = potentialMate.GetComponent<UnitController>();

        bool correctAges = thisUnitController.age > 10 && mateController.age > 10;
        bool theyHungry = thisUnitController.hungry || mateController.hungry;
        float prob = CalculateProbMating(mateController);

        return correctAges && !theyHungry  && (prob > 0.0f);
    }
    private float CalculateProbMating(UnitController mateController)
    {

        FieldInfo[] fields1 = thisUnitController.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
        FieldInfo[] fields2 = mateController.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

        float SumDiff = 0;
        float SumAll = 0;

        for (int i = 0; i < fields1.Length; i++)
        {
            object value1 = fields1[i].GetValue(thisUnitController);
            object value2 = fields2[i].GetValue(mateController);

            if (value1 is float && value2 is float)
            {
                float stat1 = (float)value1;
                float stat2 = (float)value2;

                SumDiff += Mathf.Abs(stat1 - stat2);
                SumAll += (stat1 + stat2);
            }
        }

        float ProbMating = 1 - SumDiff / SumAll;

        return ProbMating;
    }

    //Funkcja wykonująca atak na przeciwniku
    void AttackEnemy(Collision2D collision)
    {
        if (!attackAvailable) return;
        if (currentState == stateChasing)
        {
            if (visibleTargets.Contains(collision.gameObject))
            {
                float dmg = gameObject.GetComponent<UnitController>().derivativeStats.Damage;
                /*collision.gameObject.GetComponent<Rigidbody2D>().AddForce(
                    (transform.position - collision.transform.position).normalized * knockbackForce,
                    ForceMode2D.Impulse);*/
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
    private void UpdateCurrentStateName()
    {
        currentStateName = currentState.ToString();
    }
}

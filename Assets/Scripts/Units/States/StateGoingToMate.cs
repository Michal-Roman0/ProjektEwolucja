using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateGoingToMate : IState
{
<<<<<<< Updated upstream
    private Vector2 followingVector = Vector2.zero;
=======

    public bool dedicated = false;
>>>>>>> Stashed changes

    public void OnEnter(StateController sc)
    {
        sc.StartCoroutine(fleeingTimer(sc));
        sc.rb.velocity *= 0;
    }

    public void UpdateState(StateController sc)
    {
<<<<<<< Updated upstream
        bool isSafe = CheckForMates(sc);
        if (!isSafe)
        {
            sc.ChangeState(sc.stateFleeing);
            return;
        }
        
        if (sc.detectedTargets.Count > 0)
        {
            GoToNearestMate(sc);
        }
        else
        {
            sc.ChangeState(sc.stateWandering);
        }
=======
        if(dedicated == false && sc.visibleMates.Any())
        {
            float probMating = CalculateProbMating(sc);
            if (probMating > UnityEngine.Random.Range(0f, 1f))
            {
                Debug.Log("dedicated to follow potentail partner");
                dedicated = true;
            }
            else // Odechciewa mu siê partnerów przez pora¿ki i wraca do wêdrówki
            {
                sc.ChangeState(sc.stateWandering);
                return;
            }
        }

        CalculateGoingToMateVector(sc);


>>>>>>> Stashed changes
    }

    public void OnExit(StateController sc)
    {
        sc.detectedTargets.Clear();
    }

    private IEnumerator fleeingTimer(StateController sc)
    {
        yield return new WaitForSeconds(4);
        sc.ChangeState(sc.stateWandering);
    }

<<<<<<< Updated upstream
    private bool CheckForMates(StateController sc)
=======

    private float CalculateProbMating(StateController sc)
    {
        if (sc.visibleMates.Any())
        {
            GameObject closestMateObj = sc.visibleMates
                .OrderBy(mate => Vector2.Distance(mate.transform.position, sc.rb.position)).First();

            UnitController my_ucontroller = sc.GetComponent<UnitController>();
            UnitController her_ucontroller = closestMateObj.GetComponent<UnitController>();

            FieldInfo[] fields1 = typeof(my_ucontroller).GetFields(BindingFlags.Public | BindingFlags.Instance);
            FieldInfo[] fields2 = typeof(her_ucontroller).GetFields(BindingFlags.Public | BindingFlags.Instance);


            float SumDiff = 0;
            float SumAll = 0;

            for (int i = 0; i < fields1.Length; i++)
            {
                var stat1 = (float)fields1[i].GetValue(my_ucontroller);
                var stat2 = (float)fields2[i].GetValue(her_ucontroller);

                SumDiff += Mathf.Abs(stat1 - stat2);
                SumAll += (stat1 + stat2);
            }

            float ProbMating = Mathf.Sqrt(1 - SumDiff / SumAll);

            return ProbMating;
        }
        else
        {
            return 0f;
        }
    }

    private void CalculateGoingToMateVector(StateController sc)
>>>>>>> Stashed changes
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(sc.transform.position, sc.detectionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Herbivore"))
            {
                sc.detectedTargets.Add(collider.gameObject.transform.position);
            }
            if (collider.gameObject.CompareTag("Carnivore"))
            {
                return false;
            }
        }

        return true;
    }

    private void GoToNearestMate(StateController sc)
    {
        followingVector = sc.detectedTargets.OrderByDescending(
                mate => Vector2.Distance(mate, sc.rb.position))
            .First()
            .normalized;

        sc.rb.velocity = followingVector * sc.thisUnitController.maxSpeed; 
    }
    
}



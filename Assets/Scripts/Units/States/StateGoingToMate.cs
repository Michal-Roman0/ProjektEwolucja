using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;
using Utils;
using System.Reflection;
public class StateGoingToMate : IState
{

    private Vector2 followingVector = Vector2.zero;

    public bool dedicated = false;


    public void OnEnter(StateController sc)
    {
        Debug.Log("Going to mate");
        sc.StartCoroutine(GoingToMateTimer(sc));
        sc.rb.velocity *= 0;
    }

    public void UpdateState(StateController sc)
    {

        if (dedicated == false && sc.visibleMates.Any())
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
    }

    public void OnExit(StateController sc)
    {

    }

    private IEnumerator GoingToMateTimer(StateController sc)
    {
        yield return new WaitForSeconds(4);

        if (sc.thisUnitController.hungry)
        {
            sc.ChangeState(sc.stateWandering);
        }

        else if (sc.visibleEnemies.Any())
        {
            sc.ChangeState(sc.stateFleeing);
        }

        else if (!sc.visibleMates.Any())
        {
            sc.ChangeState(sc.stateWandering);
        }
        else sc.StartCoroutine(GoingToMateTimer(sc));
    }

    private void CalculateGoingToMateVector(StateController sc)
    {
        if (sc.visibleMates.Any())
        {
            Vector2 closestMate = sc.visibleMates
                .OrderBy(mate =>
                    Vector2.Distance(mate.transform.position, sc.rb.position))
                .First().transform.position;

            Vector2 followDirection = (closestMate - sc.rb.position).normalized;
            float speedFactor = MapInfoUtils.GetTileDifficulty(sc.transform.position.x, sc.transform.position.y);

            sc.rb.velocity = followDirection * sc.thisUnitController.normalSpeed * speedFactor;
        }
    }


    private float CalculateProbMating(StateController sc)
    {
        if (sc.visibleMates.Any())
        {
            GameObject closestMateObj = sc.visibleMates
                .OrderBy(mate => Vector2.Distance(mate.transform.position, sc.rb.position)).First();

            UnitController my_ucontroller = sc.GetComponent<UnitController>();
            UnitController her_ucontroller = closestMateObj.GetComponent<UnitController>();

            FieldInfo[] fields1 = my_ucontroller.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            FieldInfo[] fields2 = her_ucontroller.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

            float SumDiff = 0;
            float SumAll = 0;



            for (int i = 0; i < fields1.Length; i++)
            {
                object value1 = fields1[i].GetValue(my_ucontroller);
                object value2 = fields2[i].GetValue(her_ucontroller);

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
        else
        {
            return 0f;
        }
    }

    public override string ToString()
    {
        return "Going to Mate";
    }
}



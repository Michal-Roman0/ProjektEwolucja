using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class StateChasing : IState
{
    public void OnEnter(StateController sc)
    {
        sc.StartCoroutine(chasingTimer(sc));
        sc.rb.velocity *= Vector2.zero;
        
        Debug.Log("Chasing");
    }
    public void UpdateState(StateController sc)
    {

        CalculateChasingVector(sc);
    }

    public void OnExit(StateController sc)
    {
        
    }

    private IEnumerator chasingTimer(StateController sc)
    {
        Debug.Log("Corutyna poczatek");
        yield return new WaitForSeconds(4);
        Debug.Log("corutyna po 4 sekundach");
        Debug.Log("Enemies: "+ sc.visibleEnemies.Any());
        Debug.Log("Targets: "+ sc.visibleTargets.Any());
        Debug.Log("Po wszystkim");
        if (sc.visibleEnemies.Any())
        {
            sc.ChangeState(sc.stateFleeing);
        }
        
        else if (!sc.visibleTargets.Any())
        {
            sc.ChangeState(sc.stateWandering);
        }
        else{
            sc.StartCoroutine(chasingTimer(sc));
        }
        Debug.Log("Po corutynie");
    }

    private void CalculateChasingVector(StateController sc)
    {
        if(sc.visibleTargets.Any()){
            Vector2 closestTarget = sc.visibleTargets
                .OrderBy(herbivore => 
                    Vector2.Distance(sc.rb.position, herbivore.transform.position))
                .First().transform.position;
            
            Vector2 chaseVector = (closestTarget - sc.rb.position).normalized;
            float speedFactor = MapInfoUtils.GetTileDifficulty(sc.transform.position.x, sc.transform.position.y);

            sc.rb.velocity = chaseVector * (sc.thisUnitController.maxSpeed * speedFactor);
        }

    }

    public override string ToString()
    {
        return "Chasing";
    }
}

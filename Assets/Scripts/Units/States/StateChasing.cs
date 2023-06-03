using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChasing : IState
{
    public void OnEnter(StateController sc)
    {
        sc.StartCoroutine(chasingTimer(sc));
        sc.rb.velocity *= Vector2.zero;

        if (sc.gameObject.CompareTag("Carnivore"))
        {
            Debug.Log("Carnivore start Chasing");
        }
    }
    public void UpdateState(StateController sc)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(sc.transform.position, sc.detectionRadius);
        float minDistance = float.MaxValue;
        Vector2 closestTarget = Vector2.zero;

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Herbivore") || 
                (collider.gameObject.CompareTag("Carnivore") /*&& myThreat > otherThreat*/))
            {
                Vector2 enemyPos = collider.gameObject.transform.position;
                float enemyDistance = Vector2.Distance(sc.rb.position, enemyPos);
                if (enemyDistance < minDistance)
                {
                    minDistance = enemyDistance;
                    closestTarget = enemyPos;
                }
            }
        }

        if (closestTarget != Vector2.zero)
        {
            Vector2 chaseVector = (closestTarget - sc.rb.position).normalized;
            sc.rb.velocity = chaseVector * sc.thisUnitController.maxSpeed;
        }
    }

    public void OnExit(StateController sc)
    {
        sc.detectedTargets.Clear();
    }

    private IEnumerator chasingTimer(StateController sc)
    {
        yield return new WaitForSeconds(4);
        sc.ChangeState(sc.stateWandering);
    }
}

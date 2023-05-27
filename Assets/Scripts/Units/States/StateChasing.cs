using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChasing : IState
{

    private Vector2 chaseVector = Vector2.zero;
    private const float R = 3f; // Maximum speed for fleeing

    public void OnEnter(StateController sc)
    {
        //wejscie w stan
        sc.StartCoroutine(chasingTimer(sc));
        sc.rb.velocity *= 0;

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
            if (collider.gameObject.CompareTag("Herbivore"))
            {
                Vector2 enemyPos = collider.gameObject.transform.position;
                float distance = Vector2.Distance(sc.rb.position, enemyPos);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestTarget = enemyPos;
                }
            }
        }

        if (closestTarget != Vector2.zero)
        {
            Vector2 chaseVector = (closestTarget - sc.rb.position).normalized;
            sc.rb.velocity = chaseVector * R;
        }
    }

    public void OnExit(StateController sc)
    {
        // Exit state logic
        // Clear the detected enemies list
        sc.detectedTargets.Clear();
        return;
    }

    IEnumerator chasingTimer(StateController sc)
    {
        yield return new WaitForSeconds(4);
        sc.ChangeState(sc.stateWandering);
        
    }
}

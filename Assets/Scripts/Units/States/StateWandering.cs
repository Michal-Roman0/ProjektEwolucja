using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Extensions;

public class StateWandering : IState
{
    private float angle;
    private const float Mean = 0f;
    private const float Stdev = 3f;
    private const float R = 3f;

    private float GetRandomAngle() => RandomUtils.GenerateGaussianNoise(Mean, Stdev) * Mathf.PI;

    public void OnEnter(StateController sc)
    {
        sc.rb.velocity = Vector2.zero;
        sc.rb.velocity = new Vector2().FromPolar(R, GetRandomAngle());

        if (sc.gameObject.CompareTag("Carnivore"))
        {
            Debug.Log("Carnivore start Wandering");
        }
    }

    public void UpdateState(StateController sc)
    {
        if (sc.gameObject.CompareTag("Herbivore"))
        {
            if (sc.detectedEnemies.Count > 0)
            {
                sc.ChangeState(sc.stateFleeing);
                return;
            }

            // Check for carnivores
            Collider2D[] colliders = Physics2D.OverlapCircleAll(sc.transform.position, sc.detectionRadius);

            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.CompareTag("Plant"))
                {
                    Debug.Log("Foooood!!!!!");
                    sc.ChangeState(sc.stateGoingToFood);
                    return;
                }

                if (collider.gameObject.CompareTag("Carnivore"))
                {
                    sc.ChangeState(sc.stateFleeing);
                    return;
                }

                if (collider.gameObject.CompareTag("Herbivore") && collider.gameObject != sc.gameObject)
                {
                    sc.ChangeState(sc.stateGoingToMate);
                    return;
                }
            }
        }

        if (sc.gameObject.CompareTag("Carnivore"))
        {
            if (sc.detectedTargets.Count > 0)
            {
                sc.ChangeState(sc.stateChasing);
                return;
            }
            
            Collider2D[] colliders = Physics2D.OverlapCircleAll(sc.transform.position, sc.detectionRadius);

            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.CompareTag("Herbivore"))
                {
                    sc.ChangeState(sc.stateChasing);
                    return;
                }
            }
        }

        float deltaAngle = GetRandomAngle();
        angle = Mathf.Asin(sc.rb.velocity.y / sc.rb.velocity.magnitude) + deltaAngle * Time.deltaTime;
        Vector2 newVector = new Vector2().FromPolar(R, angle);
        sc.rb.velocity = newVector.normalized * (sc.thisUnitController.maxSpeed * 0.5f);
    }

    public void OnExit(StateController sc)
    {
        return;
    }
}
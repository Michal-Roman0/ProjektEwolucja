using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWandering : IState
{
    private float angle;
    private const float Mean = 0f;
    private const float Stdev = 3f;
    private const float R = 3f;


    private float GenerateGaussianNoise(float mean, float stdev)
    {
        float u1 = Random.value;
        float u2 = Random.value;
        float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2);
        return mean + stdev * randStdNormal;
    }

    private float GetRandomAngle()
    {
        return GenerateGaussianNoise(Mean, Stdev) * Mathf.PI;
    }

    public void OnEnter(StateController sc)
    {

        sc.rb.velocity = Vector2.zero;
        angle = GetRandomAngle();
        sc.rb.velocity = new Vector2(Mathf.Cos(angle) * R, Mathf.Sin(angle) * R);

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
                if (collider.gameObject.CompareTag("Carnivore"))
                {
                    sc.ChangeState(sc.stateFleeing);
                    return;
                }
            }
        }

        else if (sc.gameObject.CompareTag("Carnivore"))

        {

            if (sc.detectedTargets.Count > 0)
            {

                sc.ChangeState(sc.stateChasing);
                return;
            }

            // check for prey
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
        Vector2 newVector = new Vector2(Mathf.Cos(angle) * R, Mathf.Sin(angle) * R);
        sc.rb.velocity = newVector.normalized *sc.thisUnitController.maxSpeed*0.5f;






    }

    public void OnExit(StateController sc)
    {
        return;
    }
}


using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;
using Extensions;

public class StateWandering : IState
{
    private float angle;
    private const float Mean = 0f;
    private const float Stdev = 3f;
    private const float R = 3f;

    public Vector2 WanderVector;

    private float GetRandomAngle() => RandomUtils.GenerateGaussianNoise(Mean, Stdev) * Mathf.PI;

    public void OnEnter(StateController sc)
    {
        Debug.Log("Wandering");
        sc.rb.velocity = Vector2.zero;
        sc.rb.velocity = new Vector2().FromPolar(R, GetRandomAngle());
    }

    public void UpdateState(StateController sc)
    {
        if (sc.visibleEnemies.Any())
        {
            sc.ChangeState(sc.stateFleeing);
            return;
        }

        if (sc.visibleTargets.Any() && sc.thisUnitController.hungry)
        {
            if (sc.CompareTag("Herbivore")) 
                sc.ChangeState(sc.stateGoingToFood);
            else if (sc.CompareTag("Carnivore"))
                sc.ChangeState(sc.stateChasing);
            return;
        }

        if (sc.visibleMates.Any())
        {
            sc.ChangeState(sc.stateGoingToMate);
            return;
        }
        CalculateWanderingVector(sc);

    }

    public void OnExit(StateController sc)
    {

    }

    private void CalculateWanderingVector(StateController sc)
    {
        float deltaAngle = GetRandomAngle() * Time.deltaTime;


        WanderVector = sc.rb.velocity;


        float speedFactor = MapInfoUtils.GetTileDifficulty(sc.transform.position.x, sc.transform.position.y);

        float centerX = sc.gameObject.transform.position.x;
        float centerY = sc.gameObject.transform.position.y;

        Vector2 center = new Vector2(centerX, centerY);

        float alpha = 1f;
        float beta = 0.01f;
        float gamma = 0.02f;

        float sum_weighted_x = 0;
        float sum_weighted_y = 0;
        float sum_of_weights = 0;

        int detectionradius = 10;

        float weight = 0;

        WanderVector = WanderVector.normalized * detectionradius;

        Vector2 WanderVectorOrtogonal1 = new Vector2(-WanderVector.y, WanderVector.x);
        Vector2 WanderVectorOrtogonal2 = new Vector2(WanderVector.y, -WanderVector.x);

        Vector2 P1 = center + WanderVectorOrtogonal1;
        Vector2 P2 = center + WanderVectorOrtogonal2;
        Vector2 P3 = center + WanderVector;


        Debug.DrawLine(P1, P2);

        Debug.DrawLine(center, center + WanderVector);


        Vector2 P1_P2_vector_iterator = (P2 - P1).normalized;

        Vector2 Radius_vector_iterator = (P3 - center).normalized;




        Vector2 Position = new Vector2(P1.x, P1.y);

        int iterator_counter = -detectionradius;


        while ((Position - P2).magnitude >= 1)
        {
            Vector2 temporal_position = new Vector2(Position.x, Position.y);
            Vector2 temporal_max = temporal_position + Radius_vector_iterator * Mathf.Sqrt(detectionradius * detectionradius - iterator_counter * iterator_counter);

            while ((temporal_position - temporal_max).magnitude >= 1)
            {
                Debug.DrawLine(Position, temporal_position);
                float diff = MapInfoUtils.GetTileDifficulty((int)(temporal_position.x), (int)(temporal_position.y));

                if (diff == -1)
                {
                    WanderVector += (center - temporal_position) / (center - temporal_position).magnitude;
                    weight = 0;
                }
                else
                {
                    weight = diff;
                }
                sum_of_weights += weight;
                sum_weighted_x += (temporal_position.x * weight);
                sum_weighted_y += (temporal_position.y * weight);
                temporal_position += Radius_vector_iterator;
            }
            iterator_counter += 1;
            Position += P1_P2_vector_iterator;
        }


        sum_weighted_x /= sum_of_weights;
        sum_weighted_y /= sum_of_weights;

        Vector2 best_vector = new Vector2(sum_weighted_x - centerX, sum_weighted_y - centerY);

        sc.rb.velocity = (WanderVector.normalized * alpha + best_vector.normalized * beta);

        deltaAngle *= gamma;
        Vector2 rotatedVector = new Vector2(
        sc.rb.velocity.x * Mathf.Cos(deltaAngle) - sc.rb.velocity.y * Mathf.Sin(deltaAngle),
        sc.rb.velocity.x * Mathf.Sin(deltaAngle) + sc.rb.velocity.y * Mathf.Cos(deltaAngle)
    );


        sc.rb.velocity = rotatedVector;
        sc.rb.velocity = sc.rb.velocity.normalized * (sc.thisUnitController.maxSpeed * 0.5f * speedFactor);
    }

    public override string ToString()
    {
        return "Wandering";
    }
}
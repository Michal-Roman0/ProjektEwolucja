using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class StateFleeing : IState
{
    Vector2 WanderVector;
    public void OnEnter(StateController sc)
    {
        Debug.Log("Fleeing");
        sc.StartCoroutine(FleeingTimer(sc));
        sc.rb.velocity *= 0;
    }

    public void UpdateState(StateController sc)
    {
        SetEscapeVector(sc);
    }

    public void OnExit(StateController sc)
    {

    }

    private IEnumerator FleeingTimer(StateController sc)
    {
        yield return new WaitForSeconds(4);

        if (!sc.visibleEnemies.Any())
        {
            sc.ChangeState(sc.stateWandering);
        }
        else
        {
            sc.StartCoroutine(FleeingTimer(sc));
        }
    }

    private void SetEscapeVector(StateController sc)
    {
        var escapeVector = sc.rb.velocity;



        foreach (GameObject enemy in sc.visibleEnemies)
        {
            Vector2 difference = (Vector2)enemy.transform.position - sc.rb.position; // reverse direction
            escapeVector -= difference / (difference.sqrMagnitude * 2); // subtract instead of add to further reverse direction
            escapeVector -= new Vector2(-difference.y, difference.x) * 0.001f;
        }

        escapeVector.Normalize();

        //sc.rb.velocity = escapeVector * (sc.thisUnitController.maxSpeed * speedFactor);





        WanderVector = escapeVector;


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

        float detectionradius = sc.thisUnitController.radius;

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

        float iterator_counter = -detectionradius;
        bool break_iterations = false;

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

            if (break_iterations)
            {
                break;
            }
            iterator_counter += 1;
            Position += P1_P2_vector_iterator;
        }


        sum_weighted_x /= sum_of_weights;
        sum_weighted_y /= sum_of_weights;

        Vector2 best_vector = new Vector2(sum_weighted_x - centerX, sum_weighted_y - centerY);

        sc.rb.velocity = (WanderVector.normalized * alpha + best_vector.normalized * beta);
        sc.rb.velocity = sc.rb.velocity.normalized * (sc.thisUnitController.maxSpeed * 0.5f * speedFactor);






    }

    public override string ToString()
    {
        return "Fleeing";
    }
}

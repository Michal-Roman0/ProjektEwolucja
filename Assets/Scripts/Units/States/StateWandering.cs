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

        if (sc.visibleMates.Any() /*&& currentEnergy>0.6*maxEnergy*/)
        {
            sc.ChangeState(sc.stateGoingToMate);
            return;
        }

        if (sc.visibleTargets.Any() && sc.thisUnitController.hungry)
        {
            if (sc.CompareTag("Herbivore")) 
                sc.ChangeState(sc.stateGoingToFood);
            else if (sc.CompareTag("Carnivore"))
                sc.ChangeState(sc.stateChasing);
                //sc.ChangeState(sc.stateGoingToFood);
            //return;
        }
        else CalculateWanderingVector(sc);

    }

    public void OnExit(StateController sc)
    {

    }

    private void CalculateWanderingVector(StateController sc)
    {
        float deltaAngle = GetRandomAngle();
        angle = Mathf.Asin(sc.rb.velocity.y / sc.rb.velocity.magnitude) + deltaAngle * Time.deltaTime;
        Vector2 newVector = new Vector2().FromPolar(R, angle);
        sc.rb.velocity = newVector.normalized * (sc.thisUnitController.maxSpeed * 0.5f);
    }

    public override string ToString()
    {
        return "Wandering";
    }
}
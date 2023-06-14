using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploring : MonoBehaviour
{
    public float deltaTime => Time.deltaTime;
    public float Mean => 0f;
    public float Stdev => 3f;
    public float R => 3f;
    public float longest = 0;
    public Vector2 escape_vector;

    public int state  = 0;

    public int scared_duration;

    public Rigidbody2D rb;
    float randomNumber;
    float angle;

    public CircleCollider2D circleCollider;


    private float f(float x, float y)
    {
        // nie wiem jak to nazwac wiec po prostu f
        return Mathf.Sqrt(-2f * Mathf.Log(x)) * Mathf.Sin(2f * Mathf.PI * y);
    }


    protected float AngleFromValue(float value)
    {
        return value * Mathf.PI;
    }
    protected float GetRandomAngle()
    {
        float value = Mean + Stdev * f(Random.value, Random.value);
        return AngleFromValue(value);
    }
    private void ResetRBody()
    {
        circleCollider = GetComponent<CircleCollider2D>();

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }


    void Start()
    {
        ResetRBody();
        angle = GetRandomAngle();


        var newVector = new Vector2(
            Mathf.Cos(angle) * R,
            Mathf.Sin(angle) * R
        );

        rb.velocity = newVector;

    }

    // Update is called once per frame
    void Update()
    {

        Collider2D[] overlappingColliders = Physics2D.OverlapCircleAll(transform.position, circleCollider.radius);






        List<Vector2> enemy_coordinates = new List<Vector2>();

        foreach (Collider2D collider in overlappingColliders)
        {



            UnitController unitController2 = collider.gameObject.GetComponent<UnitController>();
            Foodcon unitController = collider.gameObject.GetComponent<Foodcon>();
            Vector2 coordinates = collider.gameObject.transform.position;


            if (unitController2 != null)
            {

                if (unitController2.type == 2)
                {
                    Vector2 cords = collider.gameObject.transform.position;


                    if (enemy_coordinates.Count < 3)
                    {
                        enemy_coordinates.Add(cords);
                    }


                    scared_duration = 20;
                }
            }


            if (unitController != null && state != 2)
            {

                if (unitController.type == 1)
                {

                }
            }
        }



        if(scared_duration<=0)
        {

            float current_angle = Mathf.Asin(rb.velocity.y / rb.velocity.magnitude);

            float deltaAngle = GetRandomAngle();

            angle = current_angle + deltaAngle * deltaTime;

            var newVector = new Vector2(
                Mathf.Cos(angle) * R,
                Mathf.Sin(angle) * R
            );

            rb.velocity = newVector;

            rb.velocity = (rb.velocity / rb.velocity.magnitude) * R;


        }
        else
        {
            if (enemy_coordinates.Count >= 1)
            {


                for (int i = 0; i < enemy_coordinates.Count; i++)
                {
                    Vector2 difference = (rb.position - enemy_coordinates[i]);
                    escape_vector += difference / (difference.magnitude* difference.magnitude);

                }
                rb.velocity = escape_vector;
                rb.velocity = (rb.velocity / rb.velocity.magnitude)*R;
            }

            longest = 0;
            scared_duration -= 1;
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploring : MonoBehaviour
{
    public float deltaTime => Time.deltaTime;
    public float Mean => 0f;
    public float Stdev => 3f;
    public float R => 1f;

    public Rigidbody2D rb;
    float randomNumber;
    float angle;


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

        float deltaAngle = GetRandomAngle();

        angle += deltaAngle * deltaTime;

        var newVector = new Vector2(
            Mathf.Cos(angle) * R,
            Mathf.Sin(angle) * R
        );

        rb.velocity = newVector;
        rb.position += (rb.velocity) * deltaTime;
    }
}

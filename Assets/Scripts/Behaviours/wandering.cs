using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class wandering : MonoBehaviour
{
    
    public Rigidbody2D rb;
    float randomNumber;
    float angle;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0,0);
        angle = Random.value* (Mathf.PI);
    }

    // Update is called once per frame
    void Update()
    {
        float delta_t = 0.001f;


        Random.seed = (int)System.DateTime.Now.Ticks;


        float mean = 0f;
        float stdDeviation = 0.1f;
        float value = mean + stdDeviation * (float)(Mathf.Sqrt(-2f * Mathf.Log(Random.value)) * Mathf.Sin(2f * Mathf.PI * Random.value));

        float delta_angle = value * (Mathf.PI);
        float r = 1.0f;

        angle = angle + delta_angle * delta_t*100;


        var v_x = Mathf.Cos( angle )*r ;
        var v_y = Mathf.Sin( angle )*r ;

        var new_vector = new Vector2(v_x, v_y);

        

        rb.velocity = new_vector;
        rb.position += (rb.velocity)*delta_t;
    }
}

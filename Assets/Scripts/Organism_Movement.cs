using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organism_Movement : MonoBehaviour
{
    [SerializeField] float Radius;
    float angle;

    void Start()
    {
        angle = 0;
        transform.position = new Vector3(Radius, 0, 0);
    }

    public void Move()
    {
        angle += 2 * Time.deltaTime;

        if (angle > 2 * Mathf.PI) {
            angle -= 2 * Mathf.PI;
        }

        float posX = Radius * Mathf.Sin(angle);
        float posY = Radius * Mathf.Cos(angle);

        transform.position = new Vector3(posX, posY, 0);
    }
}

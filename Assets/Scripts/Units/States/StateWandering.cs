using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Extensions;
using UnityEngine.Tilemaps;




public class StateWandering : IState
{
    private float angle;
    private const float Mean = 0f;
    private const float Stdev = 3f;
    private const float R = 3f;


    private float GetRandomAngle() => RandomUtils.GenerateGaussianNoise(Mean, Stdev) * Mathf.PI;


    private GameObject mapObject;

    private Tilemap tilemap;

    private Tilemap_Controller tilemapcontroller;

    private MapTile[,] mapTiles;


    public Vector2 WanderVector;

    public float deltaAngle;


    public void OnEnter(StateController sc)
    {
        sc.rb.velocity = Vector2.zero;
        sc.rb.velocity = new Vector2().FromPolar(R, GetRandomAngle());

        this.mapObject = GameObject.FindWithTag("Ground");

        this.tilemap = mapObject.GetComponent<Tilemap>();

        this.tilemapcontroller = tilemap.GetComponent<Tilemap_Controller>();

        this.mapTiles = tilemapcontroller.mapTiles;

        float deltaAngle = GetRandomAngle();
        
    }


    public void UpdateState(StateController sc)
    {




        deltaAngle = GetRandomAngle();




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

        angle = Mathf.Asin(sc.rb.velocity.y / sc.rb.velocity.magnitude) + deltaAngle * Time.deltaTime;
        WanderVector = new Vector2().FromPolar(R, angle);

        //WanderVector = WanderVector.normalized;


        float centerX = sc.gameObject.transform.position.x;
        float centerY = sc.gameObject.transform.position.y;
        float radius = sc.detectionRadius;

        mapObject = GameObject.FindWithTag("Ground");

        tilemap = mapObject.GetComponent<Tilemap>();

        tilemapcontroller = tilemap.GetComponent<Tilemap_Controller>();

        mapTiles = tilemapcontroller.mapTiles;

        Vector3Int center2d = tilemap.WorldToCell(new Vector3(centerX, centerY, 1f));




        Vector2 center = new Vector2((int)(centerX), (int)(centerY));


        // sprawdzanie najmnieszej wagi w osmiosasiedztwie

        float alpha = 1f;
        float beta = 100f;

        float best_weight = -1;
        float diff = 0;

        Vector2 best_vector = new Vector2(0f, 0f);


        float sum_weighted_x = 0;

        float sum_weighted_y = 0;

        float sum_of_weights = 0;

        for (int y= -(int)(radius) ; y <= (int)(radius); y++)
        {
            for (int x = -(int)(radius); x <= (int)(radius); x++)
            {

                if (x < 0 && WanderVector.x > 0)
                {
                    continue;
                }

                if (x > 0 && WanderVector.x < 0)
                {
                    continue;
                }

                //if (y < 0 && WanderVector.y > 0)
                //{
                //    continue;
                //}

                //if (y > 0 && WanderVector.y < 0)
                //{
                //    continue;
                //}



                Vector2 position = new Vector2(center.x + x, center.y + y); // after hipotetical vecotr

                Vector2 lg1 = new Vector2(x, y);

                diff = mapTiles[(int)(position.x), (int)(position.y)].GetValue(MapType.Difficulty) ;

                sum_of_weights += 1 / diff;

                sum_weighted_x += (position.x) / diff;

                sum_weighted_y += (position.y) / diff;
            }
        }

        sum_weighted_x /= sum_of_weights;
        sum_weighted_y /= sum_of_weights;


        best_vector = new Vector2(sum_weighted_x - centerX, sum_weighted_y - centerY); // after hipotetical vecotr

        sc.rb.velocity = ( best_vector.normalized * beta + WanderVector.normalized * alpha).normalized * (sc.thisUnitController.maxSpeed * 0.5f)*(100/ mapTiles[(int)(centerX), (int)(centerY)].GetValue(MapType.Difficulty));

        WanderVector = sc.rb.velocity;



    }

    public void OnExit(StateController sc)
    {
        return;
    }
}
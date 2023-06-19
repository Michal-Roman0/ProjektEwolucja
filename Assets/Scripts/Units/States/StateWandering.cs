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
    private const float Stdev = 1f;
    private const float R = 3f;
    Vector2 CenterofMap = new Vector2(250f, 130f);
    Vector2 WanderVector;


    public Vector2 WanderVector;

    private float GetRandomAngle() => RandomUtils.GenerateGaussianNoise(Mean, Stdev) * Mathf.PI;


    private GameObject mapObject;

    private Tilemap tilemap;

    private Tilemap_Controller tilemapcontroller;

    private MapTile[,] mapTiles;


    public Vector2 WanderVector;

    public float deltaAngle;


    public void OnEnter(StateController sc)
    {
<<<<<<< Updated upstream
        sc.rb.velocity = Vector2.zero;
        sc.rb.velocity = new Vector2().FromPolar(R, GetRandomAngle());

        this.mapObject = GameObject.FindWithTag("Ground");

        this.tilemap = mapObject.GetComponent<Tilemap>();

        this.tilemapcontroller = tilemap.GetComponent<Tilemap_Controller>();

        this.mapTiles = tilemapcontroller.mapTiles;

        float deltaAngle = GetRandomAngle();
        
=======
        Debug.Log("Wandering");
        angle = GetRandomAngle();
        sc.rb.velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));



        WanderVector = sc.rb.velocity;

        Vector2 CenterofMap = new Vector2(250f, 130f);




>>>>>>> Stashed changes
    }


    public void UpdateState(StateController sc)
    {
<<<<<<< Updated upstream




        deltaAngle = GetRandomAngle();




        if (sc.gameObject.CompareTag("Herbivore"))
=======
        


        sc.thisUnitController.hunger = 100;
        if (sc.visibleEnemies.Any())
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
            if (sc.detectedTargets.Count > 0)
            {
=======
            if (sc.CompareTag("Herbivore"))
            {
                sc.ChangeState(sc.stateGoingToFood);
                
            }
            else if (sc.CompareTag("Carnivore"))
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
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
=======
            Debug.Log("To Herbivor frien");
            sc.ChangeState(sc.stateGoingToMate);
            
            return;
>>>>>>> Stashed changes
        }

        sum_weighted_x /= sum_of_weights;
        sum_weighted_y /= sum_of_weights;


        best_vector = new Vector2(sum_weighted_x - centerX, sum_weighted_y - centerY); // after hipotetical vecotr

        sc.rb.velocity = ( best_vector.normalized * beta + WanderVector.normalized * alpha).normalized * (sc.thisUnitController.maxSpeed * 0.5f)*(100/ mapTiles[(int)(centerX), (int)(centerY)].GetValue(MapType.Difficulty));

        WanderVector = sc.rb.velocity;



    }

    public void OnExit(StateController sc)
    {
<<<<<<< Updated upstream
        return;
=======

    }

    private void CalculateWanderingVector(StateController sc)
<<<<<<< Updated upstream
{
    float deltaAngle = GetRandomAngle() * Time.deltaTime;


    WanderVector = sc.rb.velocity ;


    float speedFactor = MapInfoUtils.GetTileDifficulty(sc.transform.position.x, sc.transform.position.y);

    float centerX = sc.gameObject.transform.position.x;
    float centerY = sc.gameObject.transform.position.y;

    Vector2 center = new Vector2(centerX, centerY);

    // new vector directing towards the center of the map
    Vector2 centerVector = -WanderVector;

    float alpha = 10f;  // weight for wander vector
    float beta = 1f;   // weight for center vector
    float gamma = 0.05f;

    float sum_weighted_x = 0;
    float sum_weighted_y = 0;
    float sum_of_weights = 0;

    int detectionradius = 10;

    float weight = 0;
=======
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
>>>>>>> Stashed changes

        WanderVector = WanderVector.normalized * detectionradius;

        Vector2 WanderVectorOrtogonal1 = new Vector2(-WanderVector.y, WanderVector.x);
<<<<<<< Updated upstream
    Vector2 WanderVectorOrtogonal2 = new Vector2(WanderVector.y, -WanderVector.x);

    Vector2 P1 = center + WanderVectorOrtogonal1;
    Vector2 P2 = center + WanderVectorOrtogonal2;
    Vector2 P3 = center + WanderVector;
=======
        Vector2 WanderVectorOrtogonal2 = new Vector2(WanderVector.y, -WanderVector.x);

        Vector2 P1 = center + WanderVectorOrtogonal1;
        Vector2 P2 = center + WanderVectorOrtogonal2;
        Vector2 P3 = center + WanderVector;
>>>>>>> Stashed changes

        Debug.DrawLine(P1, P2);

        Debug.DrawLine(center, center + WanderVector);


        Vector2 P1_P2_vector_iterator = (P2 - P1).normalized;

        Vector2 Radius_vector_iterator = (P3 - center).normalized;




        Vector2 Position = new Vector2(P1.x, P1.y);

        int iterator_counter = -detectionradius;


<<<<<<< Updated upstream
        while( (Position - P2).magnitude >= 1)
=======
        while ((Position - P2).magnitude >= 1)
>>>>>>> Stashed changes
        {

            Vector2 temporal_position = new Vector2(Position.x, Position.y);
            Vector2 temporal_max = temporal_position + Radius_vector_iterator * Mathf.Sqrt(detectionradius * detectionradius - iterator_counter * iterator_counter);

<<<<<<< Updated upstream
            while((temporal_position - temporal_max).magnitude >= 1)
            {

=======
            while ((temporal_position - temporal_max).magnitude >= 1)
            {
                Debug.DrawLine(Position, temporal_position);
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
                
=======

>>>>>>> Stashed changes
                temporal_position += Radius_vector_iterator;
            }
            iterator_counter += 1;
            Position += P1_P2_vector_iterator;
        }


<<<<<<< Updated upstream
    sum_weighted_x /= sum_of_weights;
    sum_weighted_y /= sum_of_weights;

    Vector2 best_vector = new Vector2(sum_weighted_x - centerX, sum_weighted_y - centerY);
=======
        sum_weighted_x /= sum_of_weights;
        sum_weighted_y /= sum_of_weights;

        Vector2 best_vector = new Vector2(sum_weighted_x - centerX, sum_weighted_y - centerY);
>>>>>>> Stashed changes

        sc.rb.velocity = (WanderVector.normalized * alpha + best_vector.normalized * beta);

        deltaAngle *= gamma;
        Vector2 rotatedVector = new Vector2(
        sc.rb.velocity.x * Mathf.Cos(deltaAngle) - sc.rb.velocity.y * Mathf.Sin(deltaAngle),
        sc.rb.velocity.x * Mathf.Sin(deltaAngle) + sc.rb.velocity.y * Mathf.Cos(deltaAngle)
    );

<<<<<<< Updated upstream
        sc.rb.velocity = rotatedVector ;
=======
        sc.rb.velocity = rotatedVector;
>>>>>>> Stashed changes
        sc.rb.velocity = sc.rb.velocity.normalized * (sc.thisUnitController.maxSpeed * 0.5f * speedFactor);


    }


    public override string ToString()
    {
        return "Wandering";
>>>>>>> Stashed changes
    }
}
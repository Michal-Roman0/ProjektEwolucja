using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class Tilemap_Controller : MonoBehaviour
{
    public static Tilemap_Controller instance;

    public MapData mapData;
    public MapEditor mapEditor;
    public Tilemap groundTilemap;
    public Tile groundTile;
    private float diffOffset;
    private float tempOffset;
    private float vegeOffset;

    public float noiseScale;
    public bool moreDetails = false;
    public float regrowthInSeconds = 10f;
    public float plantScarcity = 10f;
    public GameObject plantPrefab;

    MapTile[,] mapTiles;
    bool[,] checkedPaintedOver;

    // IEnumerator PlantTimer()
    // {
    //     WaitForSeconds cooldown = new WaitForSeconds(regrowthInSeconds);
    //     while(true){
    //         yield return cooldown;
    //         for (int x = 0; x < mapData.MapWidth; x++) {
    //             for (int y = 0; y < mapData.MapHeight; y++) {
    //                 float threshold = Random.Range(25,100*plantScarcity);
    //                 if(mapTiles[x,y].GetValue(MapType.Vegetation) > threshold){
    //                     Vector3 tilePos = groundTilemap.GetCellCenterWorld(new Vector3Int(x,y,0));
    //                     Instantiate(plantPrefab, tilePos, Quaternion.identity);
    //                 }
    //             }
    //         }
    //     }
    // }

    void Start()
    {
        InitializeNewMap();

        if (instance == null) {
            instance = this;
        }

        //StartCoroutine(PlantTimer());
    }

    public MapTile GetMapTile(Vector2Int coords) {
        if (coords.x >= 0 && coords.x < mapData.MapWidth && coords.y >= 0 && coords.y < mapData.MapHeight)
            return mapTiles[coords.x, coords.y];
        return null;
    }

    public void InitializeNewMap() {
        mapTiles = new MapTile[mapData.MapWidth, mapData.MapHeight];
        checkedPaintedOver = new bool[mapData.MapWidth, mapData.MapHeight];

        // offsets - to make map different every time
        diffOffset = Random.Range(0f, 10f);
        tempOffset = Random.Range(0f, 10f);
        vegeOffset = Random.Range(0f, 10f);
                
        float diffMaxValue = 0;
        float tempMaxValue = 0;
        float vegeMaxValue = 0;
        float diffMinValue = 1;
        float tempMinValue = 1;
        float vegeMinValue = 1;
        float[,] diffNoiseMap = new float[mapData.MapWidth, mapData.MapHeight];
        float[,] tempNoiseMap = new float[mapData.MapWidth, mapData.MapHeight];
        float[,] vegeNoiseMap = new float[mapData.MapWidth, mapData.MapHeight];


        for (int x = 0; x < mapData.MapWidth; x++) {
            for (int y = 0; y < mapData.MapHeight; y++) {


                float diffPerlinValue = Mathf.PerlinNoise(x * noiseScale + diffOffset, y * noiseScale + diffOffset);
                float tempPerlinValue = Mathf.PerlinNoise(x * noiseScale + tempOffset, y * noiseScale + tempOffset);
                float vegePerlinValue = Mathf.PerlinNoise(x * noiseScale + vegeOffset, y * noiseScale + vegeOffset);

                // makes more "swirly" patterns if needed. Remember to adjust noiseScale
                if(moreDetails){
                    diffPerlinValue = Mathf.PerlinNoise(diffPerlinValue*x*noiseScale + diffOffset, diffPerlinValue*y*noiseScale + diffOffset);
                    tempPerlinValue = Mathf.PerlinNoise(tempPerlinValue*x*noiseScale + tempOffset, tempPerlinValue*y*noiseScale + tempOffset);
                    vegePerlinValue = Mathf.PerlinNoise(vegePerlinValue*x*noiseScale + vegeOffset, vegePerlinValue*y*noiseScale + vegeOffset);
                }
                diffNoiseMap[x,y] = diffPerlinValue;
                tempNoiseMap[x,y] = tempPerlinValue;
                vegeNoiseMap[x,y] = vegePerlinValue;

                diffMaxValue = Mathf.Max(diffMaxValue, diffPerlinValue);
                tempMaxValue = Mathf.Max(tempMaxValue, tempPerlinValue);
                vegeMaxValue = Mathf.Max(vegeMaxValue, vegePerlinValue);
                diffMinValue = Mathf.Min(diffMinValue, diffPerlinValue);
                tempMinValue = Mathf.Min(tempMinValue, tempPerlinValue);
                vegeMinValue = Mathf.Min(vegeMinValue, vegePerlinValue);
            }
        }
        for (int x = 0; x < mapData.MapWidth; x++) {
            for (int y = 0; y < mapData.MapHeight; y++) {

                // scale values to make sure they range from 0 to 100
                int difficulty = (int)((diffNoiseMap[x,y] - diffMinValue)/(diffMaxValue - diffMinValue) * 100);
                int temperature = (int)((tempNoiseMap[x,y] - tempMinValue)/(tempMaxValue - tempMinValue) * 100);
                int vegetation = (int)((vegeNoiseMap[x,y]  - vegeMinValue)/(vegeMaxValue - vegeMinValue) * 100);

                mapTiles[x, y] = new MapTile(difficulty, temperature, vegetation);

                Vector3Int position = new Vector3Int(x, y, 0);

                groundTilemap.SetTile(position, groundTile);
                groundTilemap.SetTileFlags(position, TileFlags.None);
                groundTilemap.SetColor(position, mapTiles[x, y].GetColor(mapData.ActiveMap));

                checkedPaintedOver[x, y] = false;
            }
        }
    }

    public void ChangeMap() {
        for (int x = 0; x < mapData.MapWidth; x++) {
            for (int y = 0; y < mapData.MapHeight; y++) {
                Vector3Int position = new Vector3Int(x, y, 0);
                groundTilemap.SetTileFlags(position, TileFlags.None);
                groundTilemap.SetColor(position, mapTiles[x, y].GetColor(mapData.ActiveMap));
            }
        }
    }

    void OnMouseDrag() {
        if (mapData.IsPointerOverUI) return;

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPoint = groundTilemap.WorldToCell(worldPoint);
        
        if (groundTilemap.GetTile(gridPoint) && mapEditor.BrushActive) {
            Vector3Int startingPoint = gridPoint - new Vector3Int((int)(mapEditor.BrushSize / 2), (int)(mapEditor.BrushSize / 2), 0);

            if (mapEditor.BrushShape == 1) { // Square
                for (int x = 0; x < mapEditor.BrushSize; x++) {
                    for (int y = 0; y < mapEditor.BrushSize; y++) {
                        Vector3Int checkedPoint = startingPoint + new Vector3Int(x, y, 0);
                        
                        if (groundTilemap.GetTile(checkedPoint)) {
                            if (mapEditor.BrushMode == 1) { // Set
                                mapTiles[checkedPoint.x, checkedPoint.y].SetValue(mapData.ActiveMap, mapEditor.BrushValue);
                            }
                            if (mapEditor.BrushMode == 2) { // Add
                                mapTiles[checkedPoint.x, checkedPoint.y].AddValue(mapData.ActiveMap, mapEditor.BrushValue);
                            }
                            checkedPaintedOver[checkedPoint.x, checkedPoint.y] = true;
                            groundTilemap.SetColor(checkedPoint, mapTiles[checkedPoint.x, checkedPoint.y].GetColor(mapData.ActiveMap));
                        }
                    }
                }
            }
            else if (mapEditor.BrushShape == 2) { // Circle
                for (int x = 0; x < mapEditor.BrushSize + 1; x++) {
                    for (int y = 0; y < mapEditor.BrushSize + 1; y++) {
                        Vector3Int checkedPoint = startingPoint + new Vector3Int(x, y, 0);
                        float distance = Vector3Int.Distance(gridPoint, checkedPoint);

                        if (groundTilemap.GetTile(checkedPoint) && distance <= mapEditor.BrushSize / 2 && !checkedPaintedOver[checkedPoint.x, checkedPoint.y]) {
                            if (mapEditor.BrushMode == 1) { // Set
                                mapTiles[checkedPoint.x, checkedPoint.y].SetValue(mapData.ActiveMap, mapEditor.BrushValue);
                            }
                            if (mapEditor.BrushMode == 2) { // Add
                                mapTiles[checkedPoint.x, checkedPoint.y].AddValue(mapData.ActiveMap, mapEditor.BrushValue);
                            }
                            checkedPaintedOver[checkedPoint.x, checkedPoint.y] = true;
                            groundTilemap.SetColor(checkedPoint, mapTiles[checkedPoint.x, checkedPoint.y].GetColor(mapData.ActiveMap));
                        }
                    }
                }
            }
        }
    }

    void OnMouseUp() {
        for (int x = 0; x < mapData.MapWidth; x++)
            for (int y = 0; y < mapData.MapHeight; y++)
                checkedPaintedOver[x, y] = false;
    }

    void OnMouseDown() {
        if (mapData.IsPointerOverUI) return;

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPoint = groundTilemap.WorldToCell(worldPoint);
        var tile = groundTilemap.GetTile(gridPoint);
        int tileValue = mapTiles[gridPoint.x, gridPoint.y].GetValue(mapData.ActiveMap);

        if (tile && mapEditor.BucketActive && tileValue != mapEditor.BucketValue) {
            int referenceValue = mapTiles[gridPoint.x, gridPoint.y].GetValue(mapData.ActiveMap);

            List<Vector3Int> pointsToFill = new List<Vector3Int>();
            pointsToFill.Add(gridPoint);

            Vector3Int checkedPoint;

            while(pointsToFill.Count > 0) {
                Debug.Log(pointsToFill.Count);

                mapTiles[pointsToFill[0].x, pointsToFill[0].y].SetValue(mapData.ActiveMap, mapEditor.BucketValue);
                groundTilemap.SetColor(pointsToFill[0], mapTiles[pointsToFill[0].x, pointsToFill[0].y].GetColor(mapData.ActiveMap));

                checkedPoint = pointsToFill[0] + Vector3Int.up;
                if (FloodfillCheckPoint(checkedPoint, referenceValue)) {
                    pointsToFill.Add(checkedPoint);
                    checkedPaintedOver[checkedPoint.x, checkedPoint.y] = true;
                }
                checkedPoint = pointsToFill[0] + Vector3Int.down;
                if (FloodfillCheckPoint(checkedPoint, referenceValue)) {
                    pointsToFill.Add(checkedPoint);
                    checkedPaintedOver[checkedPoint.x, checkedPoint.y] = true;
                }
                checkedPoint = pointsToFill[0] + Vector3Int.left;
                if (FloodfillCheckPoint(checkedPoint, referenceValue)) {
                    pointsToFill.Add(checkedPoint);
                    checkedPaintedOver[checkedPoint.x, checkedPoint.y] = true;
                }
                checkedPoint = pointsToFill[0] + Vector3Int.right;
                if (FloodfillCheckPoint(checkedPoint, referenceValue)) {
                    pointsToFill.Add(checkedPoint);
                    checkedPaintedOver[checkedPoint.x, checkedPoint.y] = true;
                }

                if (mapEditor.BucketMode == 2) {
                    checkedPoint = pointsToFill[0] + Vector3Int.up + Vector3Int.left;
                    if (FloodfillCheckPoint(checkedPoint, referenceValue)) {
                        pointsToFill.Add(checkedPoint);
                        checkedPaintedOver[checkedPoint.x, checkedPoint.y] = true;
                    }
                    checkedPoint = pointsToFill[0] + Vector3Int.up + Vector3Int.right;
                    if (FloodfillCheckPoint(checkedPoint, referenceValue)) {
                        pointsToFill.Add(checkedPoint);
                        checkedPaintedOver[checkedPoint.x, checkedPoint.y] = true;
                    }
                    checkedPoint = pointsToFill[0] + Vector3Int.down + Vector3Int.left;
                    if (FloodfillCheckPoint(checkedPoint, referenceValue)) {
                        pointsToFill.Add(checkedPoint);
                        checkedPaintedOver[checkedPoint.x, checkedPoint.y] = true;
                    }
                    checkedPoint = pointsToFill[0] + Vector3Int.down + Vector3Int.right;
                    if (FloodfillCheckPoint(checkedPoint, referenceValue)) {
                        pointsToFill.Add(checkedPoint);
                        checkedPaintedOver[checkedPoint.x, checkedPoint.y] = true;
                    }
                }

                pointsToFill.RemoveAt(0);
            }
        }
    }

    bool FloodfillCheckPoint(Vector3Int checkedPoint, int referenceValue) {
        int difference = Mathf.Abs(mapTiles[checkedPoint.x, checkedPoint.y].GetValue(mapData.ActiveMap) - referenceValue);
        int tileValue = mapTiles[checkedPoint.x, checkedPoint.y].GetValue(mapData.ActiveMap);

        return groundTilemap.GetTile(checkedPoint)
            && !checkedPaintedOver[checkedPoint.x, checkedPoint.y]
            && tileValue != mapEditor.BucketValue
            && difference <= mapEditor.BucketThreshold;
    }
}

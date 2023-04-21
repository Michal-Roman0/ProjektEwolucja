using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tilemap_Controller : MonoBehaviour
{
    public MapData mapData;
    public MapEditor mapEditor;

    public Tilemap groundTilemap;
    public Tile groundTile;
    MapTile[,] mapTiles;
    bool[,] checkedPaintedOver;

    void Start()
    {
        InitializeNewMap();
    }

    public void InitializeNewMap() {
        mapTiles = new MapTile[mapData.MapWidth, mapData.MapHeight];
        checkedPaintedOver = new bool[mapData.MapWidth, mapData.MapHeight];

        for (int x = 0; x < mapData.MapWidth; x++) {
            for (int y = 0; y < mapData.MapHeight; y++) {
                int difficulty  = Random.Range(0, 101);
                int temperature = Random.Range(0, 101);
                int vegetation  = Random.Range(0, 101);

                mapTiles[x, y] = new MapTile(difficulty, temperature, vegetation);

                Vector3Int position = new Vector3Int(x, y, 0);

                groundTilemap.SetTile(position, groundTile);
                groundTilemap.SetTileFlags(position, TileFlags.None);
                groundTilemap.SetColor(position, mapTiles[x, y].GetColor(mapData.ActiveMap));

                checkedPaintedOver[x, y] = false;
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

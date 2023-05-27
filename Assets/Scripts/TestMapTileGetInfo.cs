using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMapTileGetInfo : MonoBehaviour
{
    public Vector3 position;

    private void Start() {
        position = new Vector3(10, 20, 0);
    }

    void Update() {
        Vector2Int tilePosition = new Vector2Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y));
        MapTile mapTile = Tilemap_Controller.instance.GetMapTile(tilePosition);

        if (mapTile != null) {
            Debug.Log($"{mapTile.GetValue(MapType.Difficulty)} {mapTile.GetValue(MapType.Vegetation)} {mapTile.GetValue(MapType.Temperature)}");
        }
    }
}

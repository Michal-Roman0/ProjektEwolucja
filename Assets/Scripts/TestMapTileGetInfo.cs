using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMapTileGetInfo : MonoBehaviour
{
    void Update() {
        Vector2Int tilePosition = new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));
        MapTile mapTile = Tilemap_Controller.instance.GetMapTile(tilePosition);

        if (mapTile != null) {
            Debug.Log($"{mapTile.GetValue(MapType.Difficulty)} {mapTile.GetValue(MapType.Vegetation)} {mapTile.GetValue(MapType.Temperature)}");
        }
    }
}

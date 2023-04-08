using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum MapType {
    Default = 0, Difficulty = 1, Temperature = 2, Vegetation = 3
}

[CreateAssetMenu(fileName = "MapData", menuName = "ProjektEwolucja/MapData", order = 0)]
public class MapData : ScriptableObject {
    public int MapWidth;
    public int MapHeight;

    public GameObject MapTile;
    public MapTile[,] Map;

    public MapType ActiveMap;
    public Vector2 ActiveTile;

    public float MinParameterValue = 0;
    public float MaxParameterValue = 100;

    public void InitializeNewMap() {
        Map = new MapTile[MapWidth, MapHeight];

        for (int x = 0; x < MapWidth; x++) {
            for (int y = 0; y < MapHeight; y++) {
                float difficulty = Random.Range(MinParameterValue, MaxParameterValue);
                float temperature = Random.Range(MinParameterValue, MaxParameterValue);
                float vegetation = Random.Range(MinParameterValue, MaxParameterValue);

                GameObject mapTileObject = Instantiate(MapTile, new Vector3(x, y, 0), Quaternion.identity);
                Map[x, y] = mapTileObject.GetComponent<MapTile>();
                Map[x, y].SetupMapTile(difficulty, temperature, vegetation);
            }
        }
    }

    public void ChangeMap(MapType mapType) {
        ActiveMap = mapType;

        for (int x = 0; x < MapWidth; x++) {
            for (int y = 0; y < MapHeight; y++) {
                Map[x, y].UpdateDisplayedColor();
            }
        }
    }

    public void ChangeTile(float value) {
        Map[(int)ActiveTile.x, (int)ActiveTile.y].ChangeValue(value * MaxParameterValue);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum MapType {
    Default = 0, Difficulty = 1, Temperature = 2, Vegetation = 3
}

[CreateAssetMenu(fileName = "MapData", menuName = "ProjektEwolucja/MapData", order = 0)]
public class MapData : ScriptableObject {
    public MapType ActiveMap = MapType.Vegetation;

    public int MapWidth;
    public int MapHeight;

    public bool IsPointerOverUI;

    // public void InitializeNewMap() {
    //     //Map = new MapTile[MapWidth, MapHeight];

    //     for (int x = 0; x < MapWidth; x++) {
    //         for (int y = 0; y < MapHeight; y++) {
    //             int difficulty  = Random.Range(MinParameterValue, MaxParameterValue + 1);
    //             int temperature = Random.Range(MinParameterValue, MaxParameterValue + 1);
    //             int vegetation  = Random.Range(MinParameterValue, MaxParameterValue + 1);

    //             //GameObject mapTileObject = Instantiate(MapTile, new Vector3(x, y, 0), Quaternion.identity);
    //             //Map[x, y] = mapTileObject.GetComponent<MapTile>();
    //             //Map[x, y].SetupMapTile(difficulty, temperature, vegetation);
    //         }
    //     }
    // }

    // public void ChangeMap(MapType mapType) {
    //     ActiveMap = mapType;

    //     for (int x = 0; x < MapWidth; x++) {
    //         for (int y = 0; y < MapHeight; y++) {
    //             Map[x, y].ChangeDisplayedColor();
    //         }
    //     }
    // }

    // public void Fill(int x, int y, int valueToFill) {
    //     Map[x, y].ChangeValue();

    //     if (x+1 < MapWidth  && Map[x+1, y].GetValue() == valueToFill) {
    //         Fill(x+1, y, valueToFill);
    //     }
    //     if (y+1 < MapHeight && Map[x, y+1].GetValue() == valueToFill) {
    //         Fill(x, y+1, valueToFill);
    //     }
    //     if (x-1 >= 0 && Map[x-1, y].GetValue() == valueToFill) {
    //         Fill(x-1, y, valueToFill);
    //     }
    //     if (y-1 >= 0 && Map[x, y-1].GetValue() == valueToFill) {
    //         Fill(x, y-1, valueToFill);
    //     }
    // }
}

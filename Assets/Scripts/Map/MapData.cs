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
}

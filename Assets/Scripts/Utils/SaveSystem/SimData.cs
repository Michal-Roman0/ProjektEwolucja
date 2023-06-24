using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SimData
{
    [SerializeField]
    public int mapWidth;
    [SerializeField]
    public int mapHeight;
    [SerializeField]
    public List<SerializableTile> mapTiles;
    [SerializeField]
    public float mapDiff;
    [SerializeField]
    public float mapTemp;
    [SerializeField]
    public float mapVege;
    [SerializeField]
    public List<SerializableUnit> Herbivores;
    [SerializeField]
    public List<SerializableUnit> Carnivores;
    [SerializeField] 
    public List<Vector3> Porkchops;
    public SimData()
    {
        Herbivores = new List<SerializableUnit>();
        Carnivores = new List<SerializableUnit>();
        Porkchops = new List<Vector3>();
        mapTiles = new List<SerializableTile>();
    }

    public void SaveTiles(MapTile[,] tiles)
    {
        for (int x = 0; x < mapWidth; x++)
            for (int y = 0; y < mapHeight; y++)
                mapTiles.Add(tiles[x, y]);
                
    }
}

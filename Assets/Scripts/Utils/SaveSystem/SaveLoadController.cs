using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UIElements;

public class SaveLoadController : MonoBehaviour
{
    public static SaveLoadController instance;

    public GameObject herbivorePrefab;
    public GameObject carnivorePrefab;
    public GameObject meatPrefab;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SaveSim(string saveName)
    {
        if (string.IsNullOrEmpty(saveName))
        {
            return;
        }
        SimData simData = CreateSimDataFromScene();
        string data = JsonUtility.ToJson(simData,true);
        using (StreamWriter sw = new StreamWriter(Application.persistentDataPath + $"/{saveName}.json"))
        {
            sw.Write(data);
        }
    }
    public void LoadSim(string saveName)
    {
        if (!File.Exists(Application.persistentDataPath + $"/{saveName}.json"))
        {
            Debug.Log("Plik nie istnieje");
            return;
        }

        string data = "";
        using (StreamReader sr = new StreamReader(Application.persistentDataPath + $"/{saveName}.json"))
        {
            data = sr.ReadToEnd();
        }
        SimData simData = JsonUtility.FromJson<SimData>(data);
        DestroyObjectsFromScene();
        LoadMapFromSave(simData);
        GenerateObjectsFromSimData(simData);
    }
    //DO TESTÓW ODKOMENTOWAÆ
    /*
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            SaveSim("Save0");
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            LoadSim("Save0");
        }
    }*/
    public SimData CreateSimDataFromScene()
    {
        SimData temp = new SimData();
        temp.mapDiff = Tilemap_Controller.instance.DiffOffset;
        temp.mapTemp = Tilemap_Controller.instance.TempOffset;
        temp.mapVege = Tilemap_Controller.instance.VegeOffset;
        temp.mapHeight = Tilemap_Controller.instance.mapData.MapHeight;
        temp.mapWidth = Tilemap_Controller.instance.mapData.MapWidth;
        temp.SaveTiles(Tilemap_Controller.instance.MapTiles);
        foreach (GameObject herbivore in GameObject.FindGameObjectsWithTag("Herbivore"))
        {
            temp.Herbivores.Add(herbivore.GetComponent<UnitController>().GetUnitInfo());
        }
        foreach (GameObject carnivore in GameObject.FindGameObjectsWithTag("Carnivore"))
        {
            temp.Carnivores.Add(carnivore.GetComponent<UnitController>().GetUnitInfo());
        }
        foreach (GameObject meat in GameObject.FindGameObjectsWithTag("Meat"))
        {
            temp.Porkchops.Add(meat.transform.position);
        }
        return temp;
    }
    public void DestroyObjectsFromScene()
    {
        foreach(GameObject herbivore in GameObject.FindGameObjectsWithTag("Herbivore"))
        {
            Destroy(herbivore);
        }
        foreach (GameObject carnivore in GameObject.FindGameObjectsWithTag("Carnivore"))
        {
            Destroy(carnivore);
        }
        foreach (GameObject meat in GameObject.FindGameObjectsWithTag("Meat"))
        {
            Destroy(meat);
        }
    }
    public void LoadMapFromSave(SimData simData)
    {
        Tilemap_Controller.instance.mapData.MapWidth = simData.mapWidth;
        Tilemap_Controller.instance.mapData.MapHeight = simData.mapHeight;

        MapTile[,] temp = new MapTile[simData.mapWidth, simData.mapHeight];
        int i = 0;
        for (int x = 0; x < simData.mapWidth; x++)
            for (int y = 0; y < simData.mapHeight; y++)
            {
                temp[x, y] = simData.mapTiles[i];
                i++;
            }

        Tilemap_Controller.instance.LoadDataFromSave(simData.mapDiff, simData.mapTemp, simData.mapVege, temp);
        Tilemap_Controller.instance.ChangeMap();
    }
    public void GenerateObjectsFromSimData(SimData simData)
    {
        foreach(SerializableUnit info in simData.Herbivores)
        {
            gameObject.Instantiate(herbivorePrefab, info.location, Quaternion.identity, info);
        }
        foreach (SerializableUnit info in simData.Carnivores)
        {
            gameObject.Instantiate(carnivorePrefab, info.location, Quaternion.identity, info);
        }
        foreach (Vector3 v in simData.Porkchops)
        {
            Instantiate(meatPrefab, v, Quaternion.identity);
        }
    }
}

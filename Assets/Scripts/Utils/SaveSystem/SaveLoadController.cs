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
    public GameObject plantPrefab;
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
    public void SaveSim(int fileNumber)
    {
        SimData simData = CreateSimDataFromScene();
        Debug.Log("Zapisano plik: " + Application.persistentDataPath + $"/SaveGame{fileNumber}.json");
        string data = JsonUtility.ToJson(simData,true);
        using (StreamWriter sw = new StreamWriter(Application.persistentDataPath + $"/SaveGame{fileNumber}.json"))
        {
            sw.Write(data);
        }
    }
    public void LoadSim(int fileNumber)
    {
        if (!File.Exists(Application.persistentDataPath + $"/SaveGame{fileNumber}.json"))
        {
            Debug.Log("Plik nie istnieje");
            return;
        }

        string data = "";
        using (StreamReader sr = new StreamReader(Application.persistentDataPath + $"/SaveGame{fileNumber}.json"))
        {
            data = sr.ReadToEnd();
        }
        SimData simData = JsonUtility.FromJson<SimData>(data);
        DestroyObjectsFromScene();
        GenerateObjectsFromSimData(simData);
    }
    //DO TESTÓW ODKOMENTOWAÆ
    /*private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            SaveSim(0);
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            LoadSim(0);
        }
    }*/
    public SimData CreateSimDataFromScene()
    {
        SimData temp = new SimData();
        temp.mapDiff = Tilemap_Controller.instance.DiffOffset;
        temp.mapTemp = Tilemap_Controller.instance.TempOffset;
        temp.mapVege = Tilemap_Controller.instance.VegeOffset;
        foreach (GameObject herbivore in GameObject.FindGameObjectsWithTag("Herbivore"))
        {
            temp.Herbivores.Add(herbivore.transform.position);
        }
        foreach (GameObject carnivore in GameObject.FindGameObjectsWithTag("Carnivore"))
        {
            temp.Carnivores.Add(carnivore.transform.position);
        }
        foreach (GameObject plant in GameObject.FindGameObjectsWithTag("Plant"))
        {
            temp.Fruits.Add(plant.transform.position);
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
        foreach (GameObject plant in GameObject.FindGameObjectsWithTag("Plant"))
        {
            Destroy(plant);
        }
        foreach (GameObject meat in GameObject.FindGameObjectsWithTag("Meat"))
        {
            Destroy(meat);
        }
    }
    public void GenerateObjectsFromSimData(SimData simData)
    {
        Tilemap_Controller.instance.InitializeNewMap(simData.mapDiff, simData.mapTemp, simData.mapVege);
        foreach(Vector3 v in simData.Herbivores)
        {
            Instantiate(herbivorePrefab, v, Quaternion.identity);
        }
        foreach (Vector3 v in simData.Carnivores)
        {
            Instantiate(carnivorePrefab, v, Quaternion.identity);
        }
        foreach (Vector3 v in simData.Fruits)
        {
            Instantiate(plantPrefab, v, Quaternion.identity);
        }
        foreach (Vector3 v in simData.Porkchops)
        {
            Instantiate(meatPrefab, v, Quaternion.identity);
        }
    }
}

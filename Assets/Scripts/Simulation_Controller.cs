using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation_Controller : MonoBehaviour
{
    public static Simulation_Controller instance;

    Tilemap_Controller tilemapController;

    public GameObject HerbivorePrefab;
    public GameObject CarnivorePrefab;

    bool simulationRunning;
    float savedTimeScale;

    public float mutationProbability;

    void Start()
    {
        if (instance == null) {
            instance = this;
        }
        
        tilemapController = Tilemap_Controller.instance;

        Time.timeScale = 0;
        savedTimeScale = 0;
        simulationRunning = false;

        // if (newSimulation) spawnOrganisms
        // else if (loadSimulation) updateMap putOrganismsAndUpdateTheirStats

        SpawnOrganisms();
    }

    private void SpawnOrganisms() {
        int organisms = SimulationStartData.Organisms_Number;
        float proportion = SimulationStartData.Organisms_Proportion;

        int herbivores = (int)(organisms * proportion);
        int carnivores = organisms - herbivores;

        int mapXmin = 10, mapXmax = tilemapController.mapData.MapWidth - 10;
        int mapYmin = 10, mapYmax = tilemapController.mapData.MapHeight - 10;

        for (int i = 0; i < herbivores; i++) {
            Instantiate(HerbivorePrefab, new Vector3(Random.Range(mapXmin, mapXmax), Random.Range(mapYmin, mapYmax), 0), Quaternion.identity);
        }
        for (int i = 0; i < carnivores; i++) {
            Instantiate(CarnivorePrefab, new Vector3(Random.Range(mapXmin, mapXmax), Random.Range(mapYmin, mapYmax), 0), Quaternion.identity);
        }
    }

    public bool PlayPauseSimulation() {
        simulationRunning = !simulationRunning;

        Time.timeScale = simulationRunning ? savedTimeScale : 0;

        return simulationRunning;
    }

    public void SetSimulationSpeed(float speed)
    {
        savedTimeScale = speed;

        if (simulationRunning)
            Time.timeScale = speed;
    }
}

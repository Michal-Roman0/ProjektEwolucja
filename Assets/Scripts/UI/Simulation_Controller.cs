using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation_Controller : MonoBehaviour
{
    public Tilemap_Controller tilemapController;

    bool simulationRunning;
    float savedTimeScale;

    void Start()
    {
        Time.timeScale = 0;
        savedTimeScale = 0;
        simulationRunning = false;
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

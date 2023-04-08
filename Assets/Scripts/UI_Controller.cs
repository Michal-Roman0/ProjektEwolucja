using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] Sprite PauseIcon;
    [SerializeField] Sprite PlayIcon;
    [SerializeField] Button PlayPauseButton;

    public GameObject simulation;
    public MapData mapData;

    public void PlayPauseButtonClicked()
    {
        bool simulationRunning = simulation.GetComponent<Simulation_Controller>().PlayPauseSimulation();

        PlayPauseButton.image.sprite = simulationRunning ? PauseIcon : PlayIcon;
    }

    public void SpeedButtonClicked(int speed) {
        simulation.GetComponent<Simulation_Controller>().SetSimulationSpeed(speed);
    }

    public void ChangeMapButtonClicked_Default() { ChangeMapButtonClicked(MapType.Default); }
    public void ChangeMapButtonClicked_Difficulty() { ChangeMapButtonClicked(MapType.Difficulty); }
    public void ChangeMapButtonClicked_Temperature() { ChangeMapButtonClicked(MapType.Temperature); }
    public void ChangeMapButtonClicked_Vegetation() { ChangeMapButtonClicked(MapType.Vegetation); }

    void ChangeMapButtonClicked(MapType mapType) {
        mapData.ChangeMap(mapType);
    }

    public void OnScrollbarChange(float value) {
        mapData.ChangeTile(value);
    }
}

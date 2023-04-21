using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] Sprite PauseIcon;
    [SerializeField] Sprite PlayIcon;
    [SerializeField] Button PlayPauseButton;

    public GameObject simulation;
    public MapData mapData;

    void Update() {
        mapData.IsPointerOverUI = EventSystem.current.IsPointerOverGameObject();
    }

    public void PlayPauseButtonClicked()
    {
        bool simulationRunning = simulation.GetComponent<Simulation_Controller>().PlayPauseSimulation();
        PlayPauseButton.image.sprite = simulationRunning ? PauseIcon : PlayIcon;
    }

    public void SpeedButtonClicked(int speed) {
        simulation.GetComponent<Simulation_Controller>().SetSimulationSpeed(speed);
    }

    public void ChangeMapButtonClicked_Default()     { ChangeMapButtonClicked(MapType.Default);     }
    public void ChangeMapButtonClicked_Difficulty()  { ChangeMapButtonClicked(MapType.Difficulty);  }
    public void ChangeMapButtonClicked_Temperature() { ChangeMapButtonClicked(MapType.Temperature); }
    public void ChangeMapButtonClicked_Vegetation()  { ChangeMapButtonClicked(MapType.Vegetation);  }

    void ChangeMapButtonClicked(MapType mapType) {
        //mapData.ChangeMap(mapType);
    }



    public MapEditor mapEditor;
    public GameObject Group_BrushOptions;
    public GameObject Group_BucketOptions;
    public GameObject Group_PointerOptions;

    public void ToolChangeBrush() {
        Group_BrushOptions.SetActive(true);
        Group_BucketOptions.SetActive(false);
        Group_PointerOptions.SetActive(false);
        mapEditor.BrushActive = true;
        mapEditor.BucketActive = false;
    }
    public void ToolChangeBucket() {
        Group_BrushOptions.SetActive(false);
        Group_BucketOptions.SetActive(true);
        Group_PointerOptions.SetActive(false);
        mapEditor.BrushActive = false;
        mapEditor.BucketActive = true;
    }
    public void ToolChangePointer() {
        Group_BrushOptions.SetActive(false);
        Group_BucketOptions.SetActive(false);
        Group_PointerOptions.SetActive(true);
        mapEditor.BrushActive = false;
        mapEditor.BucketActive = false;
    }

    public void BrushChangeMode(int mode) {
        mapEditor.BrushMode = mode;
    }
    public void BrushChangeShape(int shape) {
        mapEditor.BrushShape = shape;
    }
    public void BrushChangeSize(float value) {
        mapEditor.BrushSize = value == 0 ? 1 : (int)(value*100);
    }
    public void BrushChangeValue(float value) {
        mapEditor.BrushValue = (int)(value*100);
    }

    public void BucketChangeMode(int mode) {
        mapEditor.BucketMode = mode;
    }
    public void BucketChangeShape(int shape) {
        mapEditor.BucketShape = shape;
    }
    public void BucketChangeThreshold(float value) {
        mapEditor.BucketThreshold = (int)(value*100);
    }
    public void BucketChangeValue(float value) {
        mapEditor.BucketValue = (int)(value*100);
    }
}

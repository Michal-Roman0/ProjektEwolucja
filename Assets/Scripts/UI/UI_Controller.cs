using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] Sprite PauseIcon;
    [SerializeField] Sprite PlayIcon;
    [SerializeField] Button PlayPauseButton;

    public GameObject simulation;
    public MapData mapData;

    private void Start() {
        ToggleGroup_Tools = Panel_MapEditor.transform.Find("ToggleGroup_Tools").gameObject;
        ButtonMap_Hide = Panel_MapEditor.transform.Find("ButtonMap_Hide").gameObject;

        Group_BrushOptions = Panel_MapEditor.transform.Find("Group_BrushOptions").gameObject;
        Text_BrushSize = Group_BrushOptions.transform.Find("Group_Size/Text_Size").gameObject.GetComponent<TextMeshProUGUI>();
        Text_BrushValue = Group_BrushOptions.transform.Find("Group_Value/Text_Value").gameObject.GetComponent<TextMeshProUGUI>();

        Group_BucketOptions = Panel_MapEditor.transform.Find("Group_BucketOptions").gameObject;
        Text_BucketThreshold = Group_BucketOptions.transform.Find("Group_Threshold/Text_Threshold").gameObject.GetComponent<TextMeshProUGUI>();
        Text_BucketValue = Group_BucketOptions.transform.Find("Group_Value/Text_Value").gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
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



    [SerializeField] Tilemap_Controller tilemap;

    public void ChangeMapButtonClicked_Default()     { ChangeMapButtonClicked(MapType.Default);     }
    public void ChangeMapButtonClicked_Difficulty()  { ChangeMapButtonClicked(MapType.Difficulty);  }
    public void ChangeMapButtonClicked_Temperature() { ChangeMapButtonClicked(MapType.Temperature); }
    public void ChangeMapButtonClicked_Vegetation()  { ChangeMapButtonClicked(MapType.Vegetation);  }

    void ChangeMapButtonClicked(MapType mapType) {
        mapData.ActiveMap = mapType;
        tilemap.ChangeMap();
    }



    public MapEditor mapEditor;
    public GameObject Panel_MapEditor;
    GameObject ToggleGroup_Tools;
    GameObject ButtonMap_Hide;
    GameObject Group_BrushOptions;
    GameObject Group_BucketOptions;
    //GameObject Group_PointerOptions;

    public void ToolChangeBrush() {
        Group_BrushOptions.SetActive(true);
        Group_BucketOptions.SetActive(false);
        //Group_PointerOptions.SetActive(false);
        mapEditor.BrushActive = true;
        mapEditor.BucketActive = false;

        //Panel_MapEditor.GetComponent<RectTransform>().offsetMax = new Vector2(-860, -320);
        //ToggleGroup_Tools.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-10, 80, 0);
        //ButtonMap_Hide.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(80, 80, 0);
    }
    public void ToolChangeBucket() {
        Group_BrushOptions.SetActive(false);
        Group_BucketOptions.SetActive(true);
        //Group_PointerOptions.SetActive(false);
        mapEditor.BrushActive = false;
        mapEditor.BucketActive = true;
        
        //Panel_MapEditor.GetComponent<RectTransform>().offsetMax = new Vector2(-860, -320);
        //ToggleGroup_Tools.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-10, 80, 0);
        //ButtonMap_Hide.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(80, 80, 0);
    }
    public void ToolChangePointer() {
        Group_BrushOptions.SetActive(false);
        Group_BucketOptions.SetActive(false);
        //Group_PointerOptions.SetActive(true);
        mapEditor.BrushActive = false;
        mapEditor.BucketActive = false;
        
        //Panel_MapEditor.GetComponent<RectTransform>().offsetMax = new Vector2(-860, -500);
        //ToggleGroup_Tools.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-10, 0, 0);
        //ButtonMap_Hide.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(80, 0, 0);
    }

    TextMeshProUGUI Text_BrushSize;
    TextMeshProUGUI Text_BrushValue;
    TextMeshProUGUI Text_BucketThreshold;
    TextMeshProUGUI Text_BucketValue;

    public void BrushChangeMode(int mode) {
        mapEditor.BrushMode = mode;
    }
    public void BrushChangeShape(int shape) {
        mapEditor.BrushShape = shape;
    }
    public void BrushChangeSize(float value) {
        mapEditor.BrushSize = value == 0 ? 1 : (int)(value*100);
        Text_BrushSize.text = $"{mapEditor.BrushSize}";
    }
    public void BrushChangeValue(float value) {
        mapEditor.BrushValue = (int)(value*100);
        Text_BrushValue.text = $"{mapEditor.BrushValue}";
    }

    public void BucketChangeMode(int mode) {
        mapEditor.BucketMode = mode;
    }
    public void BucketChangeShape(int shape) {
        mapEditor.BucketShape = shape;
    }
    public void BucketChangeThreshold(float value) {
        mapEditor.BucketThreshold = (int)(value*100);
        Text_BucketThreshold.text = $"{mapEditor.BucketThreshold}";
    }
    public void BucketChangeValue(float value) {
        mapEditor.BucketValue = (int)(value*100);
        Text_BucketValue.text = $"{mapEditor.BucketValue}";
    }
}

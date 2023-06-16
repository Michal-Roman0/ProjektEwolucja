using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_Controller : MonoBehaviour
{
    public static UI_Controller instance;

    [SerializeField] Sprite PauseIcon;
    [SerializeField] Sprite PlayIcon;
    [SerializeField] Button PlayPauseButton;

    public GameObject simulation;
    public MapData mapData;

    private void Start() {
        if (instance == null) {
            instance = this;
        }


        GameObject top = Panel_OrganismStats.transform.Find("Top").gameObject;
        Organism_Image = top.transform.Find("Organism_Image").GetComponent<Image>();
        Organism_Name = top.transform.Find("Organism_Name").GetComponent<TextMeshProUGUI>();
        Diet_Value = top.transform.Find("Diet_Value").GetComponent<TextMeshProUGUI>();
        Size_Value = top.transform.Find("Size_Value").GetComponent<TextMeshProUGUI>();

        GameObject stats = Panel_OrganismStats.transform.Find("Stats").gameObject;
        Agility_Value = stats.transform.Find("Agility_Value").GetComponent<TextMeshProUGUI>();
        Strength_Value = stats.transform.Find("Strength_Value").GetComponent<TextMeshProUGUI>();


        ToggleGroup_Tools = Panel_MapEditor.transform.Find("ToggleGroup_Tools").gameObject;
        ButtonMap_Hide = Panel_MapEditor.transform.Find("ButtonMap_Hide").gameObject;

        Group_BrushOptions = Panel_MapEditor.transform.Find("Group_BrushOptions").gameObject;
        Text_BrushSize = Group_BrushOptions.transform.Find("Group_Size/Text_Size").gameObject.GetComponent<TextMeshProUGUI>();
        Text_BrushValue = Group_BrushOptions.transform.Find("Group_Value/Text_Value").gameObject.GetComponent<TextMeshProUGUI>();

        Group_BucketOptions = Panel_MapEditor.transform.Find("Group_BucketOptions").gameObject;
        Text_BucketThreshold = Group_BucketOptions.transform.Find("Group_Threshold/Text_Threshold").gameObject.GetComponent<TextMeshProUGUI>();
        Text_BucketValue = Group_BucketOptions.transform.Find("Group_Value/Text_Value").gameObject.GetComponent<TextMeshProUGUI>();

        Group_PointerOptions = Panel_MapEditor.transform.Find("Group_PointerOptions").gameObject;


        SetActive_OrganismStats(false);
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



    public GameObject Panel_OrganismStats;
    Image Organism_Image;
    TextMeshProUGUI Organism_Name;
    TextMeshProUGUI Diet_Value;
    TextMeshProUGUI Size_Value;
    TextMeshProUGUI Agility_Value;
    TextMeshProUGUI Strength_Value;

    public void ShowUnitStats(string organismName, bool eatsMeat, bool eatsPlants, float size, Sprite sprite, Color color, float agility, float strength) {
        SetActive_OrganismStats(true);

        Organism_Image.sprite = sprite;
        Organism_Image.color = color;
        Organism_Name.text = organismName;
        Diet_Value.text = eatsMeat ? (eatsPlants ? "Omnivore" : "Carnivore") : (eatsPlants ? "Herbivore" : "Nothing");
        Size_Value.text = size.ToString();

        Agility_Value.text = agility.ToString();
        Strength_Value.text = strength.ToString();
    }

    public void SetActive_OrganismStats(bool value) {
        Panel_OrganismStats.SetActive(value);
    }



    public MapEditor mapEditor;
    public GameObject Panel_MapEditor;
    GameObject ToggleGroup_Tools;
    GameObject ButtonMap_Hide;
    GameObject Group_BrushOptions;
    GameObject Group_BucketOptions;
    GameObject Group_PointerOptions;

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

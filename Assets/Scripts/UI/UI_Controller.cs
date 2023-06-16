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

    UnitController focusedOrganismController;
    bool isFocused = false;

    private void Start() {
        if (instance == null) {
            instance = this;
        }


        MutationProbability_Label = Panel_Bottom.transform.Find("MutationProbability_Label").GetComponent<TextMeshProUGUI>();


        GameObject top = Panel_OrganismStats.transform.Find("Top").gameObject;
        Organism_Image = top.transform.Find("Organism_Image").GetComponent<Image>();
        Organism_Name = top.transform.Find("Organism_Name").GetComponent<TextMeshProUGUI>();
        Diet_Value = top.transform.Find("Diet_Value").GetComponent<TextMeshProUGUI>();
        Size_Value = top.transform.Find("Size_Value").GetComponent<TextMeshProUGUI>();
        Age_Value = top.transform.Find("Age_Value").GetComponent<TextMeshProUGUI>();

        GameObject stats = Panel_OrganismStats.transform.Find("Stats").gameObject;
        Agility_Value = stats.transform.Find("Agility_Value").GetComponent<TextMeshProUGUI>();
        Strength_Value = stats.transform.Find("Strength_Value").GetComponent<TextMeshProUGUI>();
        Sight_Value = stats.transform.Find("Sight_Value").GetComponent<TextMeshProUGUI>();

        Damage_Value = stats.transform.Find("Damage_Value").GetComponent<TextMeshProUGUI>();
        Threat_Value = stats.transform.Find("Threat_Value").GetComponent<TextMeshProUGUI>();
        Radius_Value = stats.transform.Find("Radius_Value").GetComponent<TextMeshProUGUI>();
        Stamina_Value = stats.transform.Find("Stamina_Value").GetComponent<TextMeshProUGUI>();
        MaxSpeed_Value = stats.transform.Find("MaxSpeed_Value").GetComponent<TextMeshProUGUI>();

        Hunger_Value = stats.transform.Find("Hunger_Value").GetComponent<TextMeshProUGUI>();
        Energy_Value = stats.transform.Find("Energy_Value").GetComponent<TextMeshProUGUI>();


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

        if (isFocused)
            UpdateUnitStats();
    }



    public void PlayPauseButtonClicked()
    {
        bool simulationRunning = simulation.GetComponent<Simulation_Controller>().PlayPauseSimulation();
        PlayPauseButton.image.sprite = simulationRunning ? PauseIcon : PlayIcon;
    }

    public void SpeedButtonClicked(int speed) {
        simulation.GetComponent<Simulation_Controller>().SetSimulationSpeed(speed);
    }



    public GameObject Panel_Bottom;
    TextMeshProUGUI MutationProbability_Label;

    public void UpdateMutationProbabilityText(int value) {
        MutationProbability_Label.text = $"Mutation Probability: {PercentFormat(value)}";
    }



    [SerializeField] Tilemap_Controller tilemap;

    public void ChangeMapButtonClicked_Default() {
        ChangeMapButtonClicked(MapType.Default);
    }
    public void ChangeMapButtonClicked_Difficulty() {
        ChangeMapButtonClicked(MapType.Difficulty);
    }
    public void ChangeMapButtonClicked_Temperature() {
        ChangeMapButtonClicked(MapType.Temperature);
    }
    public void ChangeMapButtonClicked_Vegetation() {
        ChangeMapButtonClicked(MapType.Vegetation);
    }

    void ChangeMapButtonClicked(MapType mapType) {
        mapData.ActiveMap = mapType;
        tilemap.ChangeMap();
    }



    public GameObject Panel_OrganismStats;
    Image Organism_Image;
    TextMeshProUGUI Organism_Name;
    TextMeshProUGUI Diet_Value;
    TextMeshProUGUI Size_Value;
    TextMeshProUGUI Age_Value;

    TextMeshProUGUI Agility_Value;
    TextMeshProUGUI Strength_Value;
    TextMeshProUGUI Sight_Value;

    TextMeshProUGUI Damage_Value;
    TextMeshProUGUI Threat_Value;
    TextMeshProUGUI Radius_Value;
    TextMeshProUGUI Stamina_Value;
    TextMeshProUGUI MaxSpeed_Value;

    TextMeshProUGUI Hunger_Value;
    TextMeshProUGUI Energy_Value;

    public void UpdateFocusedUnit(GameObject organism) {
        focusedOrganismController = organism.GetComponent<UnitController>();

        SetActive_OrganismStats(true);
        SetUnitStats(organism);
        UpdateUnitStats();
    }

    private void SetUnitStats(GameObject organism) {
        SpriteRenderer sprRend = organism.GetComponent<SpriteRenderer>();

        Organism_Image.sprite = sprRend.sprite;
        Organism_Image.color = sprRend.color;
        Organism_Name.text = organism.name;

        Diet_Value.text = focusedOrganismController.eatsMeat
            ? (focusedOrganismController.eatsPlants ? "Omnivore" : "Carnivore")
            : (focusedOrganismController.eatsPlants ? "Herbivore" : "Nothing");
        Size_Value.text = FloatFormat(focusedOrganismController.size);

        Agility_Value.text = FloatFormat(focusedOrganismController.agility);
        Strength_Value.text = FloatFormat(focusedOrganismController.strength);
        Sight_Value.text = FloatFormat(focusedOrganismController.sight);

        Damage_Value.text = FloatFormat(focusedOrganismController.damage);
        Threat_Value.text = FloatFormat(focusedOrganismController.threat);
        Radius_Value.text = FloatFormat(focusedOrganismController.radius);
        Stamina_Value.text = FloatFormat(focusedOrganismController.stamina);
        MaxSpeed_Value.text = FloatFormat(focusedOrganismController.maxSpeed);
    }
    public string FloatFormat(float value) {
        return value.ToString("F2");
    }
    public string PercentFormat(float value) {
        return $"{value} %";
    }
    public string FractionFormat(float value, float max) {
        return $"{value}/{max}";
    }
    public void UpdateHunger() {
        Hunger_Value.text = PercentFormat(focusedOrganismController.hunger);
    }
    public void UpdateEnergy() {
        Energy_Value.text = FractionFormat(
            focusedOrganismController.energy,
            focusedOrganismController.maxEnergy
        );
    }
    public void UpdateAge() {
        Age_Value.text = FractionFormat(
            focusedOrganismController.age,
            focusedOrganismController.maxAge
        );
    }
    public void UpdateUnitStats() {
        UpdateHunger();
        UpdateEnergy();
        UpdateAge();
    }

    public void SetActive_OrganismStats(bool value) {
        isFocused = value;
        Panel_OrganismStats.SetActive(value);
    }



    public MapEditor mapEditor;
    public GameObject Panel_MapEditor;
    GameObject ToggleGroup_Tools;
    GameObject ButtonMap_Hide;
    GameObject Group_BrushOptions;
    GameObject Group_BucketOptions;
    GameObject Group_PointerOptions;

    public void ToolChange(string activeGroup) {
        Group_BrushOptions.SetActive(activeGroup == "Brush");
        Group_BucketOptions.SetActive(activeGroup == "Bucket");
        Group_PointerOptions.SetActive(activeGroup == "Pointer");
        mapEditor.BrushActive = (activeGroup == "Brush");
        mapEditor.BucketActive = (activeGroup == "Bucket");
    }

    public void ToolChangeBrush() {
        ToolChange("Brush");
    }

    public void ToolChangeBucket() {
        ToolChange("Bucket");
    }

    public void ToolChangePointer() {
        ToolChange("Pointer");
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

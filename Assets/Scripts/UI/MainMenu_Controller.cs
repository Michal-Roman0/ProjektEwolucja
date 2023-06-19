using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu_Controller : MonoBehaviour
{
    public GameObject HerbivoreStats_Panel;
    TextMeshProUGUI Herbivore_Agility_Value_1, Herbivore_Agility_Value_2;
    TextMeshProUGUI Herbivore_Strength_Value_1, Herbivore_Strength_Value_2;
    TextMeshProUGUI Herbivore_Sight_Value_1, Herbivore_Sight_Value_2;
    TextMeshProUGUI Herbivore_Size_Value_1, Herbivore_Size_Value_2;

    public GameObject CarnivoreStats_Panel;
    TextMeshProUGUI Carnivore_Agility_Value_1, Carnivore_Agility_Value_2;
    TextMeshProUGUI Carnivore_Strength_Value_1, Carnivore_Strength_Value_2;
    TextMeshProUGUI Carnivore_Sight_Value_1, Carnivore_Sight_Value_2;
    TextMeshProUGUI Carnivore_Size_Value_1, Carnivore_Size_Value_2;

    public GameObject Organisms_Panel;
    TextMeshProUGUI Organisms_Number_Value;
    TextMeshProUGUI Organisms_Proportion_Value;

    float h_agility_1, h_agility_2, c_agility_1, c_agility_2;
    float h_strength_1, h_strength_2, c_strength_1, c_strength_2;
    float h_sight_1, h_sight_2, c_sight_1, c_sight_2;
    float h_size_1, h_size_2, c_size_1, c_size_2;
    int organisms;
    float proportion;
    

    void Start()
    {
        GameObject AgilityH = HerbivoreStats_Panel.transform.Find("Agility").gameObject;
        Herbivore_Agility_Value_1 = AgilityH.transform.Find("Slider_1_Value").GetComponent<TextMeshProUGUI>();
        Herbivore_Agility_Value_2 = AgilityH.transform.Find("Slider_2_Value").GetComponent<TextMeshProUGUI>();

        GameObject StrengthH = HerbivoreStats_Panel.transform.Find("Strength").gameObject;
        Herbivore_Strength_Value_1 = StrengthH.transform.Find("Slider_1_Value").GetComponent<TextMeshProUGUI>();
        Herbivore_Strength_Value_2 = StrengthH.transform.Find("Slider_2_Value").GetComponent<TextMeshProUGUI>();

        GameObject SightH = HerbivoreStats_Panel.transform.Find("Sight").gameObject;
        Herbivore_Sight_Value_1 = SightH.transform.Find("Slider_1_Value").GetComponent<TextMeshProUGUI>();
        Herbivore_Sight_Value_2 = SightH.transform.Find("Slider_2_Value").GetComponent<TextMeshProUGUI>();

        GameObject SizeH = HerbivoreStats_Panel.transform.Find("Size").gameObject;
        Herbivore_Size_Value_1 = SizeH.transform.Find("Slider_1_Value").GetComponent<TextMeshProUGUI>();
        Herbivore_Size_Value_2 = SizeH.transform.Find("Slider_2_Value").GetComponent<TextMeshProUGUI>();


        GameObject AgilityC = CarnivoreStats_Panel.transform.Find("Agility").gameObject;
        Carnivore_Agility_Value_1 = AgilityC.transform.Find("Slider_1_Value").GetComponent<TextMeshProUGUI>();
        Carnivore_Agility_Value_2 = AgilityC.transform.Find("Slider_2_Value").GetComponent<TextMeshProUGUI>();

        GameObject StrengthC = CarnivoreStats_Panel.transform.Find("Strength").gameObject;
        Carnivore_Strength_Value_1 = StrengthC.transform.Find("Slider_1_Value").GetComponent<TextMeshProUGUI>();
        Carnivore_Strength_Value_2 = StrengthC.transform.Find("Slider_2_Value").GetComponent<TextMeshProUGUI>();

        GameObject SightC = CarnivoreStats_Panel.transform.Find("Sight").gameObject;
        Carnivore_Sight_Value_1 = SightC.transform.Find("Slider_1_Value").GetComponent<TextMeshProUGUI>();
        Carnivore_Sight_Value_2 = SightC.transform.Find("Slider_2_Value").GetComponent<TextMeshProUGUI>();

        GameObject SizeC = CarnivoreStats_Panel.transform.Find("Size").gameObject;
        Carnivore_Size_Value_1 = SizeC.transform.Find("Slider_1_Value").GetComponent<TextMeshProUGUI>();
        Carnivore_Size_Value_2 = SizeC.transform.Find("Slider_2_Value").GetComponent<TextMeshProUGUI>();


        Organisms_Number_Value = Organisms_Panel.transform.Find("Number_Value").GetComponent<TextMeshProUGUI>();
        Organisms_Proportion_Value = Organisms_Panel.transform.Find("Proportion_Value").GetComponent<TextMeshProUGUI>();

        UpdateHerbivoreAgilityValue1(0.1f);
        UpdateHerbivoreAgilityValue2(0.1f);
        UpdateHerbivoreSightValue1(0.1f);
        UpdateHerbivoreSightValue2(0.1f);
        UpdateHerbivoreSizeValue1(0.1f);
        UpdateHerbivoreSizeValue2(0.1f);
        UpdateHerbivoreStrengthValue1(0.1f);
        UpdateHerbivoreStrengthValue2(0.1f);

        UpdateCarnivoreAgilityValue1(0.1f);
        UpdateCarnivoreAgilityValue2(0.1f);
        UpdateCarnivoreSightValue1(0.1f);
        UpdateCarnivoreSightValue2(0.1f);
        UpdateCarnivoreSizeValue1(0.1f);
        UpdateCarnivoreSizeValue2(0.1f);
        UpdateCarnivoreStrengthValue1(0.1f);
        UpdateCarnivoreStrengthValue2(0.1f);

        UpdateOrganismsNumberValue(10);
        UpdateOrganismsProportionValue(0);
    }



    public void NewSimulation() {
        float maxValue, minValue;

        maxValue = Mathf.Max(h_agility_1, h_agility_2);
        minValue = Mathf.Min(h_agility_1, h_agility_2);
        SimulationStartData.Herbivore_AgilityMax = maxValue;
        SimulationStartData.Herbivore_AgilityMin = minValue;

        maxValue = Mathf.Max(h_strength_1, h_strength_2);
        minValue = Mathf.Min(h_strength_1, h_strength_2);
        SimulationStartData.Herbivore_StrengthMax = maxValue;
        SimulationStartData.Herbivore_StrengthMin = minValue;

        maxValue = Mathf.Max(h_sight_1, h_sight_2);
        minValue = Mathf.Min(h_sight_1, h_sight_2);
        SimulationStartData.Herbivore_SightMax = maxValue;
        SimulationStartData.Herbivore_SightMin = minValue;

        maxValue = Mathf.Max(h_size_1, h_size_2);
        minValue = Mathf.Min(h_size_1, h_size_2);
        SimulationStartData.Herbivore_SizeMax = maxValue;
        SimulationStartData.Herbivore_SizeMin = minValue;


        maxValue = Mathf.Max(c_agility_1, c_agility_2);
        minValue = Mathf.Min(c_agility_1, c_agility_2);
        SimulationStartData.Carnivore_AgilityMax = maxValue;
        SimulationStartData.Carnivore_AgilityMin = minValue;

        maxValue = Mathf.Max(c_strength_1, c_strength_2);
        minValue = Mathf.Min(c_strength_1, c_strength_2);
        SimulationStartData.Carnivore_StrengthMax = maxValue;
        SimulationStartData.Carnivore_StrengthMin = minValue;

        maxValue = Mathf.Max(c_sight_1, c_sight_2);
        minValue = Mathf.Min(c_sight_1, c_sight_2);
        SimulationStartData.Carnivore_SightMax = maxValue;
        SimulationStartData.Carnivore_SightMin = minValue;

        maxValue = Mathf.Max(c_size_1, c_size_2);
        minValue = Mathf.Min(c_size_1, c_size_2);
        SimulationStartData.Carnivore_SizeMax = maxValue;
        SimulationStartData.Carnivore_SizeMin = minValue;


        SimulationStartData.Organisms_Number = organisms;
        SimulationStartData.Organisms_Proportion = proportion;


        SceneManager.LoadScene(1);
    }

    public void LoadSimulation() {
        // Ładowanie sceny, do zrobienia

        SceneManager.LoadScene(1);
    }



    public void UpdateHerbivoreAgilityValue1(float value) {
        h_agility_1 = value;
        Herbivore_Agility_Value_1.text = value.ToString("F2");
    }

    public void UpdateHerbivoreAgilityValue2(float value) {
        h_agility_2 = value;
        Herbivore_Agility_Value_2.text = value.ToString("F2");
    }

    public void UpdateHerbivoreStrengthValue1(float value) {
        h_strength_1 = value;
        Herbivore_Strength_Value_1.text = value.ToString("F2");
    }

    public void UpdateHerbivoreStrengthValue2(float value) {
        h_strength_2 = value;
        Herbivore_Strength_Value_2.text = value.ToString("F2");
    }

    public void UpdateHerbivoreSightValue1(float value) {
        h_sight_1 = value;
        Herbivore_Sight_Value_1.text = value.ToString("F2");
    }

    public void UpdateHerbivoreSightValue2(float value) {
        h_sight_2 = value;
        Herbivore_Sight_Value_2.text = value.ToString("F2");
    }

    public void UpdateHerbivoreSizeValue1(float value) {
        h_size_1 = value;
        Herbivore_Size_Value_1.text = value.ToString("F2");
    }

    public void UpdateHerbivoreSizeValue2(float value) {
        h_size_2 = value;
        Herbivore_Size_Value_2.text = value.ToString("F2");
    }



    public void UpdateCarnivoreAgilityValue1(float value) {
        c_agility_1 = value;
        Carnivore_Agility_Value_1.text = value.ToString("F2");
    }

    public void UpdateCarnivoreAgilityValue2(float value) {
        c_agility_2 = value;
        Carnivore_Agility_Value_2.text = value.ToString("F2");
    }

    public void UpdateCarnivoreStrengthValue1(float value) {
        c_strength_1 = value;
        Carnivore_Strength_Value_1.text = value.ToString("F2");
    }

    public void UpdateCarnivoreStrengthValue2(float value) {
        c_strength_2 = value;
        Carnivore_Strength_Value_2.text = value.ToString("F2");
    }

    public void UpdateCarnivoreSightValue1(float value) {
        c_sight_1 = value;
        Carnivore_Sight_Value_1.text = value.ToString("F2");
    }

    public void UpdateCarnivoreSightValue2(float value) {
        c_sight_2 = value;
        Carnivore_Sight_Value_2.text = value.ToString("F2");
    }

    public void UpdateCarnivoreSizeValue1(float value) {
        c_size_1 = value;
        Carnivore_Size_Value_1.text = value.ToString("F2");
    }

    public void UpdateCarnivoreSizeValue2(float value) {
        c_size_2 = value;
        Carnivore_Size_Value_2.text = value.ToString("F2");
    }



    public void UpdateOrganismsNumberValue(float value) {
        organisms = (int)value;
        Organisms_Number_Value.text = ((int)(value)).ToString();
    }

    public void UpdateOrganismsProportionValue(float value) {
        proportion = value;
        int herbivores = (int)(proportion * 100);
        int carnivores = 100 - herbivores;
        Organisms_Proportion_Value.text = $"% {herbivores}|{carnivores} %";
    }

    // public void UpdateAgilityValue(bool herbivore, bool firstSlider, float value) {
    //     if (herbivore) {
    //         if (firstSlider) {
    //             Herbivore_Agility_Value_1.text = value.ToString("F2");
    //         } else {
    //             Herbivore_Agility_Value_2.text = value.ToString("F2");
    //         }
    //     } else {
    //         if (firstSlider) {
    //             Carnivore_Agility_Value_1.text = value.ToString("F2");
    //         } else {
    //             Carnivore_Agility_Value_2.text = value.ToString("F2");
    //         }
    //     }
    // }

    // public void UpdateStrengthValue(bool herbivore, bool firstSlider, float value) {
    //     if (herbivore) {
    //         if (firstSlider) {
    //             Herbivore_Strength_Value_1.text = value.ToString("F2");
    //         } else {
    //             Herbivore_Strength_Value_2.text = value.ToString("F2");
    //         }
    //     } else {
    //         if (firstSlider) {
    //             Carnivore_Strength_Value_1.text = value.ToString("F2");
    //         } else {
    //             Carnivore_Strength_Value_2.text = value.ToString("F2");
    //         }
    //     }
    // }

    // public void UpdateSightValue(bool herbivore, bool firstSlider, float value) {
    //     if (herbivore) {
    //         if (firstSlider) {
    //             Herbivore_Sight_Value_1.text = value.ToString("F2");
    //         } else {
    //             Herbivore_Sight_Value_2.text = value.ToString("F2");
    //         }
    //     } else {
    //         if (firstSlider) {
    //             Carnivore_Sight_Value_1.text = value.ToString("F2");
    //         } else {
    //             Carnivore_Sight_Value_2.text = value.ToString("F2");
    //         }
    //     }
    // }

    // public void UpdateSizeValue(bool herbivore, bool firstSlider, float value) {
    //     if (herbivore) {
    //         if (firstSlider) {
    //             Herbivore_Size_Value_1.text = value.ToString("F2");
    //         } else {
    //             Herbivore_Size_Value_2.text = value.ToString("F2");
    //         }
    //     } else {
    //         if (firstSlider) {
    //             Carnivore_Size_Value_1.text = value.ToString("F2");
    //         } else {
    //             Carnivore_Size_Value_2.text = value.ToString("F2");
    //         }
    //     }
    // }
}

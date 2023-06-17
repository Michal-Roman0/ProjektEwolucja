using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu_Controller : MonoBehaviour
{
    public GameObject HerbivoreStats_Panel;
    TextMeshProUGUI Herbivore_Agility_Value_1;
    TextMeshProUGUI Herbivore_Agility_Value_2;
    TextMeshProUGUI Herbivore_Strength_Value_1;
    TextMeshProUGUI Herbivore_Strength_Value_2;
    TextMeshProUGUI Herbivore_Sight_Value_1;
    TextMeshProUGUI Herbivore_Sight_Value_2;
    TextMeshProUGUI Herbivore_Size_Value_1;
    TextMeshProUGUI Herbivore_Size_Value_2;


    public GameObject CarnivoreStats_Panel;
    TextMeshProUGUI Carnivore_Agility_Value_1;
    TextMeshProUGUI Carnivore_Agility_Value_2;
    TextMeshProUGUI Carnivore_Strength_Value_1;
    TextMeshProUGUI Carnivore_Strength_Value_2;
    TextMeshProUGUI Carnivore_Sight_Value_1;
    TextMeshProUGUI Carnivore_Sight_Value_2;
    TextMeshProUGUI Carnivore_Size_Value_1;
    TextMeshProUGUI Carnivore_Size_Value_2;


    public GameObject Organisms_Panel;
    TextMeshProUGUI Organisms_Number_Value;
    TextMeshProUGUI Organisms_Proportion_Value;
    

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
    }



    public void NewSimulation() {
        float maxValue, minValue;

        maxValue = Mathf.Max(float.Parse(Herbivore_Agility_Value_1.text), float.Parse(Herbivore_Agility_Value_2.text));
        minValue = Mathf.Min(float.Parse(Herbivore_Agility_Value_1.text), float.Parse(Herbivore_Agility_Value_2.text));
        SimulationStartData.Herbivore_AgilityMax = maxValue;
        SimulationStartData.Herbivore_AgilityMin = minValue;

        maxValue = Mathf.Max(float.Parse(Herbivore_Strength_Value_1.text), float.Parse(Herbivore_Strength_Value_2.text));
        minValue = Mathf.Min(float.Parse(Herbivore_Strength_Value_1.text), float.Parse(Herbivore_Strength_Value_2.text));
        SimulationStartData.Herbivore_StrengthMax = maxValue;
        SimulationStartData.Herbivore_StrengthMin = minValue;

        maxValue = Mathf.Max(float.Parse(Herbivore_Sight_Value_1.text), float.Parse(Herbivore_Sight_Value_2.text));
        minValue = Mathf.Min(float.Parse(Herbivore_Sight_Value_1.text), float.Parse(Herbivore_Sight_Value_2.text));
        SimulationStartData.Herbivore_SightMax = maxValue;
        SimulationStartData.Herbivore_SightMin = minValue;

        maxValue = Mathf.Max(float.Parse(Herbivore_Size_Value_1.text), float.Parse(Herbivore_Size_Value_2.text));
        minValue = Mathf.Min(float.Parse(Herbivore_Size_Value_1.text), float.Parse(Herbivore_Size_Value_2.text));
        SimulationStartData.Herbivore_SizeMax = maxValue;
        SimulationStartData.Herbivore_SizeMin = minValue;


        maxValue = Mathf.Max(float.Parse(Carnivore_Agility_Value_1.text), float.Parse(Carnivore_Agility_Value_2.text));
        minValue = Mathf.Min(float.Parse(Carnivore_Agility_Value_1.text), float.Parse(Carnivore_Agility_Value_2.text));
        SimulationStartData.Carnivore_AgilityMax = maxValue;
        SimulationStartData.Carnivore_AgilityMin = minValue;

        maxValue = Mathf.Max(float.Parse(Carnivore_Strength_Value_1.text), float.Parse(Carnivore_Strength_Value_2.text));
        minValue = Mathf.Min(float.Parse(Carnivore_Strength_Value_1.text), float.Parse(Carnivore_Strength_Value_2.text));
        SimulationStartData.Carnivore_StrengthMax = maxValue;
        SimulationStartData.Carnivore_StrengthMin = minValue;

        maxValue = Mathf.Max(float.Parse(Carnivore_Sight_Value_1.text), float.Parse(Carnivore_Sight_Value_2.text));
        minValue = Mathf.Min(float.Parse(Carnivore_Sight_Value_1.text), float.Parse(Carnivore_Sight_Value_2.text));
        SimulationStartData.Carnivore_SightMax = maxValue;
        SimulationStartData.Carnivore_SightMin = minValue;

        maxValue = Mathf.Max(float.Parse(Carnivore_Size_Value_1.text), float.Parse(Carnivore_Size_Value_2.text));
        minValue = Mathf.Min(float.Parse(Carnivore_Size_Value_1.text), float.Parse(Carnivore_Size_Value_2.text));
        SimulationStartData.Carnivore_SizeMax = maxValue;
        SimulationStartData.Carnivore_SizeMin = minValue;


        SimulationStartData.Organisms_Number = int.Parse(Organisms_Number_Value.text);
        SimulationStartData.Organisms_Proportion = float.Parse(Organisms_Proportion_Value.text.Remove(Organisms_Proportion_Value.text.Length - 1, 1)) / 100;


        SceneManager.LoadScene(1); // Ustawiłem w build settings indeksy
    }

    public void LoadSimulation() {
        // Ładowanie sceny
    }



    public void UpdateHerbivoreAgilityValue1(float value) {
        Herbivore_Agility_Value_1.text = value.ToString("F2");
    }

    public void UpdateHerbivoreAgilityValue2(float value) {
        Herbivore_Agility_Value_2.text = value.ToString("F2");
    }

    public void UpdateHerbivoreStrengthValue1(float value) {
        Herbivore_Strength_Value_1.text = value.ToString("F2");
    }

    public void UpdateHerbivoreStrengthValue2(float value) {
        Herbivore_Strength_Value_2.text = value.ToString("F2");
    }

    public void UpdateHerbivoreSightValue1(float value) {
        Herbivore_Sight_Value_1.text = value.ToString("F2");
    }

    public void UpdateHerbivoreSightValue2(float value) {
        Herbivore_Sight_Value_2.text = value.ToString("F2");
    }

    public void UpdateHerbivoreSizeValue1(float value) {
        Herbivore_Size_Value_1.text = value.ToString("F2");
    }

    public void UpdateHerbivoreSizeValue2(float value) {
        Herbivore_Size_Value_2.text = value.ToString("F2");
    }



    public void UpdateCarnivoreAgilityValue1(float value) {
        Carnivore_Agility_Value_1.text = value.ToString("F2");
    }

    public void UpdateCarnivoreAgilityValue2(float value) {
        Carnivore_Agility_Value_2.text = value.ToString("F2");
    }

    public void UpdateCarnivoreStrengthValue1(float value) {
        Carnivore_Strength_Value_1.text = value.ToString("F2");
    }

    public void UpdateCarnivoreStrengthValue2(float value) {
        Carnivore_Strength_Value_2.text = value.ToString("F2");
    }

    public void UpdateCarnivoreSightValue1(float value) {
        Carnivore_Sight_Value_1.text = value.ToString("F2");
    }

    public void UpdateCarnivoreSightValue2(float value) {
        Carnivore_Sight_Value_2.text = value.ToString("F2");
    }

    public void UpdateCarnivoreSizeValue1(float value) {
        Carnivore_Size_Value_1.text = value.ToString("F2");
    }

    public void UpdateCarnivoreSizeValue2(float value) {
        Carnivore_Size_Value_2.text = value.ToString("F2");
    }



    public void UpdateOrganismsNumberValue(float value) {
        Organisms_Number_Value.text = ((int)(value)).ToString();
    }

    public void UpdateOrganismsProportionValue(float value) {
        Organisms_Proportion_Value.text = ((int)(value * 100)).ToString() + "%";
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

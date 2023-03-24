using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Organism_Movement))]
[RequireComponent(typeof(Organism_Properties))]

public class Organism_Controller : MonoBehaviour
{
    [SerializeField] GameObject panel;

    SpriteRenderer sprRend;

    Organism_Movement movement;
    Organism_Properties properties;

    // Start is called before the first frame update
    void Start()
    {
        sprRend = GetComponent<SpriteRenderer>();
        movement = GetComponent<Organism_Movement>();
        properties = GetComponent<Organism_Properties>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.Move();
    }

    void OnMouseDown()
    {
        panel.transform.Find("Image").GetComponent<Image>().sprite = sprRend.sprite;
        panel.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = name;
        panel.transform.Find("S").GetComponent<TextMeshProUGUI>().text = "Strength: " + properties.Strength;
        panel.transform.Find("P").GetComponent<TextMeshProUGUI>().text = "Perception: " + properties.Perception;
        panel.transform.Find("E").GetComponent<TextMeshProUGUI>().text = "Endurance: " + properties.Endurance;
        panel.transform.Find("C").GetComponent<TextMeshProUGUI>().text = "Charisma: " + properties.Charisma;
        panel.transform.Find("I").GetComponent<TextMeshProUGUI>().text = "Intelligence: " + properties.Intelligence;
        panel.transform.Find("A").GetComponent<TextMeshProUGUI>().text = "Agility: " + properties.Agility;
        panel.transform.Find("L").GetComponent<TextMeshProUGUI>().text = "Luck: " + properties.Luck;
    }
}

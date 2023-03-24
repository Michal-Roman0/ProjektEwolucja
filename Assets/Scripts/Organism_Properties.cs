using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Organism_Properties : MonoBehaviour
{
    [SerializeField] int strength;
    [SerializeField] int perception;
    [SerializeField] int endurance;
    [SerializeField] int charisma;
    [SerializeField] int intelligence;
    [SerializeField] int agility;
    [SerializeField] int luck;

    public int Strength { get { return strength; } }
    public int Perception { get { return perception; } }
    public int Endurance { get { return endurance; } }
    public int Charisma { get { return charisma; } }
    public int Intelligence { get { return intelligence; } }
    public int Agility { get { return agility; } }
    public int Luck { get { return luck; } }
}

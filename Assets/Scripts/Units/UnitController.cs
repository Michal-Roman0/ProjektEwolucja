using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class UnitController : MonoBehaviour
{
    public UnitBaseStats baseStats;

    public UnitDerivativeStats derivativeStats;

    [Header("Base stats")]
    [SerializeField]
    public float agility;
    [SerializeField]
    public float strength;
    [SerializeField]
    public float stealth;
    [SerializeField]
    public float sight;
    [SerializeField]
    public float sense;
    [SerializeField]
    public float size;
    [SerializeField]
    public float morality;
    [SerializeField]
    public bool eatsMeat;
    [SerializeField]
    public bool eatsPlants;

    [Header("Derivative stats")]
    [SerializeField]
    public float energy;
    [SerializeField]
    public float maxSpeed;
    [SerializeField]
    public float maxEnergy;
    [SerializeField]
    public float energyEfficiency;
    [SerializeField]
    public float range;
    [SerializeField]
    public float damage;
    [SerializeField]
    public int maxAge;

    [Header("Other")]
    public int age; //global tick adding + 1 to age for every unit?
    public bool readyToMate=true;
    public bool hungry=false;
    // Start is called before the first frame update
    void Start()
    {
        derivativeStats = ScriptableObject.CreateInstance<UnitDerivativeStats>();
        baseStats.PrintInfo();
        derivativeStats.PrintInfo();
        LoadBaseStats();
        LoadHealth();
        LoadDerivativeStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadBaseStats()
    {
        agility = baseStats.agility;
        strength = baseStats.strength;
        stealth = baseStats.stealth;
        sight = baseStats.sight;
        sense = baseStats.sense;
        size = baseStats.size;
        morality = baseStats.morality;
        eatsMeat = baseStats.eatsMeat;
        eatsPlants = baseStats.eatsPlants;
    }
    private void LoadHealth()
    {
        // formula wciaz do ustalenia
        int health = 10 + (int)Mathf.Round(size * strength);
        GetComponent<Health>().SetHealth(health, health);

        damage = strength * size;
    }
    private void LoadDerivativeStats()
    {
        derivativeStats.InitFromBase(baseStats);
        energy = derivativeStats.Energy;
        maxSpeed = derivativeStats.MaxSpeed;
        maxEnergy = derivativeStats.MaxEnergy;
        energyEfficiency = derivativeStats.EnergyEfficiency;
        range = derivativeStats.Range;
        damage = derivativeStats.Damage;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class UnitController : MonoBehaviour
{
    public UnitBaseStats baseStats;

    public UnitDerivativeStats derivativeStats;

    [Header("Base stats")]
    [SerializeField] private float agility;
    [SerializeField] private float strength;
    [SerializeField] private float stealth;
    [SerializeField] private float sight;
    [SerializeField] private float sense;
    [SerializeField] private float size;
    [SerializeField] private float morality;
    [SerializeField] private bool eatsMeat;
    [SerializeField] private bool eatsPlants;

    [Header("Derivative stats")]
    [SerializeField] private float energy;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float stamina;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float damage;
    [SerializeField] private float threat;
    [SerializeField] private int maxAge;

    [Header("Other")]
    private int age; //global tick adding + 1 to age for every unit?
    public bool readyToMate = true;
    public bool hungry;
    // Start is called before the first frame update
    void Start()
    {
        derivativeStats = ScriptableObject.CreateInstance<UnitDerivativeStats>();
        baseStats.PrintInfo();
        derivativeStats.PrintInfo();
        LoadBaseStats();
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
    private void LoadDerivativeStats()
    {
        derivativeStats.InitFromBase(baseStats);
        energy = derivativeStats.Energy;
        stamina = derivativeStats.Stamina;
        maxSpeed = derivativeStats.MaxSpeed;
        maxEnergy = derivativeStats.MaxEnergy;
        threat = derivativeStats.Threat;
        damage = derivativeStats.Damage;

        int health = derivativeStats.MaxHealth;
        GetComponent<Health>().SetHealth(health, health);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public UnitBaseStats baseStats;

    [Header("Base stats:")]
    [SerializeField]
    private float agility;
    [SerializeField]
    private float strength;
    [SerializeField]
    private float stealth;
    [SerializeField]
    private float sight;
    [SerializeField]
    private float sense;
    [SerializeField]
    private float size;
    [SerializeField]
    private float morality;
    [SerializeField]
    private bool eatsMeat;
    [SerializeField]
    private bool eatsPlants;

    [Header("Derivative stats")]
    [SerializeField]
    private float energy;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float maxEnergy;
    [SerializeField]
    private float energyEfficiency;
    [SerializeField]
    private float range;
    [SerializeField]
    private float damage;
    [SerializeField]
    private int maxAge;

    [Header("Other")]
    private int age; //global tick adding + 1 to age for every unit?

    // Start is called before the first frame update
    void Start()
    {
        baseStats.PrintInfo();
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
        int health = 10 + (int)Mathf.Floor(size * strength); //need better algorhitm
        GetComponent<Health>().SetHealth(health, health);

        damage = strength * size;
    }
}

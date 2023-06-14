using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class UnitController : MonoBehaviour
{
    public UnitBaseStats baseStats;

    public UnitDerivativeStats derivativeStats;
    public GameObject afterKillDrop;

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
    public float threat;
    [SerializeField]
    public float stamina;
    [SerializeField]
    public int maxAge;

    

    public int type;

    [Header("Other")]
    public int age; //global tick adding + 1 to age for every unit?
    public bool readyToMate=true;
    public bool hungry=false;
    public float hunger = 100;

    public float Hunger
    {
     get { return hunger; }
     set{
        if(value < 100)
        {
            hunger = value;
        }
        else
        {
            hunger = 100;
        }
        
        if (hunger < 60)
        {
            hungry = true;
        }
        //check if starving
        if(hunger <= 0)
        {
            KillSelf();
            // cleanup from lists of other objects required?
        }
     }
    }
    public float normalSpeed => maxSpeed / 2;
    // Start is called before the first frame update

    IEnumerator HungerTimer()
    {
        while(true){
            yield return new WaitForSeconds(2f);
            Hunger -= 1;
        }
    }
    void Start()
    {
        derivativeStats = ScriptableObject.CreateInstance<UnitDerivativeStats>();
        baseStats.PrintInfo();
        derivativeStats.PrintInfo();
        LoadBaseStats();
        LoadDerivativeStats();

        StartCoroutine(HungerTimer());
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

        gameObject.GetComponent<CircleCollider2D>().radius = range;

        int health = derivativeStats.MaxHealth;
        GetComponent<Health>().SetHealth(health, health);
    }
    public void KillSelf()
    {
        Instantiate(afterKillDrop, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

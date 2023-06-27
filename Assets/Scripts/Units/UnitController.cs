using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class UnitController : MonoBehaviour
{
    StateController sc;
    public UnitBaseStats baseStats;

    public UnitDerivativeStats derivativeStats;
    public GameObject afterKillDrop;
    public GameObject killEffect;
    public GameObject spawnEffect;
    public UniversalBar hungerBar;

    [Header("Base stats")]
    [SerializeField]
    public float agility;
    [SerializeField]
    public float strength;
    [SerializeField]
    public float sight;
    [SerializeField]
    public float size;
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
    public float damage;
    [SerializeField]
    public float threat;
    [SerializeField]
    public float stamina;
    [SerializeField]
    public float radius;
    [SerializeField]
    public int maxAge;


    public int type;

    [Header("Other")]
    public int age;
    public bool readyToMate = true;
    public bool hungry = false;
    public float hunger = 100;

    public float Hunger
    {
       get { return hunger; }
       set{
          if(value < maxEnergy*100)
          {
              hunger = value;
          }
          else
          {
              hunger = maxEnergy*100;
          }

          if (hunger < maxEnergy*60)
          {
              hungry = true;
          }
          else{
              hungry = false;
          }
          //check if starving
          if(hunger <= 0)
          {
              KillSelf();
                Debug.Log("Umrzylem ze glodu");
              // cleanup from lists of other objects required?
          }
          hungerBar.SetBarFill((int)hunger);
       }
    }
    public float normalSpeed => maxSpeed / 2;
    // Start is called before the first frame update

    IEnumerator HungerTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            Hunger -= 1;

            if (eatsPlants && sc.currentStateName == "Wandering") {
                Vector2Int pos = new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));
                Hunger += Mathf.FloorToInt(Tilemap_Controller.instance.GetMapTile(pos).GetValue(MapType.Vegetation) / 33);
            }
        }
    }
    IEnumerator AgeTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            age += 1;
        }
    }
    void Start()
    {
        if(derivativeStats != null)
        {
            return;
        }
        LoadBaseStats();
        SetupBasicInformation();
    }

    void SetupBasicInformation()
    {
        derivativeStats = ScriptableObject.CreateInstance<UnitDerivativeStats>();
        baseStats.PrintInfo();
        derivativeStats.PrintInfo();
        LoadDerivativeStats();
        AdjustSize();
        hungerBar.SetBarMaxFill((int)maxEnergy);
        age = 10;
        sc = GetComponent<StateController>();
        Instantiate(spawnEffect, gameObject.transform.position, Quaternion.identity);
        StartCoroutine(HungerTimer());
        StartCoroutine(AgeTimer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReloadStats(float agility, float strength, float sight, float size)
    {
        baseStats.agility = agility;
        baseStats.strength = strength;
        baseStats.sight = sight;
        baseStats.size = size;

        agility = baseStats.agility;
        strength = baseStats.strength;
        sight = baseStats.sight;
        size = baseStats.size;

        LoadDerivativeStats();
        AdjustSize();

        hungerBar.SetBarMaxFill((int)maxEnergy);
        age = 0;
    }

    private void LoadBaseStats()
    {
        if (eatsPlants) {
            baseStats.agility = UnityEngine.Random.Range(SimulationStartData.Herbivore_AgilityMin, SimulationStartData.Herbivore_AgilityMax);
            baseStats.strength = UnityEngine.Random.Range(SimulationStartData.Herbivore_StrengthMin, SimulationStartData.Herbivore_StrengthMax);
            baseStats.sight = UnityEngine.Random.Range(SimulationStartData.Herbivore_SightMin, SimulationStartData.Herbivore_SightMax);
            baseStats.size = UnityEngine.Random.Range(SimulationStartData.Herbivore_SizeMin, SimulationStartData.Herbivore_SizeMax);
        } else {
            baseStats.agility = UnityEngine.Random.Range(SimulationStartData.Carnivore_AgilityMin, SimulationStartData.Carnivore_AgilityMax);
            baseStats.strength = UnityEngine.Random.Range(SimulationStartData.Carnivore_StrengthMin, SimulationStartData.Carnivore_StrengthMax);
            baseStats.sight = UnityEngine.Random.Range(SimulationStartData.Carnivore_SightMin, SimulationStartData.Carnivore_SightMax);
            baseStats.size = UnityEngine.Random.Range(SimulationStartData.Carnivore_SizeMin, SimulationStartData.Carnivore_SizeMax);
        }

        agility = baseStats.agility;
        strength = baseStats.strength;
        sight = baseStats.sight;
        size = baseStats.size;
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
        radius = derivativeStats.Radius;
        gameObject.GetComponent<CircleCollider2D>().radius = radius;

        int health = derivativeStats.MaxHealth;
        GetComponent<Health>().SetHealth(health, health);
    }

    private void AdjustSize()
    {
        gameObject.transform.localScale = new(size+.5f, size+.5f);
    }

    public void KillSelf()
    {
        Instantiate(afterKillDrop, gameObject.transform.position, Quaternion.identity);
        Instantiate(killEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnMouseDown() { // to rework
        Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 objectPos = gameObject.transform.localPosition;

        if (Vector2.Distance(clickPos, objectPos) < 1)
            UI_Controller.instance.UpdateFocusedUnit(gameObject);
    }

    public void LoadStatsFromSave(SerializableUnit info)
    {
        agility = info.agility;
        strength = info.strength;
        size = info.size;
        sight = info.sight;
        SetupBasicInformation();
        Hunger = info.hunger;
    }
    public SerializableUnit GetUnitInfo()
    {
        SerializableUnit temp = new SerializableUnit();
        temp.agility = agility;
        temp.strength = strength;
        temp.size = size;
        temp.sight = sight;
        temp.hunger = Hunger;
        temp.presentHealth = GetComponent<Health>().HP;
        temp.location = gameObject.transform.position;
        return temp;
    }
}

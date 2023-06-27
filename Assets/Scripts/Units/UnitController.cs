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
    public int age; //global tick adding + 1 to age for every unit?
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
                Debug.Log("Umrzy³em ze g³odu");
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
    void Start()
    {
        if(derivativeStats != null)
        {
            return;
        }
        derivativeStats = ScriptableObject.CreateInstance<UnitDerivativeStats>();
        baseStats.PrintInfo();
        derivativeStats.PrintInfo();
        //LoadBaseStats();
        LoadStartStats();
        LoadDerivativeStats();
        AdjustSize();
        hungerBar.SetBarMaxFill((int)maxEnergy);
        sc = GetComponent<StateController>();

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
        sight = baseStats.sight;
        size = baseStats.size;
        eatsMeat = baseStats.eatsMeat;
        eatsPlants = baseStats.eatsPlants;
    }

    private void LoadStartStats()
    {
        if (eatsPlants) {
            agility = UnityEngine.Random.Range(SimulationStartData.Herbivore_AgilityMin, SimulationStartData.Herbivore_AgilityMax);
            strength = UnityEngine.Random.Range(SimulationStartData.Herbivore_StrengthMin, SimulationStartData.Herbivore_StrengthMax);
            sight = UnityEngine.Random.Range(SimulationStartData.Herbivore_SightMin, SimulationStartData.Herbivore_SightMax);
            size = UnityEngine.Random.Range(SimulationStartData.Herbivore_SizeMin, SimulationStartData.Herbivore_SizeMax);
        } else {
            agility = UnityEngine.Random.Range(SimulationStartData.Carnivore_AgilityMin, SimulationStartData.Carnivore_AgilityMax);
            strength = UnityEngine.Random.Range(SimulationStartData.Carnivore_StrengthMin, SimulationStartData.Carnivore_StrengthMax);
            sight = UnityEngine.Random.Range(SimulationStartData.Carnivore_SightMin, SimulationStartData.Carnivore_SightMax);
            size = UnityEngine.Random.Range(SimulationStartData.Carnivore_SizeMin, SimulationStartData.Carnivore_SizeMax);
        }
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
        gameObject.transform.localScale = new(size, size);
    }

    public void KillSelf()
    {
        Instantiate(afterKillDrop, gameObject.transform.position, Quaternion.identity);
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
        derivativeStats = ScriptableObject.CreateInstance<UnitDerivativeStats>();
        agility = info.agility;
        strength = info.strength;
        size = info.size;
        sight = info.sight;
        baseStats.PrintInfo();
        derivativeStats.PrintInfo();
        LoadDerivativeStats();
        AdjustSize();
        GetComponent<Health>().SetHealthFromSave(info.presentHealth);
        hungerBar.SetBarMaxFill((int)maxEnergy);
        Hunger = info.hunger;
        sc = GetComponent<StateController>();
        StartCoroutine(HungerTimer());
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

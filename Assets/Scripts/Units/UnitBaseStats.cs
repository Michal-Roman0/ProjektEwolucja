using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class UnitBaseStats : ScriptableObject
{
    public float agility;
    public float strength;
    public float sight; //rozbić na distance i szerokość widzenia?
    public float size;
    public bool eatsMeat;
    public bool eatsPlants;
    public Sprite sprite;
    public Dictionary<string, float> ToDictionary() {
        return new Dictionary<string, float>() {
            {"Agility", agility},
            {"Strength", strength},
            {"Sight", sight},
            {"Size", size},
        };
    }
    public float this[string key] =>
        this.ToDictionary()
            .FirstOrDefault(
                stat => stat.Key == key
            ).Value;
    public string Info =>
        string.Join(
            ",",
            new string[] {
                agility.ToString(),
                strength.ToString(),
                sight.ToString(),
                size.ToString(),
                eatsMeat.ToString(),
                eatsPlants.ToString(),
            }
        );

    public void PrintInfo()
    {
        Debug.Log(Info);
    }
}

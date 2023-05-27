using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UnitBaseStats : ScriptableObject
{
    public float agility;
    public float strength;
    public float stealth;
    public float sight; //rozbić na distance i szerokość widzenia?
    public float sense; //rozbić na radius i efficiency?
    public float size;
    public float morality;
    public bool eatsMeat;
    public bool eatsPlants;
    public int type;


    public Sprite sprite;

    public void PrintInfo(){
        //Debug.Log($"{agility} , {strength} , {stealth} , {stealth} , {sight} , {sense} , {size} , {morality} , {eatsMeat} , {eatsPlants}");
    }
}

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableUnit
{
    [SerializeField]
    public float agility;
    [SerializeField]
    public float strength;
    [SerializeField]
    public float sight;
    [SerializeField]
    public float size;
    [SerializeField]
    public Vector3 location;
    [SerializeField]
    public int presentHealth;
    [SerializeField]
    public float hunger;
}

public static class UnitExtensionMethods
{
    public static Object Instantiate(this Object thisObj, Object original, Vector3 position, Quaternion rotation, SerializableUnit info)
    {
        GameObject temp = Object.Instantiate(original, position, rotation) as GameObject;
        temp.GetComponent<UnitController>().LoadStatsFromSave(info);
        return temp;
    }
}

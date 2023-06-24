using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SimData
{
    [SerializeField]
    public float mapDiff;
    [SerializeField]
    public float mapTemp;
    [SerializeField]
    public float mapVege;
    [SerializeField]
    public List<Vector3> Herbivores;
    [SerializeField]
    public List<Vector3> Carnivores;
    [SerializeField]
    public List<Vector3> Fruits;
    [SerializeField] 
    public List<Vector3> Porkchops;
    public SimData()
    {
        Herbivores = new List<Vector3>();
        Carnivores = new List<Vector3>();
        Fruits = new List<Vector3>();
        Porkchops = new List<Vector3>();
    }
}

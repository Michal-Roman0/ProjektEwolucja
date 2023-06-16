using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BaseFoodStats : ScriptableObject
{
    public float nutrition;
    public int type;

    public Sprite sprite;

    public void PrintInfo()
    {
        Debug.Log($"{nutrition} , {type}");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableTile
{
    [SerializeField]
    public int difficulty;
    [SerializeField]
    public int temperature;
    [SerializeField]
    public int vegetation;

    [SerializeField]
    public float defaultColorR;
    [SerializeField]
    public float defaultColorG;
    [SerializeField]
    public float defaultColorB;

    [SerializeField]
    public float difficultyColorR;
    [SerializeField]
    public float difficultyColorG;
    [SerializeField]
    public float difficultyColorB;

    [SerializeField]
    public float temperatureColorR;
    [SerializeField]
    public float temperatureColorG;
    [SerializeField]
    public float temperatureColorB;

    [SerializeField]
    public float vegetationColorR;
    [SerializeField]
    public float vegetationColorG;
    [SerializeField]
    public float vegetationColorB;

    public SerializableTile(int diff, int temp, int vege, Color defaultColor, Color difficultyColor, Color temperatureColor, Color vegetationColor)
    {
        difficulty = diff;
        temperature = temp;
        vegetation = vege;

        defaultColorR = defaultColor.r;
        defaultColorG = defaultColor.g;
        defaultColorB = defaultColor.b;

        difficultyColorR = difficultyColor.r;
        difficultyColorG = difficultyColor.g;
        difficultyColorB = difficultyColor.b;

        temperatureColorR = temperatureColor.r;
        temperatureColorG = temperatureColor.g;
        temperatureColorB = temperatureColor.b;

        vegetationColorR = vegetationColor.r;
        vegetationColorG = vegetationColor.g;
        vegetationColorB = vegetationColor.b;
    }

    public Color DefaultColor
    {
        get
        {
            return new Color(defaultColorR, defaultColorG, defaultColorB);
        }
    }
    public Color DifficultyColor
    {
        get
        {
            return new Color(difficultyColorR, difficultyColorG, difficultyColorB);
        }
    }
    public Color TemperatureColor
    {
        get
        {
            return new Color(temperatureColorR, temperatureColorG, temperatureColorB);
        }
    }
    public Color VegetationColor
    {
        get
        {
            return new Color(vegetationColorR, vegetationColorG, vegetationColorB);
        }
    }
    public static implicit operator SerializableTile(MapTile tile)
    {
        return new SerializableTile(tile.GetValue(MapType.Difficulty), tile.GetValue(MapType.Temperature), tile.GetValue(MapType.Vegetation), 
            tile.GetColor(MapType.Default), tile.GetColor(MapType.Difficulty), tile.GetColor(MapType.Temperature), tile.GetColor(MapType.Vegetation));
    }
    public static implicit operator MapTile(SerializableTile tile)
    {
        return new MapTile(tile.difficulty, tile.temperature, tile.vegetation, tile.DefaultColor, tile.DifficultyColor, tile.TemperatureColor, tile.VegetationColor);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile
{
    // public GameObject plant;
    // [SerializeField] float cooldownBeforePlantSpawn;
    // float timeLeftBeforePlantSpawn;

    public int difficulty;
    public int temperature;
    public int vegetation;

    public float a = 0.8f;

    public Color defaultColor, difficultyColor, temperatureColor, vegetationColor;

    public MapTile(int difficulty, int temperature, int vegetation) {
        this.difficulty = difficulty;
        this.temperature = temperature;
        this.vegetation = vegetation;

        defaultColor     = new Color(1, 1, 1, a);
        difficultyColor  = new Color(1, 1 - difficulty / 100.0f, 0, a);
        temperatureColor = temperature < 50
            ? new Color(0, temperature / 50.0f, 1 - temperature / 50.0f, a)
            : new Color((temperature-50) / 50.0f, 1 - (temperature-50) / 50.0f, 0, a);
        vegetationColor  = new Color(1 - vegetation / 100.0f, vegetation / 100.0f, 0, a);
    }

    public int GetValue(MapType mapType) {
        if (mapType == MapType.Default)     return 0;
        if (mapType == MapType.Difficulty)  return difficulty;
        if (mapType == MapType.Temperature) return temperature;
        if (mapType == MapType.Vegetation)  return vegetation;

        return -1;
    }

    public Color GetColor(MapType mapType) {
        if (mapType == MapType.Default)     return defaultColor;
        if (mapType == MapType.Difficulty)  return difficultyColor;
        if (mapType == MapType.Temperature) return temperatureColor;
        if (mapType == MapType.Vegetation)  return vegetationColor;

        return new Color(0, 0, 0, a);
    }

    public void SetValue(MapType mapType, int value) {
        if (mapType == MapType.Difficulty) {
            difficulty = value;
            difficultyColor = new Color(1, 1 - difficulty / 100.0f, 0, a);
        }
        if (mapType == MapType.Temperature) {
            temperature = value;
            temperatureColor = temperature < 50
                ? new Color(0, temperature / 50.0f, 1 - temperature / 50.0f, a)
                : new Color((temperature-50) / 50.0f, 1 - (temperature-50) / 50.0f, 0, a);
        }
        if (mapType == MapType.Vegetation) {
            vegetation = value;
            vegetationColor = new Color(1 - vegetation / 100.0f, vegetation / 100.0f, 0, a);
        }
    }

    public void AddValue(MapType mapType, int value) {
        if (mapType == MapType.Difficulty) {
            difficulty += value;
            if (difficulty < 0) difficulty = 0;
            else if (difficulty > 100) difficulty = 100;
            difficultyColor = new Color(1, 1 - difficulty / 100.0f, 0, a);
        }
        if (mapType == MapType.Temperature) {
            temperature += value;
            if (temperature < 0) temperature = 0;
            else if (temperature > 100) temperature = 100;
            temperatureColor = temperature < 50
                ? new Color(0, temperature / 50.0f, 1 - temperature / 50.0f, a)
                : new Color((temperature-50) / 50.0f, 1 - (temperature-50) / 50.0f, 0, a);
        }
        if (mapType == MapType.Vegetation) {
            vegetation += value;
            if (vegetation < 0) vegetation = 0;
            else if (vegetation > 100) vegetation = 100;
            vegetationColor = new Color(1 - vegetation / 100.0f, vegetation / 100.0f, 0, a);
        }
    }
}

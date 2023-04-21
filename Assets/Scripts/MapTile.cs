using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile
{
    // public GameObject plant;
    // [SerializeField] float cooldownBeforePlantSpawn;
    // float timeLeftBeforePlantSpawn;

    int difficulty;
    int temperature;
    int vegetation;

    float a = 1;

    Color defaultColor, difficultyColor, temperatureColor, vegetationColor;

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

    // void Update() {
    //     timeLeftBeforePlantSpawn -= Time.deltaTime;

    //     if (timeLeftBeforePlantSpawn <= 0) {
    //         if (Random.Range(0f, mapData.MaxParameterValue) <= vegetation - (mapData.MaxParameterValue / 10)) {
    //             Instantiate(
    //                 plant,
    //                 transform.position + new Vector3(
    //                     Random.Range(-0.5f, 0.5f),
    //                     Random.Range(-0.5f, 0.5f),
    //                     0
    //                 ),
    //                 Quaternion.identity
    //             );
    //         }

    //         timeLeftBeforePlantSpawn = cooldownBeforePlantSpawn;
    //     }
    // }

    // public void SetupMapTile(int difficulty, int temperature, int vegetation) {
    //     //spriteRenderer = GetComponent<SpriteRenderer>();
    //     timeLeftBeforePlantSpawn = 10;

    //     this.difficulty  = difficulty;
    //     this.temperature = temperature;
    //     this.vegetation  = vegetation;

    //     UpdateTileColors();
    // }

    // void OnMouseDown() {
    //     if (mapData.IsPointerOverUI) return;

    //     if (mapData.BrushActive) {
    //         ChangeValue();
    //     }
    //     else if (mapData.BucketActive) {
    //         int valueToFill;

    //         switch (mapData.ActiveMap) {
    //             case MapType.Difficulty:  valueToFill = difficulty;  break;
    //             case MapType.Temperature: valueToFill = temperature; break;
    //             case MapType.Vegetation:  valueToFill = vegetation;  break;
    //             default: return;
    //         }
    //         mapData.Fill((int)transform.position.x, (int)transform.position.y, valueToFill);
    //     }
    // } // Napisac do tego testy

    // public int GetValue() {
    //     switch (mapData.ActiveMap) {
    //         case MapType.Default: break;
    //         case MapType.Difficulty:  return difficulty;
    //         case MapType.Temperature: return temperature;
    //         case MapType.Vegetation:  return vegetation;
    //         default: break;
    //     }
    //     return -1;
    // }

    // public void ChangeValue() {
    //     switch (mapData.ActiveMap) {
    //         case MapType.Default:
    //             break;
    //         case MapType.Difficulty: 
    //             difficulty  = mapData.BrushValue;
    //             difficultyColor  = difficultyColors [(int)((difficulty -1) / 20)];
    //             spriteRenderer.color = difficultyColor;
    //             break;
    //         case MapType.Temperature:
    //             temperature = mapData.BrushValue;
    //             temperatureColor = temperatureColors[(int)((temperature-1) / 20)];
    //             spriteRenderer.color = temperatureColor; break;
    //         case MapType.Vegetation:
    //             vegetation  = mapData.BrushValue;
    //             vegetationColor  = vegetationColors [(int)((vegetation -1) / 20)];
    //             spriteRenderer.color = vegetationColor;  break;
    //         default: break;
    //     }

    //     UpdateTileColors();
    //     ChangeDisplayedColor();
    // }

    // void UpdateTileColors() {
    //     difficultyColor  = difficultyColors [(int)((difficulty -1) / 20)];
    //     temperatureColor = temperatureColors[(int)((temperature-1) / 20)];
    //     vegetationColor  = vegetationColors [(int)((vegetation -1) / 20)];
    // }

    // public void ChangeDisplayedColor() {
    //     switch (mapData.ActiveMap) {
    //         case MapType.Default:     spriteRenderer.color = defaultColor;     break;
    //         case MapType.Difficulty:  spriteRenderer.color = difficultyColor;  break;
    //         case MapType.Temperature: spriteRenderer.color = temperatureColor; break;
    //         case MapType.Vegetation:  spriteRenderer.color = vegetationColor;  break;
    //     }
    // }
}

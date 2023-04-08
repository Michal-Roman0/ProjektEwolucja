using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class MapTile : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public MapData mapData;

    public GameObject plant;
    [SerializeField] float cooldownBeforePlantSpawn;
    float timeLeftBeforePlantSpawn;

    float difficulty;
    float temperature;
    float vegetation;
    public float Difficulty => difficulty;
    public float Temperature => temperature;
    public float Vegetation => vegetation;

    int colors = 5;

    Color defaultColor = new Color(1, 1, 1, 0.5f);

    Color[] difficultyColors = {
        new Color(1, 0, 0, 0.5f),
        new Color(1, 0.25f, 0, 0.5f),
        new Color(1, 0.50f, 0, 0.5f),
        new Color(1, 0.75f, 0, 0.5f),
        new Color(1, 1, 0, 0.5f)
    };

    Color[] temperatureColors = {
        new Color(0, 0, 1, 0.5f),
        new Color(0, 0.5f, 0.5f, 0.5f),
        new Color(0, 1, 0, 0.5f),
        new Color(0.5f, 0.5f, 0, 0.5f),
        new Color(1, 0, 0, 0.5f)
    };

    Color[] vegetationColors = {
        new Color(1, 0, 0, 0.5f),
        new Color(0.75f, 0.25f, 0, 0.5f),
        new Color(0.50f, 0.50f, 0, 0.5f),
        new Color(0.25f, 0.75f, 0, 0.5f),
        new Color(0, 1, 0, 0.5f)
    };

    Color difficultyColor, temperatureColor, vegetationColor;

    void Update() {
        timeLeftBeforePlantSpawn -= Time.deltaTime;

        if (timeLeftBeforePlantSpawn <= 0) {
            if (Random.Range(0f, mapData.MaxParameterValue) <= vegetation - (mapData.MaxParameterValue / 10)) {
                Instantiate(
                    plant,
                    transform.position + new Vector3(
                        Random.Range(-0.5f, 0.5f),
                        Random.Range(-0.5f, 0.5f),
                        0
                    ),
                    Quaternion.identity
                );
            }

            timeLeftBeforePlantSpawn = cooldownBeforePlantSpawn;
        }
    }

    public void SetupMapTile(float difficulty, float temperature, float vegetation) {
        spriteRenderer = GetComponent<SpriteRenderer>();
        timeLeftBeforePlantSpawn = 10;

        this.difficulty = difficulty;
        this.temperature = temperature;
        this.vegetation = vegetation;

        UpdateTileColors(mapData.MaxParameterValue);
    }

    void OnMouseDown() {
        mapData.ActiveTile = new Vector2((int)transform.position.x, (int)transform.position.y);
    }

    void UpdateTileColors(float maxParVal) {
        difficultyColor = difficulty == maxParVal
            ? difficultyColors[colors - 1]
            : difficultyColors[(int)(difficulty / maxParVal * colors)];
        
        temperatureColor = temperature == maxParVal
            ? temperatureColors[colors - 1]
            : temperatureColors[(int)(temperature / maxParVal * colors)];
        
        vegetationColor = vegetation == maxParVal
            ? vegetationColors[colors - 1]
            : vegetationColors[(int)(vegetation / maxParVal * colors)];
        
        UpdateDisplayedColor();
    }

    public void UpdateDisplayedColor() {
        switch (mapData.ActiveMap) {
            case MapType.Default: spriteRenderer.color = defaultColor; break;
            case MapType.Difficulty: spriteRenderer.color = difficultyColor; break;
            case MapType.Temperature: spriteRenderer.color = temperatureColor; break;
            case MapType.Vegetation: spriteRenderer.color = vegetationColor; break;
        }
    }

    public void ChangeValue(float value) {
        Debug.Log("Change");
        switch (mapData.ActiveMap) {
            case MapType.Default: break;
            case MapType.Difficulty: difficulty = value; break;
            case MapType.Temperature: temperature = value; break;
            case MapType.Vegetation: vegetation = value; break;
        }

        UpdateTileColors(mapData.MaxParameterValue);
    }
}

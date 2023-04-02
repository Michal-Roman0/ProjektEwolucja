using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class MapTile : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

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

    void Start() {
        timeLeftBeforePlantSpawn = 10;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        timeLeftBeforePlantSpawn -= Time.deltaTime;

        if (timeLeftBeforePlantSpawn <= 0) {
            if (Random.Range(Simulation.MinVegetation, Simulation.MaxVegetation) <= vegetation - 1) {
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
        this.difficulty = difficulty;
        this.temperature = temperature;
        this.vegetation = vegetation;

        float maxDiff = Simulation.MaxDifficulty;
        float maxTemp = Simulation.MaxTemperature;
        float maxVege = Simulation.MaxVegetation;

        difficultyColor = difficulty == maxDiff ? difficultyColors[colors - 1] :
            difficultyColors[(int)(difficulty / maxDiff * colors)];
        
        temperatureColor = temperature == maxTemp ? temperatureColors[colors - 1] :
            temperatureColors[(int)(temperature / maxTemp * colors)];
        
        vegetationColor = vegetation == maxVege ? vegetationColors[colors - 1] :
            vegetationColors[(int)(vegetation / maxVege * colors)];
    }

    public void ChangeTileToDefault() {
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
    }

    public void ChangeTileToDifficulty() {
        spriteRenderer.color = difficultyColor;
    }

    public void ChangeTileToTemperature() {
        spriteRenderer.color = temperatureColor;
    }

    public void ChangeTileToVegetation() {
        spriteRenderer.color = vegetationColor;
    }
}

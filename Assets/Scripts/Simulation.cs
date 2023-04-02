using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Simulation : MonoBehaviour
{
    public const float MinDifficulty = 0, MaxDifficulty = 10;
    public const float MinTemperature = 0, MaxTemperature = 10;
    public const float MinVegetation = 0, MaxVegetation = 10;

    [SerializeField] int mapWidth;
    [SerializeField] int mapHeight;
    public GameObject mapTile;
    GameObject[,] map;

    [SerializeField] Sprite PauseIcon;
    [SerializeField] Sprite PlayIcon;
    [SerializeField] Button PlayPauseButton;

    void Start()
    {
        Time.timeScale = 0;

        map = new GameObject[mapWidth, mapHeight];

        for (int x = 0; x < mapWidth; x++) {
            for (int y = 0; y < mapHeight; y++) {
                float difficulty = Random.Range(MinDifficulty, MaxDifficulty);
                float temperature = Random.Range(MinTemperature, MaxTemperature);
                float vegetation = Random.Range(MinVegetation, MaxVegetation);

                map[x, y] = Instantiate(mapTile, new Vector3(x, y, 0), Quaternion.identity);
                map[x, y].GetComponent<MapTile>().SetupMapTile(difficulty, temperature, vegetation);
            }
        }
    }

    public void PlayPauseSimulation()
    {
        if (Time.timeScale == 0) {
            Time.timeScale = 1;
            PlayPauseButton.image.sprite = PauseIcon;
        }
        else {
            Time.timeScale = 0;
            PlayPauseButton.image.sprite = PlayIcon;
        }
    }

    public void SetSimulationSpeed(int speed)
    {
        if (Time.timeScale == 0) return;
        
        Time.timeScale = speed;
    }

    public void ChangeMapToDefault() {
        for (int x = 0; x < mapWidth; x++) {
            for (int y = 0; y < mapHeight; y++) {
                map[x, y].GetComponent<MapTile>().ChangeTileToDefault();
            }
        }
    }

    public void ChangeMapToDifficulty() {
        for (int x = 0; x < mapWidth; x++) {
            for (int y = 0; y < mapHeight; y++) {
                map[x, y].GetComponent<MapTile>().ChangeTileToDifficulty();
            }
        }
    }
    
    public void ChangeMapToTemperature() {
        for (int x = 0; x < mapWidth; x++) {
            for (int y = 0; y < mapHeight; y++) {
                map[x, y].GetComponent<MapTile>().ChangeTileToTemperature();
            }
        }
    }

    public void ChangeMapToVegetation() {
        for (int x = 0; x < mapWidth; x++) {
            for (int y = 0; y < mapHeight; y++) {
                map[x, y].GetComponent<MapTile>().ChangeTileToVegetation();
            }
        }
    }
}

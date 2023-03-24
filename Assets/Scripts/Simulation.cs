using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Simulation : MonoBehaviour
{
    [SerializeField] Sprite Pause_Icon;
    [SerializeField] Sprite Play_Icon;
    [SerializeField] Button PlayPause_Button;

    void Start()
    {
        Time.timeScale = 0;
    }

    public void PlayPauseSimulation()
    {
        if (Time.timeScale == 0) {
            Time.timeScale = 1;
            PlayPause_Button.image.sprite = Pause_Icon;
        }
        else {
            Time.timeScale = 0;
            PlayPause_Button.image.sprite = Play_Icon;
        }
    }

    public void SetSimulationSpeed(int speed)
    {
        if (Time.timeScale == 0) {
            return;
        }

        Time.timeScale = speed;
    }
}

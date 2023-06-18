using UnityEngine;
using UnityEngine.UI; //needed for slider and image
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public TMP_Text healthText;

    public void SetHealth(int health)
    {
        slider.value = health;

        healthText.SetText("{0}", health);

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        healthText.SetText("{0}",health);

        fill.color = gradient.Evaluate(1f);
    }
}

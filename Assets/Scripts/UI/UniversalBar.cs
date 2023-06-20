using UnityEngine;
using UnityEngine.UI; //needed for slider and image
using TMPro;

public class UniversalBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public TMP_Text infoText;

    public void SetBarFill(int amount)
    {
        slider.value = amount;

        infoText.SetText("{0}", amount);

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetBarMaxFill(int amount)
    {
        slider.maxValue = amount;
        slider.value = amount;

        infoText.SetText("{0}",amount);

        fill.color = gradient.Evaluate(1f);
    }
}

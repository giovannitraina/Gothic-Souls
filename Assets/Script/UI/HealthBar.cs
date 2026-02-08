using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    // Start is called before the first frame update

    public void setMaxHealth(float max)
    {
        slider.maxValue = max;
        slider.value = max;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    public void setHealth(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}

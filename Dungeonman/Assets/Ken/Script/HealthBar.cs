using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;
    public void SetMaxhealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        this.slider.value += health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStatusBar : MonoBehaviour
{

    public Health health;
    public Image fillImage;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        if (slider.value <= 0)
        {
            fillImage.enabled = false;
        }//turns off slider if dead

        float fillValue = health.currentHealth /health.maxHealth;
        if (fillValue < slider.maxValue / 3)
        {
            fillImage.color = Color.red;
        }//changes colour if low on health
        slider.value = fillValue;
    }
}

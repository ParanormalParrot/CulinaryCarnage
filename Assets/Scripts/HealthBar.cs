using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    public void UpdateHealth(int maxHealth, int currentHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = currentHealth;
        text.text = (currentHealth > 0 ? currentHealth : 0)+ "/" + maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
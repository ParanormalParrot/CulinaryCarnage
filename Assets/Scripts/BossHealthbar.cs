using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Класс шкалы здоровья босса
public class BossHealthbar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI bossName;

    public void SetName(string name)
    {
        bossName.text = name;
    }
    public void SetHealth(int currentHealth)
    {
        slider.value = currentHealth;
    }
    
    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BossHealthbar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI bossName;
    
    // Start is called before the first frame update
    
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

    void Update()
    {
        
    }
}

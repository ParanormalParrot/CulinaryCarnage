using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    // Оружие, привязанное к слоту
    public Gun gun;

    // Объeкт блюда(оружия), содержащегося  в слоте
    public DishObject gunItem;

    // Статус слота
    public bool isActive;

    // Щкала перезарядки оружия
    public Slider slider;

    public void Update()
    {
        // Отображение перезарядки оружия
        if (ReferenceEquals(gun, null))
        {
            slider.maxValue = 1;
            slider.value = 0;
        }
        else
        {
            slider.maxValue = gun.startRechargeTime;
            slider.value = gun.curRechargeTime;
        }
    }
}
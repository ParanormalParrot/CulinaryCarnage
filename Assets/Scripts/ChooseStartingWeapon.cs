using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Класс выбора стартовог оружия
public class ChooseStartingWeapon : MonoBehaviour
{

    public Canvas canvas;

    public GroundItem weapon1;
    public GroundItem weapon2;
    public GroundItem weapon3;

    private bool weaponChosen = false;
    
    void Update()
    {
        if (!weaponChosen)
        {
            if (weapon1 == null)
            {
                Destroy(weapon2.GameObject());
                Destroy(weapon3.GameObject());
                Destroy(canvas.GameObject());
                weaponChosen = true;
            }
            if (weapon2 == null)
            {
                Destroy(weapon1.GameObject());
                Destroy(weapon3.GameObject());
                Destroy(canvas.GameObject());
                weaponChosen = true;
            
            }
            if (weapon3 == null)
            {
                Destroy(weapon1.GameObject());
                Destroy(weapon2.GameObject());
                Destroy(canvas.GameObject());
                weaponChosen = true;
            
            }
        }
        
       
    }
}

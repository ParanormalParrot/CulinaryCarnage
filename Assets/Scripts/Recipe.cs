using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "New Recipe",  menuName = "Cooking/Recipe") ]
public class Recipe : ScriptableObject
{
    public ItemObject product;
    public List<InventorySlot> ingredients;

    
}

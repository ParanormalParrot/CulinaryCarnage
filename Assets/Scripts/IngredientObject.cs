using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Default Object",  menuName = "Inventory System/Items/Ingredient") ]
public class IngredientObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Ingredient;
        isStackable = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Default Object",  menuName = "Inventory System/Items/Dishes") ]
public class DishObject : ItemObject
{
    public Gun gun;
    public int HPIncreased;
    public int HPRestored;
    public void Awake()
    {
        type = ItemType.Dish;
        isStackable = false;
    }
}

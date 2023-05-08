using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Ingredient,
    Dish, 
    Default
}
public abstract class ItemObject : ScriptableObject
{
    public bool isStackable;
    public Sprite sprite;
    public ItemType type;
    [TextArea(15, 20)] public string itemDescription;
}

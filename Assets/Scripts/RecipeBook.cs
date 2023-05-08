using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Recipe",  menuName = "Cooking/RecipeBook") ]
public class RecipeBook : ScriptableObject
{
    public List<Recipe> recipes;
    public Dictionary<ItemObject, List<InventorySlot>> dictionary = new Dictionary<ItemObject, List<InventorySlot>>();

    private void OnEnable()
    {
        foreach (var recipe in recipes)
        {
            dictionary.Add(recipe.product, recipe.ingredients);
        }
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Runtime;

public class FoodContainer : MonoBehaviour
{
    public List<GameObject> ingredients;
    public Transform spawnPoint;
    public SpriteRenderer spriteRenderer;
    public Sprite openSprite;
    private bool isOpen;


    void Open()
    {
        var rand = new System.Random();
        Instantiate(ingredients[rand.Next(0, ingredients.Count)], spawnPoint);
        spriteRenderer.sprite = openSprite;
        isOpen = true;

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!isOpen && col.CompareTag("Player"))
        {
            Open();
        }
    }
}

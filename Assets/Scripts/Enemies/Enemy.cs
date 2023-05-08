using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // Переменная, отслеживающая колиество врагов
    public static int numberOfEnemies;
    
    public int health;
    // Скорость движения врага 
    public float speed;

    public GameObject deathEffect;
    public Animator anim;
    // Игрок
    public Player player;

    private SpriteRenderer render;

    public float WaitTime = 1;

    public AIPath aiPath;
    public AIDestinationSetter aiDestinationSetter;


    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            numberOfEnemies--;
            Destroy(gameObject);
        }

        if (WaitTime > 0)
        {
            WaitTime -= Time.deltaTime;
        }
        else
        {
            if (player.transform.position.x > transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }

   
}
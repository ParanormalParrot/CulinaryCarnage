using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;



public class Cola : Enemy
{
    public int damage;
    // Start is called before the first frame update
    
    void Start()
    {
        numberOfEnemies++;
        player = FindObjectOfType<Player>();
        aiDestinationSetter.target = player.transform.GetChild(0);
    }
    
    void Update()
    {
        // Если здоровье достигает нуля, враг погибает
        if (health <= 0)
        {
            numberOfEnemies--;
            Destroy((gameObject));
        }
        if (WaitTime > 0)
        {
            WaitTime -= Time.deltaTime;
            aiPath.maxSpeed = 0;
        }
        else
        {
            aiPath.maxSpeed = speed;
        }
        
        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    
}

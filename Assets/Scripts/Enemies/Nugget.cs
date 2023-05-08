using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Nugget : Enemy
{
    
    // Контактный урон
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        numberOfEnemies++;
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        aiDestinationSetter.target = player.transform.GetChild(0);
        aiPath.maxSpeed = speed;
        WaitTime = Random.value + 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
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


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Враг наносит контактный урон игроку
            player.TakeDamage(damage);
        }
    }
}
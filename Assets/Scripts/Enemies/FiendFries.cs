using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class FiendFries : Enemy
{
    // Start is called before the first frame update
    public float shootingDistance = 1f;
    public GameObject bullet;
    public Transform shotPoint;
    public float startTimeBtwShots;
    public float curTimeBtwShots;
    

    void Start()
    {
        numberOfEnemies++;
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        aiDestinationSetter.target = player.transform.GetChild(0);
        WaitTime = Random.value + 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            numberOfEnemies--;
            Destroy(gameObject);
        }
        if (player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (WaitTime > 0)
        {
            WaitTime -= Time.deltaTime;
            aiPath.maxSpeed = 0;
        }
        else
        {
            float distance = Vector3.Distance(shotPoint.position, player.transform.position);
            if (distance < shootingDistance && CanSeePlayer())
            {
                aiPath.maxSpeed = 0;
                anim.SetBool("isShooting", true);
                if (curTimeBtwShots <= 0)
                {
                    Shoot();
                }

                curTimeBtwShots -= Time.deltaTime;
                
            }
            else
            {
                anim.SetBool("isShooting", false);
                aiPath.maxSpeed = speed;
            }
        }
    }

    void Shoot()
    {
        Vector2 difference = player.transform.position - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Quaternion rotation1 = Quaternion.Euler(0f, 0f, rotZ - 120);
        Quaternion rotation2 = Quaternion.Euler(0f, 0f, rotZ - 90);
        Quaternion rotation3 = Quaternion.Euler(0f, 0f, rotZ - 60);
        Instantiate(bullet, shotPoint.position, rotation1);
        Instantiate(bullet, shotPoint.position, rotation2);
        Instantiate(bullet, shotPoint.position, rotation3);
        curTimeBtwShots = startTimeBtwShots;
    }


    bool CanSeePlayer()
    {
        RaycastHit2D hit2D = Physics2D.Linecast(player.transform.position, transform.position,
            1 << LayerMask.NameToLayer("ProjectileObstacle"));
        return ReferenceEquals(hit2D.collider, null);
    }
}
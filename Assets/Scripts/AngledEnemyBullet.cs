using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngledEnemyBullet : EnemyBullet
{
    // Угол полёта снаряда
    public float angle;
    
    void Update()
    {
        Vector2 dir = (Vector2)(Quaternion.Euler(0,0,angle) * Vector2.right);
        transform.Translate(dir * speed * Time.deltaTime);
        if (lifetime >= 0)
        {
            lifetime -= Time.deltaTime;
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

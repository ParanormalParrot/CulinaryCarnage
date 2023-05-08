using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SausageBossBullet : EnemyBullet
{
    public AngledEnemyBullet sausage_slice;

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<Player>().TakeDamage(damage);
        }

        if (col.CompareTag("Obstacle"))
        {
            AngledEnemyBullet projectile1 = Instantiate(sausage_slice, transform.position, Quaternion.identity);
            AngledEnemyBullet projectile2 = Instantiate(sausage_slice, transform.position, Quaternion.identity);
            AngledEnemyBullet projectile3 = Instantiate(sausage_slice, transform.position, Quaternion.identity);
            AngledEnemyBullet projectile4 = Instantiate(sausage_slice, transform.position, Quaternion.identity);
            AngledEnemyBullet projectile5 = Instantiate(sausage_slice, transform.position, Quaternion.identity);
            AngledEnemyBullet projectile6 = Instantiate(sausage_slice, transform.position, Quaternion.identity);
            AngledEnemyBullet projectile7 = Instantiate(sausage_slice, transform.position, Quaternion.identity);
            AngledEnemyBullet projectile8 = Instantiate(sausage_slice, transform.position, Quaternion.identity);
            AngledEnemyBullet projectile9 = Instantiate(sausage_slice, transform.position, Quaternion.identity);
            AngledEnemyBullet projectile10 = Instantiate(sausage_slice, transform.position, Quaternion.identity);
            AngledEnemyBullet projectile11 = Instantiate(sausage_slice, transform.position, Quaternion.identity);
            AngledEnemyBullet projectile12 = Instantiate(sausage_slice, transform.position, Quaternion.identity);
            projectile1.angle = 0;
            projectile2.angle = 30;
            projectile3.angle = 60;
            projectile4.angle = 90;
            projectile5.angle = 120;
            projectile6.angle = 150;
            projectile7.angle = 180;
            projectile8.angle = -30;
            projectile9.angle = -60;
            projectile10.angle = -90;
            projectile11.angle = -120;
            projectile12.angle = -150;
            Destroy(gameObject);
        }
    }
}
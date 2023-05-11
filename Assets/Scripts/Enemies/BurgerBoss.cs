using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

public class BurgerBoss : Boss
{
    public float timeBetweenAttack;
    public GameObject minion;
    public float attackDuration;
    public float curAttackDuration;
    public float idleTime;
    public float curIdleTime;
    public bool isAttacking;
    public AngledEnemyBullet bullet;
    private int attackNumber;
    private int minionsCount;
    private bool secondPhase;

    public Transform shotPoint;
    
    void Start()
    {
        player = FindObjectOfType<Player>();
        aiDestinationSetter.target = player.transform.GetChild(0).transform;
        anim = GetComponent<Animator>();
        health = maxHealth;
        UserInterface.instance.bossHealthbar.GameObject().SetActive(true);
        UserInterface.instance.bossHealthbar.SetMaxHealth(maxHealth);
        UserInterface.instance.bossHealthbar.SetName("Meaty Monarch");
        numberOfEnemies++;
        name = "Meaty Monarch";
        secondPhase = false;
    }

    // Update is called once per frame
    void Update()
    {
        UserInterface.instance.bossHealthbar.SetHealth(health);
        if (minionsCount == 0 && health < maxHealth * 2 / 3)
        {
            minionsCount++;
            Instantiate(minion, transform.position + new Vector3(0, -3, 0), Quaternion.identity);
            idleTime = idleTime * 2 / 3;
        }

        if (health <= maxHealth / 2 && !secondPhase)
        {
            secondPhase = true;
            idleTime = idleTime / 2;
        }

        if (minionsCount == 1 && health < maxHealth * 1 / 3)
        {
            minionsCount++;
            Instantiate(minion, transform.position + new Vector3(0, -3, 0), Quaternion.identity);
            idleTime /= 2;
        }

        if (health <= 0)
        {
            UserInterface.instance.bossHealthbar.GameObject().SetActive(false);
            numberOfEnemies--;
            Destroy((gameObject));
        }

        if (!isAttacking)
        {
            aiPath.maxSpeed = 0;
            if (curIdleTime > 0)
            {
                anim.SetBool("isIdle", true);
                curIdleTime -= Time.deltaTime;
            }
            else
            {
                anim.SetBool("isIdle", false);
                isAttacking = true;
                curAttackDuration = attackDuration;
            }
        }

        if (isAttacking)
        {
            aiPath.maxSpeed = speed;
            curAttackDuration -= Time.deltaTime;
            if (attackNumber == 0 && curAttackDuration < attackDuration * 2 / 3)
            {
                Attack1();
                attackNumber++;
            }

            if (attackNumber == 1 && curAttackDuration < attackDuration * 1 / 3)
            {
                Attack2();
                attackNumber++;
            }

            if (attackNumber == 2 && curAttackDuration < 0)
            {
                Attack3();
                attackNumber = 0;
                isAttacking = false;
                curIdleTime = idleTime;
            }

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


    void Attack1()
    {
        AngledEnemyBullet bullet1 = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        AngledEnemyBullet bullet2 = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        AngledEnemyBullet bullet3 = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        AngledEnemyBullet bullet4 = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        bullet1.angle = 0;
        bullet2.angle = 90;
        bullet3.angle = 180;
        bullet4.angle = -90;
    }


    void Attack2()
    {
        AngledEnemyBullet bullet1 = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        AngledEnemyBullet bullet2 = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        AngledEnemyBullet bullet3 = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        AngledEnemyBullet bullet4 = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        bullet1.angle = 45;
        bullet2.angle = 135;
        bullet3.angle = -45;
        bullet4.angle = -135;
    }

    void Attack3()
    {
        AngledEnemyBullet bullet1 = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        AngledEnemyBullet bullet2 = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        AngledEnemyBullet bullet3 = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        AngledEnemyBullet bullet4 = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        AngledEnemyBullet bullet5 = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        AngledEnemyBullet bullet6 = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        AngledEnemyBullet bullet7 = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        AngledEnemyBullet bullet8 = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        bullet1.angle = 0;
        bullet2.angle = 90;
        bullet3.angle = 180;
        bullet4.angle = -90;
        bullet5.angle = 45;
        bullet6.angle = 135;
        bullet7.angle = -45;
        bullet8.angle = -135;
    }
}
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.UIElements;


public class Burger : Enemy
{
    // Время между прыжками
    public float timeBetweenJumps;

    // Текущее время между прыжками
    public float curTimeBetweenJumps;

    public bool isJumping;

    public int attackNumber;

    // Cнаряд
    public AngledEnemyBullet bullet;

    // Начальная позиция снарядов
    public Transform shotPoint;

    // Контактный урон 
    public int damage;

    // Продолжительность прыжка
    public float jumpTime;

    public float curJumpTime;

    // Start is called before the first frame update
    void Start()
    {
        numberOfEnemies++;
        player = FindObjectOfType<Player>();
        aiDestinationSetter.target = player.transform.GetChild(0).transform;
        anim = GetComponent<Animator>();
        isJumping = false;
        attackNumber = 0;
        WaitTime = Random.value + 0.5f;
    }


    void Update()
    {
        // Если здоровье достигает нуля, враг погибает
        if (health <= 0)
        {
            numberOfEnemies--;
            Destroy(gameObject);
        }

        if (WaitTime > 0)
        {
            WaitTime -= Time.deltaTime;
            aiPath.maxSpeed = 0;
        }
        else
        {
            // Начало прыжка
            if (curTimeBetweenJumps <= 0 && !isJumping)
            {
                anim.SetBool("isJumping", true);
                curJumpTime = jumpTime;
                isJumping = true;
            }

            // Прыжок
            if (curJumpTime > 0 && isJumping)
            {
                aiPath.maxSpeed = speed;
                isJumping = true;
                curJumpTime -= Time.deltaTime;
            }
            else
            {
                // Приземление
                if (isJumping)
                {
                    aiPath.maxSpeed = 0;
                    anim.SetBool("isJumping", false);
                    curTimeBetweenJumps = timeBetweenJumps;
                    isJumping = false;
                    // Burger Brawler выпускает 4 снаряда после приземления
                    AngledEnemyBullet bullet1 = Instantiate(bullet, shotPoint.position, Quaternion.identity);
                    AngledEnemyBullet bullet2 = Instantiate(bullet, shotPoint.position, Quaternion.identity);
                    AngledEnemyBullet bullet3 = Instantiate(bullet, shotPoint.position, Quaternion.identity);
                    AngledEnemyBullet bullet4 = Instantiate(bullet, shotPoint.position, Quaternion.identity);
                    // Направление снарядов меняется
                    if (attackNumber % 2 == 0)
                    {
                        bullet1.angle = 0;
                        bullet2.angle = 90;
                        bullet3.angle = 180;
                        bullet4.angle = -90;
                    }
                    else
                    {
                        bullet1.angle = 45;
                        bullet2.angle = 135;
                        bullet3.angle = -45;
                        bullet4.angle = -135;
                    }

                    attackNumber++;
                }

                curTimeBetweenJumps -= Time.deltaTime;
            }

            // Поворот персонажа
            if (player.transform.position.x > transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
    }


    IEnumerator Jump()
    {
        aiPath.maxSpeed = speed;
        isJumping = true;
        yield return new WaitForSeconds(jumpTime);
        isJumping = false;
        aiPath.maxSpeed = 0;
        anim.SetBool("isJumping", false);
        curTimeBetweenJumps = timeBetweenJumps;
        isJumping = false;
        // Burger Brawler выпускает 4 снаряда после приземления
        AngledEnemyBullet bullet1 = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        AngledEnemyBullet bullet2 = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        AngledEnemyBullet bullet3 = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        AngledEnemyBullet bullet4 = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        // Направление снарядов меняется
        if (attackNumber % 2 == 0)
        {
            bullet1.angle = 0;
            bullet2.angle = 90;
            bullet3.angle = 180;
            bullet4.angle = -90;
        }
        else
        {
            bullet1.angle = 45;
            bullet2.angle = 135;
            bullet3.angle = -45;
            bullet4.angle = -135;
        }
        attackNumber++;
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isJumping && other.CompareTag("Player"))
        {
            // Враг наносит контактный урон игроку
            player.TakeDamage(damage);
        }
    }
}
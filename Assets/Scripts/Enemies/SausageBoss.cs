using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SausageBoss : Boss
{
    public SausageBossGun gun;

    public Mine mine;

    public float TimeBetweenShots;
    public float CurTimeBetweenShots;
    private bool secondPhase;

    void Start()
    {
        numberOfEnemies++;
        player = FindObjectOfType<Player>();
        aiDestinationSetter.target = aiDestinationSetter.target = player.transform.GetChild(0);
        CurTimeBetweenShots = TimeBetweenShots;
        UserInterface.instance.bossHealthbar.GameObject().SetActive(true);
        UserInterface.instance.bossHealthbar.SetMaxHealth(maxHealth);
        UserInterface.instance.bossHealthbar.SetName("Sergeant Sausage");
        StartCoroutine(PlantMines());
        health = maxHealth;
        secondPhase = false;
    }

    void Update()
    {
        UserInterface.instance.bossHealthbar.SetHealth(health);
        if (player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (health <= maxHealth/2 && !secondPhase)
        {
            secondPhase = true;
            TimeBetweenShots /= 2;
        }

        if (CurTimeBetweenShots <= 0)
        {
            gun.Shoot();
            gun.anim.SetBool("isCharged", false);
            CurTimeBetweenShots = TimeBetweenShots;
        }


        CurTimeBetweenShots -= Time.deltaTime;
        if (health <= 0)
        {
            numberOfEnemies--;
            UserInterface.instance.bossHealthbar.GameObject().SetActive(false);
            Destroy(gameObject);
        }
    }

    IEnumerator PlantMines()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            Instantiate(mine, transform.position + new Vector3(Random.value * 3, Random.value * 3, 0),
                Quaternion.identity);
        }
    }
}
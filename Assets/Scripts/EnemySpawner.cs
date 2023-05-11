using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;


public class EnemySpawner : MonoBehaviour
{
    public Enemy[] enemies;
    public Enemy[] weakerEnemies;
    public Enemy[] strongerEnemies;

    public void SpawnEnemy()
    {
        var rand = new System.Random();
        float spawnStrongerEnemy = (1.5f * LevelGenerator.instance.currentFloorNumber + 2) / 10f;
        if (spawnStrongerEnemy > UnityEngine.Random.value)
        {
            Instantiate(strongerEnemies[rand.Next(strongerEnemies.Length)].gameObject,
                transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(weakerEnemies[rand.Next(weakerEnemies.Length)].gameObject,
                transform.position, Quaternion.identity);
        }
    }
}
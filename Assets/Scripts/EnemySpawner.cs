using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class EnemySpawner : MonoBehaviour
{
    public Enemy[] enemies;
    // Start is called before the first frame update

    public void SpawnEnemy()
    {
        var rand = new System.Random();
        GameObject enemy = Instantiate(enemies[rand.Next(enemies.Length)].gameObject, transform.position, Quaternion.identity);
    }
}

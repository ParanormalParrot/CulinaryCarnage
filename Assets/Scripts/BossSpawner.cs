using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public  void SpawnBoss()
    {
        var rand = new System.Random();
        int randomValue = rand.Next(0, rand.Next(LevelGenerator.instance.bosses.Count));
        GameObject boss = Instantiate(LevelGenerator.instance.bosses[randomValue].gameObject, transform.position, Quaternion.identity);
        LevelGenerator.instance.bosses.RemoveAt(randomValue);
    }
    
}

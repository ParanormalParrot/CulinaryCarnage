using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Mine : MonoBehaviour
{
    public float lifetime;
    public MustardExplosion explosion;

    // Update is called once per frame
    void Update()
    {
        if (Enemy.numberOfEnemies <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Мина взрывается, когда персонаж на неё наступает
        if (other.CompareTag("PlayerFeet"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
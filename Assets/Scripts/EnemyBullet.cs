using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Скорость снаряда
    public float speed;

    // Время существования снаряда
    public float lifetime;
    public float currTime;

    // Урон снаряда
    public int damage;
    
    // Start is called before the first frame update
    void Start()
    {
        currTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        if (lifetime >= 0)
        {
            lifetime -= Time.deltaTime;
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            other.collider.GetComponent<Player>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
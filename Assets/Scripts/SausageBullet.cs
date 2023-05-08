using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SausageBullet : PlayerBullet
{
    [FormerlySerializedAs("explosion")] public SausageExplosion sausageExplosion;

    public Transform point;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Obstacle"))
        {
            Instantiate(sausageExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            
            other.GetComponent<Enemy>().TakeDamage(damage);
            Instantiate(sausageExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaProjectile : PlayerBullet
{
    public Transform point;
    public Object slice;

    public float angle;
    // Start is called before the first frame update


    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy") || other.collider.CompareTag("Obstacle"))
        {
            if (other.collider.CompareTag("Enemy"))
            {
                other.collider.GetComponent<Enemy>().TakeDamage(damage);
            }

            Quaternion rotation1 = Quaternion.Euler(0f, 0f, 22.5f);
            Quaternion rotation2 = Quaternion.Euler(0f, 0f, 67.5f);
            Quaternion rotation3 = Quaternion.Euler(0f, 0f, 112.5f);
            Quaternion rotation4 = Quaternion.Euler(0f, 0f, 157.5f);
            Quaternion rotation5 = Quaternion.Euler(0f, 0f, -22.5f);
            Quaternion rotation6 = Quaternion.Euler(0f, 0f, -67.5f);
            Quaternion rotation7 = Quaternion.Euler(0f, 0f, -112.5f);
            Quaternion rotation8 = Quaternion.Euler(0f, 0f, -157.5f);
            Instantiate(slice, point.position, rotation1);
            Instantiate(slice, point.position, rotation2);
            Instantiate(slice, point.position, rotation3);
            Instantiate(slice, point.position, rotation4);
            Instantiate(slice, point.position, rotation5);
            Instantiate(slice, point.position, rotation6);
            Instantiate(slice, point.position, rotation7);
            Instantiate(slice, point.position, rotation8);
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Obstacle"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            Quaternion rotation1 = Quaternion.Euler(0f, 0f, 22.5f);
            Quaternion rotation2 = Quaternion.Euler(0f, 0f, 67.5f);
            Quaternion rotation3 = Quaternion.Euler(0f, 0f, 112.5f);
            Quaternion rotation4 = Quaternion.Euler(0f, 0f, 157.5f);
            Quaternion rotation5 = Quaternion.Euler(0f, 0f, -22.5f);
            Quaternion rotation6 = Quaternion.Euler(0f, 0f, -67.5f);
            Quaternion rotation7 = Quaternion.Euler(0f, 0f, -112.5f);
            Quaternion rotation8 = Quaternion.Euler(0f, 0f, -157.5f);
            Instantiate(slice, point.position, rotation1);
            Instantiate(slice, point.position, rotation2);
            Instantiate(slice, point.position, rotation3);
            Instantiate(slice, point.position, rotation4);
            Instantiate(slice, point.position, rotation5);
            Instantiate(slice, point.position, rotation6);
            Instantiate(slice, point.position, rotation7);
            Instantiate(slice, point.position, rotation8);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, angle) * Vector2.right);
        transform.Translate(dir * speed * Time.deltaTime);
        if (lifetime >= 0)
        {
            lifetime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CroissantProjectile : PlayerBullet
{
    private Player _player;
    public bool isReturning;

    // Start is called before the first frame update
    void Start()
    {
        currTime = lifetime;
        _player = FindObjectOfType<Player>();
        isReturning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReturning)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else
        {
            // Снаряд возвращается к игроку после столкновения с препятствием
            transform.position =
                Vector2.MoveTowards(transform.position, _player.transform.position, speed * Time.deltaTime);
            // Снаряд уничтожается после того, как достигнет игрока
            if (transform.position == _player.transform.position)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Снаряд наносит урон врагу при контакте
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }

        // Снаряд возвращается к игроку после столкновения с препятствием
        if (other.CompareTag("Obstacle"))
        {
            isReturning = true;
        }
    }
}
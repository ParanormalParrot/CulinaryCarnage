using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MustardExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifetime;

    public int damage;

    public AudioClip explosionSound;


    private void Start()
    {
        SoundManager.instance.PlaySound(explosionSound);
    }

    // Update is called once per frame
    void Update()
    {
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }

        lifetime -= Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
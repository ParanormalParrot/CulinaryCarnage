using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class SausageExplosion : MonoBehaviour
{
    public float lifetime;

    public int damage;
    
    public AudioClip explosionSound;

    public string targetTag;

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
      
        if (other.CompareTag(targetTag))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
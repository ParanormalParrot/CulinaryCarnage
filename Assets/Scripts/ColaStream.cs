using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using UnityEngine;

public class ColaStream : MonoBehaviour
{
    public Player player;
    public int damage;
    public SpriteRenderer renderer;
    public float rotZ;
    public float r;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        renderer = GetComponent<SpriteRenderer>();
        Vector2 difference = player.transform.position - transform.position;
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; 
        Quaternion rotation = Quaternion.Euler(0f, 0f, rotZ + 90 + 180);
        transform.rotation = rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 difference = player.transform.position - transform.position;
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; 
        Quaternion rotation = Quaternion.Euler(0f, 0f, rotZ + 90);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * 25f);
        Vector3 localScale = Vector3.one;
        if ((rotZ < -90 || rotZ > 90))
        {
            localScale.x = -1f;
        }
        else
        {
            localScale.x = +1f;
        }

        transform.localScale = localScale;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.TakeDamage(damage);
        }
    }
    
}

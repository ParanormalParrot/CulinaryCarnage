using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SausageBossGun : MonoBehaviour
{
    public Player player;
    public SausageBossBullet bullet;

    public Transform shotPoint;
    public Animator anim;
    public float rotationZ;
    public bool shotDelayed;
    
    void Start()
    {
        player = FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
        shotDelayed = false;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = player.transform.position - transform.position;
        rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, rotationZ);
        Vector3 localScale = Vector3.one;
        if ((rotationZ < -90 || rotationZ > 90))
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = +1f;
        }

        transform.localScale = localScale;
    }


    public void Shoot()
    {
        
        Quaternion rotation = Quaternion.Euler(0f, 0f, rotationZ - 90);
        Instantiate(bullet, shotPoint.position, rotation);
    }
}
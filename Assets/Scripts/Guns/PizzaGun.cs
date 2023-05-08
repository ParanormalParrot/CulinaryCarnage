using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Vector2 = System.Numerics.Vector2;

public class PizzaGun : Gun
{
    // Start is called before the first frame update
    public PizzaProjectile projectile;

    // Update is called once per frame

    void Update()
    {
        if (isActive)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
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


            if (curRechargeTime <= 0)
            {
                anim.SetBool("isCharged", true);
            }

            if (!UserInterface.instance.inventoryOpen)
            {
                if (Input.GetMouseButton(0))
                {
                    if (curRechargeTime <= 0)
                    {
                        SoundManager.instance.PlaySound(shootingSound);
                        projectile.angle = rotationZ;
                        Instantiate(projectile, shotPoint.position, Quaternion.identity);
                        curRechargeTime = startRechargeTime;
                        anim.SetBool("isCharged", false);
                    }
                }
            }
        }


        curRechargeTime -= Time.deltaTime;
    }
}
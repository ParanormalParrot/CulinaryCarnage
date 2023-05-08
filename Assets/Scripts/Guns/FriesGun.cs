using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class FriesGun : Gun
{
    void Start()
    {
        isActive = true;
        
    }

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

            if (!UserInterface.instance.inventoryOpen)
            {
                if (Input.GetMouseButton(0))
                {
                    if (curRechargeTime <= 0)
                    {
                        SoundManager.instance.PlaySound(shootingSound);
                        Quaternion rotation1 = Quaternion.Euler(0f, 0f, rotationZ - 90);
                        Quaternion rotation2 = Quaternion.Euler(0f, 0f, rotationZ - 60);
                        Quaternion rotation3 = Quaternion.Euler(0f, 0f, rotationZ - 120);
                        Instantiate(bullet, shotPoint.position, rotation1);
                        Instantiate(bullet, shotPoint.position, rotation2);
                        Instantiate(bullet, shotPoint.position, rotation3);
                        curRechargeTime = startRechargeTime;

                    }
                }
            }

        }
        
        curRechargeTime -= Time.deltaTime;
    }
}


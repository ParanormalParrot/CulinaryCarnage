using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaconBow : Gun
{
    public bool isPulled;
    public int minDamage;
    public int maxDamage;
    public float curDamage;

    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        anim = GetComponent<Animator>();
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


            if (curRechargeTime <= 0)
            {
                if (!UserInterface.instance.inventoryOpen && Input.GetMouseButton(0))
                {
                    if (!isPulled)
                    {
                        isPulled = true;
                        anim.SetInteger("stateNumber", 2);
                        curDamage = minDamage;
                        bullet.piercing = false;
                    }
                    else
                    {
                        if (curDamage <= maxDamage)
                        {
                            curDamage += Time.deltaTime * 10;
                        }
                        else
                        {
                            bullet.piercing = true;
                        }
                    }
                }
                else
                {
                    anim.SetInteger("stateNumber", 1);
                }
            }
            else
            {
                anim.SetInteger("stateNumber", 0);
            }

            if (isPulled)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    SoundManager.instance.PlaySound(shootingSound);
                    isPulled = false;
                    Quaternion rotation1 = Quaternion.Euler(0f, 0f, rotationZ - 90);
                    bullet.damage = (int)MathF.Round(curDamage);

                    Instantiate(bullet.GameObject(), shotPoint.position, rotation1);
                    curRechargeTime = startRechargeTime;
                }
            }

            transform.localScale = localScale;
        }


        curRechargeTime -= Time.deltaTime;
    }
}
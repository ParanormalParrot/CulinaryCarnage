using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelmeniGun : Gun
{
    // Start is called before the first frame update
  

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

            if (!UserInterface.instance.inventoryOpen)
            {
                if (Input.GetMouseButton(0))
                {
                    if (curTimeBetweenCharges <= 0)
                    {
                        if (curNumberOfCharges > 0)
                        {
                            SoundManager.instance.PlaySound(shootingSound);
                            Quaternion rotation1 = Quaternion.Euler(0f, 0f, rotationZ - 90);
                            Instantiate(bullet, shotPoint.position, rotation1);
                            curNumberOfCharges--;
                            curTimeBetweenCharges = timeBetweenCharges;
                        }
                    }
                }
            }

            transform.localScale = localScale;
        }


        if (curNumberOfCharges < maxNumberOfCharges)
        {
            if (curRechargeTime <= 0)
            {
                if (curRechargeTime < startRechargeTime)
                {
                    curNumberOfCharges++;
                }

                curRechargeTime = startRechargeTime;
            }
        }

        curRechargeTime -= Time.deltaTime;
        curTimeBetweenCharges -= Time.deltaTime;
    }
}
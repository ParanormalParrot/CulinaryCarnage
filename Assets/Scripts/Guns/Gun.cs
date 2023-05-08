using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Gun : MonoBehaviour
{
    // Объект снаряда
    public PlayerBullet bullet;

    // Начальная позиция снаряда
    public Transform shotPoint;
    public float startRechargeTime;
    public float curRechargeTime;
    public float rotationZ;
    public float offset;
    protected SpriteRenderer renderer;
    public Animator anim;
    public int maxNumberOfCharges;
    public int curNumberOfCharges;
    public float timeBetweenCharges;
    public float curTimeBetweenCharges;
    public bool isActive;

    public Vector3 EquipPosition;

    public AudioClip shootingSound;

    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        renderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    public void Activate()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        isActive = true;
    }

    public void Deactivate()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        isActive = false;
    }
}
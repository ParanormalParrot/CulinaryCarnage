using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    // Максимальное здоровье 
    public int maxHealth;
    // Текущее здоровье 
    private int currentHealth;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private Animator anim;
    public SpriteRenderer renderer;
    public HealthBar healthBar;
    public bool isWeaponEquipped;
    public InventoryObject inventory;
    
    public WeaponSlot slot1;
    public WeaponSlot slot2;
    public WeaponSlot slot3;
    public List<WeaponSlot> weaponSlots;
    public bool isInvincible;
    public float InvincibilityTime;
    public bool isDead;
    public AudioClip hurtSound;
    public AudioClip eatingSound;

    // Start is called before the first frame update
    void Start()
    {
        inventory.Clear();
        currentHealth = maxHealth;
        healthBar.UpdateHealth(maxHealth, currentHealth);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        slot1.transform.GetComponent<Image>().color = Color.yellow;
        slot2.transform.GetComponent<Image>().color = new Color(0.7686275f, 0.7686275f, 0.7686275f);
        slot3.transform.GetComponent<Image>().color = new Color(0.7686275f, 0.7686275f, 0.7686275f);
        slot1.isActive = true;
        slot2.isActive = false;
        slot3.isActive = false;
        weaponSlots.Add(slot1);
        weaponSlots.Add(slot2);
        weaponSlots.Add(slot3);
        foreach (var slot in weaponSlots)
        {
            if (!ReferenceEquals(slot.gun, null))
            {
                slot.gun.Activate();
            }
        }
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            anim.SetBool("isDead", true);
            if (!isDead)
            {
                inventory.Clear();
                UserInterface.instance.cookingMenu.UpdateMenu();
                foreach (var slot in weaponSlots)
                {
                    slot.gunItem = null;
                }
                isDead = true;
            }
            moveVelocity = Vector2.zero;
            foreach (var slot in weaponSlots)
            {
                if (!ReferenceEquals(slot.gun, null) )
                {
                    slot.gun.Deactivate();
                }
            }
            UserInterface.instance.defeatMenu.SetActive(true);

        }
        else
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveVelocity = moveInput.normalized * speed;

            Vector3 localScale = Vector3.one;
            if ((rotationZ < -90 || rotationZ > 90))
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

            transform.localScale = localScale;
            if (moveInput.x == 0 && moveInput.y == 0)
            {
                anim.SetBool("isRunning", false);
            }
            else
            {
                anim.SetBool("isRunning", true);
            }
            
            if (Input.GetKeyDown("1"))
            {
                slot1.transform.GetComponent<Image>().color = Color.yellow;
                slot2.transform.GetComponent<Image>().color = new Color(0.7686275f, 0.7686275f, 0.7686275f);
                slot3.transform.GetComponent<Image>().color = new Color(0.7686275f, 0.7686275f, 0.7686275f);

                slot1.isActive = true;
                slot2.isActive = false;
                slot3.isActive = false;

                if (!ReferenceEquals(slot1.gun, null))
                {
                    slot1.gun.Activate();
                }

                if (!ReferenceEquals(slot2.gun, null))
                {
                    slot2.gun.Deactivate();
                }

                if (!ReferenceEquals(slot3.gun, null))
                {
                    slot3.gun.Deactivate();
                }
            }

            if (Input.GetKeyDown("2"))
            {
                slot1.transform.GetComponent<Image>().color = new Color(0.7686275f, 0.7686275f, 0.7686275f);
                slot2.transform.GetComponent<Image>().color = Color.yellow;
                slot3.transform.GetComponent<Image>().color = new Color(0.7686275f, 0.7686275f, 0.7686275f);

                slot1.isActive = false;
                slot2.isActive = true;
                slot3.isActive = false;
                if (!ReferenceEquals(slot1.gun, null))
                {
                    slot1.gun.Deactivate();
                }

                if (!ReferenceEquals(slot2.gun, null))
                {
                    slot2.gun.Activate();
                }

                if (!ReferenceEquals(slot3.gun, null))
                {
                    slot3.gun.Deactivate();
                }
            }

            if (Input.GetKeyDown("3"))
            {
                slot1.transform.GetComponent<Image>().color = new Color(0.7686275f, 0.7686275f, 0.7686275f);
                slot2.transform.GetComponent<Image>().color = new Color(0.7686275f, 0.7686275f, 0.7686275f);
                slot3.transform.GetComponent<Image>().color = Color.yellow;
                slot1.isActive = false;
                slot2.isActive = false;
                slot3.isActive = true;
                if (!ReferenceEquals(slot1.gun, null))
                {
                    slot1.gun.Deactivate();
                }

                if (!ReferenceEquals(slot2.gun, null))
                {
                    slot2.gun.Deactivate();
                }

                if (!ReferenceEquals(slot3.gun, null))
                {
                    slot3.gun.Activate();
                }
            }
        }
        foreach (var slot in weaponSlots)
        {
            if (!ReferenceEquals(slot.gunItem, null) && ReferenceEquals(slot.gun, null))
            {
                slot.gun = Instantiate(slot.gunItem.gun, slot.gunItem.gun.EquipPosition, quaternion.identity);
                slot.gun.transform.parent = gameObject.transform;
                slot.gun.transform.localPosition = slot.gunItem.gun.EquipPosition;
            }

            if (ReferenceEquals(slot.gunItem, null) && !ReferenceEquals(slot.gun, null))
            {
                Destroy(slot.gun.gameObject);
                slot.gun = null;
            }

            if (!ReferenceEquals(slot.gunItem, null) && !ReferenceEquals(!slot.gun, null) &&
                slot.gunItem.gun.EquipPosition != slot.gun.EquipPosition)
            {
                Destroy(slot.gun.gameObject);
                slot.gun = Instantiate(slot.gunItem.gun, slot.gunItem.gun.EquipPosition, quaternion.identity);
                slot.gun.transform.parent = gameObject.transform;
                slot.gun.transform.localPosition = slot.gunItem.gun.EquipPosition;
            }

            if (!ReferenceEquals(slot.gun, null))
            {
                if (slot.isActive)
                {
                    slot.gun.Activate();
                }
                else
                {
                    slot.gun.Deactivate();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
    
    // Получение урона персонажем 
    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            SoundManager.instance.PlaySound(hurtSound);
            currentHealth -= damage;
            healthBar.UpdateHealth(maxHealth, currentHealth);
            StartCoroutine(Invicibility());
        }
      
    }

    IEnumerator Invicibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(InvincibilityTime);
        isInvincible = false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            GroundItem groundItem = other.GetComponent<GroundItem>();
            if (!groundItem.itemTaken)
            {
                inventory.AddItem(groundItem.item, 1);
                groundItem.itemTaken = true;
                Destroy(groundItem.GameObject());
            }
            
        }
    }

    public void OnApplicationQuit()
    {
        inventory.Container.items = new InventorySlot[20];
    }

    
    // Употребление блюда для восстановения здоровья
    public void Consume(DishObject dishObject)
    {
        SoundManager.instance.PlaySound(eatingSound);
        maxHealth += dishObject.HPIncreased;
        currentHealth += dishObject.HPRestored;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthBar.UpdateHealth(maxHealth, currentHealth);
    }
}
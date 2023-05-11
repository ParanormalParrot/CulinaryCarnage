using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
// Класс отображения инвентаря
public class DisplayInventory : MonoBehaviour
{
    public Player player;
    public InventoryObject inventory;
    public MouseItem mouseItem = new MouseItem();
    public Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
    public GameObject InventorySlotPrefab;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEMS;
    public int NUMBER_OF_COLUMNS;
    public int Y_SPACE_BETWEEN_ITEMS;
    public Vector3 mousePosition;
    public WeaponSlot[] weaponSlots;
    public Dictionary<GameObject, WeaponSlot> weaponsDisplayed = new Dictionary<GameObject, WeaponSlot>();
    public bool leftClick;
    public bool rightClick;
    public GameObject descriptionPanel;

    void Start()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        weaponsDisplayed = new Dictionary<GameObject, WeaponSlot>();
        weaponSlots = FindObjectsOfType<WeaponSlot>();
        player = FindObjectOfType<Player>();
        foreach (var slot in weaponSlots)
        {
            var obj = slot.gameObject;
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            weaponsDisplayed.Add(obj, slot);
        }
        CreateSlots();
        UpdateSlots();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlots();
        if (Input.GetMouseButton(0))
        {
            leftClick = true;
            rightClick = false;
        }
        else if (Input.GetMouseButton(1))
        {
            leftClick = false;
            rightClick = true;
        }
        else
        {
            leftClick = false;
            rightClick = false;
        }

        mousePosition = Input.mousePosition;
        mousePosition.z = 10.0f; //distance of the plane from the camera
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    public void CreateSlots()
    {
        for (int i = 0; i < inventory.Container.items.Length; i++)
        {
            var obj = Instantiate(InventorySlotPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            AddEvent(obj, EventTriggerType.PointerClick, delegate { OnClick(obj); });
            itemsDisplayed.Add(obj, inventory.Container.items[i]);
        }
    }

    public void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            if (_slot.Value != null)
            {
                if (_slot.Value.item != null)
                {
                    _slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().sprite =
                        _slot.Value.item.sprite;
                    _slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                    _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text =
                        _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
                }
                else
                {
                    _slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().sprite = null;
                    _slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                    _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
                }
            }
        }

        foreach (var _slot in weaponsDisplayed)
        {
            if (_slot.Value.gunItem != null)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
                    _slot.Value.gunItem.sprite;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            }
        }
    }

    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }


    public void OnClick(GameObject obj)
    {
        if (rightClick)
        {
            if (itemsDisplayed.ContainsKey(obj) && !ReferenceEquals(itemsDisplayed[obj].item, null) &&
                itemsDisplayed[obj].item.type == ItemType.Dish)
            {
                player.Consume((DishObject)itemsDisplayed[obj].item);
                itemsDisplayed[obj].item = null;
            }
        }
    }

    public void OnEnter(GameObject obj)
    {
        mouseItem.hoverObj = obj;
        if (itemsDisplayed.ContainsKey(obj) && itemsDisplayed[obj].item != null)
        {
            mouseItem.hoverItem = itemsDisplayed[obj];
            //mouseItem.descriptionPanel = Instantiate(descriptionPanel, mousePosition, Quaternion.identity);
            //mouseItem.descriptionPanel.transform.SetParent(transform);
            //mouseItem.descriptionPanel.transform.localScale = new Vector3(1, 1, 1);
            //mouseItem.descriptionPanel.transform.localPosition = new Vector3(obj.transform.localPosition.x,
                //obj.transform.localPosition.y - 52, 0);
        }

        if (weaponsDisplayed.ContainsKey(obj) && weaponsDisplayed[obj].gunItem != null)
        {
            mouseItem.hoverItem = new InventorySlot(weaponsDisplayed[obj].gunItem, 1);
        }
    }

    public void OnExit(GameObject obj)
    {
        mouseItem.hoverObj = null;
        mouseItem.hoverItem = null;
        //Destroy(mouseItem.descriptionPanel.GameObject());
        //mouseItem.descriptionPanel = null;
    }

    public void OnDragStart(GameObject obj)
    {
        if (leftClick)
        {
            if (obj != null)
            {
                var mouseObject = new GameObject();
                var rt = mouseObject.AddComponent<RectTransform>();
                rt.sizeDelta = new Vector2(2, 2);
                mouseObject.transform.SetParent(transform.parent);
                if (itemsDisplayed.ContainsKey(obj) && itemsDisplayed[obj].item != null)
                {
                    var img = mouseObject.AddComponent<Image>();
                    img.sprite = itemsDisplayed[obj].item.sprite;
                    img.raycastTarget = false;
                    mouseItem.obj = mouseObject;
                    mouseItem.item = itemsDisplayed[obj];
                }


                else if (weaponsDisplayed.ContainsKey(obj) && weaponsDisplayed[obj].gunItem != null)
                {
                    var img = mouseObject.AddComponent<Image>();
                    img.sprite = weaponsDisplayed[obj].gunItem.sprite;
                    img.raycastTarget = false;
                    mouseItem.obj = mouseObject;
                    mouseItem.item = new InventorySlot(weaponsDisplayed[obj].gunItem, 1);
                }
            }
        }
    }

    public void OnDragEnd(GameObject obj)
    {
        if (leftClick)
        {
            if (mouseItem.hoverObj)
            {
                if (itemsDisplayed.ContainsKey(obj) && itemsDisplayed[obj].item != null &&
                    itemsDisplayed.ContainsKey(mouseItem.hoverObj))
                {
                    inventory.MoveItem(itemsDisplayed[obj], itemsDisplayed[mouseItem.hoverObj]);
                }

                else if (itemsDisplayed.ContainsKey(obj) && itemsDisplayed[obj].item != null &&
                         itemsDisplayed[obj].item.type == ItemType.Dish &&
                         weaponsDisplayed.ContainsKey(mouseItem.hoverObj))
                {
                    DishObject temp = (DishObject)itemsDisplayed[obj].item;
                    itemsDisplayed[obj].UpdateSlot(weaponsDisplayed[mouseItem.hoverObj].gunItem, 1);
                    weaponsDisplayed[mouseItem.hoverObj].gunItem = temp;
                    weaponsDisplayed[mouseItem.hoverObj].gameObject.transform.GetChild(0)
                            .GetComponentInChildren<Image>().sprite =
                        weaponsDisplayed[mouseItem.hoverObj].gunItem.sprite;
                    weaponsDisplayed[mouseItem.hoverObj].gameObject.transform.GetChild(0)
                        .GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                }

                else if (weaponsDisplayed.ContainsKey(obj) && itemsDisplayed.ContainsKey(mouseItem.hoverObj))
                {
                    DishObject temp = (DishObject)itemsDisplayed[mouseItem.hoverObj].item;
                    itemsDisplayed[mouseItem.hoverObj].UpdateSlot(weaponsDisplayed[obj].gunItem, 1);
                    weaponsDisplayed[obj].gunItem = temp;
                    weaponsDisplayed[obj].gameObject.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
                        null;
                    weaponsDisplayed[obj].gameObject.transform.GetChild(0).GetComponentInChildren<Image>().color =
                        new Color(1, 1, 1, 0);
                }
            }

            Destroy(mouseItem.obj);
            mouseItem.item = null;
        }
    }

    public void OnDrag(GameObject obj)
    {
        if (mouseItem.obj != null)
            mouseItem.obj.GetComponent<RectTransform>().position = mousePosition;
    }


    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEMS * (i % NUMBER_OF_COLUMNS)),
            Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMNS)), 0f);
    }

    public void Clear()
    {

        foreach (var obj in itemsDisplayed.Keys.ToList())
        {
            itemsDisplayed[obj] = null;
        }
        
    }

    public void ResetMenu()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        weaponsDisplayed = new Dictionary<GameObject, WeaponSlot>();
        foreach (var slot in weaponSlots)
        {
            var obj = slot.gameObject;
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            weaponsDisplayed.Add(obj, slot);
        }

        player = FindObjectOfType<Player>();
        CreateSlots();
    }
}

public class MouseItem
{
    public GameObject obj;
    public InventorySlot item;
    public InventorySlot hoverItem;
    public GameObject hoverObj;
    public GameObject descriptionPanel;
}
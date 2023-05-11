using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

// Класс меню готовки
public class CookingMenu : MonoBehaviour
{
    
    private InventorySlot[] slots = new InventorySlot[20];
    public Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
    public RecipeBook recipeBook;
    [FormerlySerializedAs("inventoryObject")] public InventoryObject inventory;
    public GameObject InventorySlotPrefab;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEMS;
    public int NUMBER_OF_COLUMNS;
    public int Y_SPACE_BETWEEN_ITEMS;
    public bool leftClick;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new InventorySlot();
        }
        CreateSlots();
        UpdateMenu();
        gameObject.SetActive(false);
    }
    
    public void CreateSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            var obj = Instantiate(InventorySlotPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            AddEvent(obj, EventTriggerType.PointerClick, delegate { OnClick(obj); });
            itemsDisplayed.Add(obj, slots[i]);
        }
    }


    // Update is called once per frame
    void Update()
    {
        UpdateMenu();
        if (Input.GetMouseButton(0))
        {
            leftClick = true;
        }
        else
        {
            leftClick = false;
        }
    }

    public void UpdateMenu()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].item = null;
        }
        foreach (var recipe in recipeBook.recipes)
        {
            if (CheckIngredients(recipe))
            {
                foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
                {
                    if (_slot.Value.item == null)
                    {
                        _slot.Value.item = recipe.product;
                        _slot.Value.amount = 1;
                        _slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().sprite =
                            _slot.Value.item.sprite;
                        break;
                    }
                }
            }
        }
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
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
    
    bool CheckIngredients(Recipe recipe)
    {
        for (int i = 0; i < recipe.ingredients.Count; i++)
        {
            bool flag = true;
            for (int j = 0; j < inventory.Container.items.Length; j++)
            {
                if (recipe.ingredients[i].item == inventory.Container.items[j].item && inventory.Container.items[j].amount >= recipe.ingredients[i].amount)
                {
                    flag = false;
                }
            }

            if (flag)
            {
                return false;
            }
        }

        return true;
    }

    public void OnClick(GameObject gameObject)
    {
        if (leftClick)
        {
            if (itemsDisplayed.ContainsKey(gameObject))
            {
                if (itemsDisplayed[gameObject].item != null)
                {
                    Cook(itemsDisplayed[gameObject].item);
                }
            }
        }
        
    }

    void Cook(ItemObject obj)
    {
        if (recipeBook.dictionary.ContainsKey(obj))
        {

            foreach (var slot in recipeBook.dictionary[obj])
            {
                for (int j = 0; j < inventory.Container.items.Length; j++)
                {
                    if (slot.item == inventory.Container.items[j].item)
                    {
                        inventory.Container.items[j].amount -= slot.amount;
                        if (inventory.Container.items[j].amount <= 0)
                        {
                            inventory.Container.items[j].amount = 0;
                            inventory.Container.items[j].item = null;
                                
                        }
                    }
                }
            }
            inventory.AddItem(obj, 1);
            UpdateMenu();
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
    
    
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEMS * (i % NUMBER_OF_COLUMNS)), Y_START + (-Y_SPACE_BETWEEN_ITEMS* (i/NUMBER_OF_COLUMNS)), 0f);
    }
}

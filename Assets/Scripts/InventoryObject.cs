using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "New Default Object",  menuName = "Inventory System/Inventory") ]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    public Inventory Container;

    public void AddItem(ItemObject item, int amount)
    {
        
        bool containsItem = false;
        if (!item.isStackable)
        {
            SetEmptySlot(item, amount);
        }
        else
        {
            for (int i = 0; i < Container.items.Length; i++)
            {
                if (Container.items[i]!= null)
                {
                    if (Container.items[i].item == item)
                    {
                        Container.items[i].IncreaseAmount(amount);
                        containsItem = true;
                    
                    }
                }
               
            }

            if (!containsItem)
            {
                SetEmptySlot(item, amount);
            }
        }
        
        
    }
    
    public void Clear()
    {
        for (int i = 0; i < Container.items.Length; i++)
        {
            Container.items[i].item = null;
            Container.items[i].amount = 0;
        }
    }
    public InventorySlot SetEmptySlot(ItemObject item, int amount)
    {
        for (int i = 0; i < Container.items.Length; i++)
        {
            if (Container.items[i] != null)
            {
                if (Container.items[i].item == null)
                {
                    Container.items[i].UpdateSlot(item, amount);
                    return Container.items[i];
                }
            }
        }

        return null;
    }
    
    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot(item2.item, item2.amount);
        item2.UpdateSlot(item1.item, item1.amount);
        item1.UpdateSlot(temp.item, temp.amount);
    }
    
    
    public void RemoveItem(ItemObject _item)
    {
        for (int i = 0; i < Container.items.Length; i++)
        {
            if(Container.items[i].item == _item)
            {
                Container.items[i].UpdateSlot(null, 0);
            }
        }
    }
}

[Serializable]
public class Inventory
{
    public InventorySlot[] items = new InventorySlot[20];
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;

    public InventorySlot(ItemObject item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
    
    public InventorySlot()
    {
        item = null;
        amount = 0;
    }

    public void UpdateSlot(ItemObject item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public void IncreaseAmount(int amount)
    {
        this.amount += amount;
    }
    
    
}
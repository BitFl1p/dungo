﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CraftableInventory
{
    public event EventHandler OnCraftableListChanged;
    public List<Item> itemList;
    private Action<Item> useItemAction;
    PlayerController pCon;

    public CraftableInventory(Action<Item> useItemAction,PlayerController controller)
    {
        this.useItemAction = useItemAction;
        itemList = new List<Item>();
        pCon = controller;

    }
    public void AddCraftable(Item item)
    {
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }

        OnCraftableListChanged?.Invoke(this, EventArgs.Empty);
        pCon.CheckGear();
    }
    public void RemoveCraftable(Item item)
    {

        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                itemList.Remove(itemInInventory);
            }
        }
        else
        {
            itemList.Remove(item);
        }

        OnCraftableListChanged?.Invoke(this, EventArgs.Empty);
        pCon.CheckGear();
    }
    public void ClearCraftables()
    {
        itemList.Clear();
        
        
    }
    
    public List<Item> GetCraftableList()
    {
        return itemList;
    }
}

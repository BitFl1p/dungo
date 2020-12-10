using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;
    public List<Item> itemList;
    private Action<Item> useItemAction;
    PlayerController pCon;
    public Inventory(Action<Item> useItemAction,PlayerController controller)
    {
        this.useItemAction = useItemAction;
        itemList = new List<Item>();
        pCon = controller;
        //AddItem(new Item { itemType = Item.ItemType.EmbroidedSword, amount = 1 });
        //AddItem(new Item { itemType = Item.ItemType.SilverSword, amount = 1 });
        //AddItem(new Item { itemType = Item.ItemType.IronSword, amount = 1 });
        //AddItem(new Item { itemType = Item.ItemType.RefinedWoodSword, amount = 1 });
        //AddItem(new Item { itemType = Item.ItemType.ReinforcedWoodSword, amount = 1 });
        //AddItem(new Item { itemType = Item.ItemType.WoodenSword, amount = 1 });

    }
    
    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach(Item inventoryItem in itemList)
            {
                if(inventoryItem.itemType == item.itemType)
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
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
        pCon.CheckGear();
    }
    public void RemoveItem(Item item)
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
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    itemInInventory = inventoryItem; 
                }
            }
            itemList.Remove(itemInInventory);
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
        pCon.CheckGear();
    }
    public void UseItem(Item item)
    {
        useItemAction(item);
    }
    public List<Item> GetItemList()
    {
        return itemList;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CraftingItems : MonoBehaviour
{

    [SerializeField] private Inventory inv;
    public List<Item> craftables;
    [SerializeField] private CraftableInventory craftInv;
    private void Awake()
    {
        craftables = craftInv.itemList;
    }

    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.tag == "Crafting")
        {
            craftInv.ClearCraftables();
            if (CheckForItem(Item.ItemType.Wood,1))
            {
                craftInv.AddCraftable(new Item { itemType = Item.ItemType.WoodenHandle, amount = 1 });

            }
            if (CheckForItem(Item.ItemType.Wood, 2))
            {

                craftInv.AddCraftable(new Item { itemType = Item.ItemType.WoodenBlade, amount = 1 });
            }
            if (CheckForItem(Item.ItemType.WoodenHandle, 1) && CheckForItem(Item.ItemType.WoodenBlade, 1) && CheckForItem(Item.ItemType.Rope, 1))
            {
                craftInv.AddCraftable(new Item { itemType = Item.ItemType.WoodenSword, amount = 1 });

            }
            if (CheckForItem(Item.ItemType.WoodenSword, 1)&& CheckForItem(Item.ItemType.MetalOre, 1))
            {
                craftInv.AddCraftable(new Item { itemType = Item.ItemType.ReinforcedWoodSword, amount = 1 });

            }
            if (CheckForItem(Item.ItemType.MetalOre, 2))
            {
                craftInv.AddCraftable(new Item { itemType = Item.ItemType.RefinedOre, amount = 1 });

            }
            if (CheckForItem(Item.ItemType.RefinedOre, 1) && CheckForItem(Item.ItemType.ReinforcedWoodSword, 2))
            {
                craftInv.AddCraftable(new Item { itemType = Item.ItemType.RefinedWoodSword, amount = 1 });

            }
        }
        
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Crafting")
        {
            craftInv.ClearCraftables();
        }
    }
    public void CraftItem(List<Item> itemsToRemove, List<Item> itemsToAdd)
    {
        foreach(Item currentItem in itemsToRemove)
        {
            inv.RemoveItem(currentItem);
        }
        foreach (Item currentItem in itemsToAdd)
        {
            inv.AddItem(currentItem);
        }

        
        
    }
    public bool CheckForItem(Item.ItemType requiredItemType, int quantity)
    {
        foreach (Item item in inv.itemList)
        {
            if (item.itemType == requiredItemType)
            {
                if (item.amount >= quantity)
                {
                    return true;
                }
            }

        }
        return false;

    }
    
}


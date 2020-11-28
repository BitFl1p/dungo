using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditorInternal.VersionControl;
using System.Linq;

public class CraftingItems : MonoBehaviour
{

    [SerializeField] private Inventory inv;
    public List<Item> craftables;
    [SerializeField] private CraftableInventory craftInv;
    [SerializeField] private UICrafting uiCraft;
    

    private void Awake()
    {
        //if(craftInv.itemList != null)
        //{
        //    craftables = craftInv.itemList;
        //}
    }
    public void SetCraftInv(CraftableInventory craftInv)
    {
        this.craftInv = craftInv;
        
        
    }
    public void SetInv(Inventory inv)
    {
        this.inv = inv;


    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Crafting") { CheckCraftables(); }
        if (other.tag == "Smelting") { CheckSmeltables(); }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Crafting")
        {
            if (craftInv != null) { craftInv.ClearCraftables(); }
            uiCraft.RefreshCraftables();
        }
        if (other.tag == "Smelting")
        {
            if (craftInv != null) { craftInv.ClearCraftables(); }
            uiCraft.RefreshCraftables();
        }
    }

        
        

    public void CraftItem(List<Item> itemsToRemove, List<Item> itemsToAdd)
    {
        for(int i = itemsToRemove.Count - 1; i >= 0; i--)
        {
            
            inv.RemoveItem(itemsToRemove.ElementAt(i));
        }
        //foreach (Item item in itemsToRemove)
        //{
        //    Debug.Log(item);
        //    inv.RemoveItem(item);
            
        //}
        foreach (Item currentItem in itemsToAdd)
        {
            Item.ItemType currentItemType = currentItem.itemType;
            inv.AddItem(new Item { itemType = currentItemType, amount = 1});
            craftInv.RemoveCraftable(currentItem);
        }
        CheckCraftables();
        uiCraft.RefreshCraftables();


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
    public void CheckCraftables()
    {
        if (craftInv != null) { craftInv.ClearCraftables(); }


        if (CheckForItem(Item.ItemType.Wood, 1))
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
        if (CheckForItem(Item.ItemType.WoodenSword, 1) && CheckForItem(Item.ItemType.MetalOre, 1))
        {
            craftInv.AddCraftable(new Item { itemType = Item.ItemType.ReinforcedWoodSword, amount = 1 });

        }
        if (CheckForItem(Item.ItemType.MetalOre, 2))
        {
            craftInv.AddCraftable(new Item { itemType = Item.ItemType.RefinedOre, amount = 1 });

        }
        if (CheckForItem(Item.ItemType.RefinedOre, 1) && CheckForItem(Item.ItemType.ReinforcedWoodSword, 1))
        {
            craftInv.AddCraftable(new Item { itemType = Item.ItemType.RefinedWoodSword, amount = 1 });

        }
        if (CheckForItem(Item.ItemType.Ruby, 1) && CheckForItem(Item.ItemType.BrassNecklace, 1))
        {
            craftInv.AddCraftable(new Item { itemType = Item.ItemType.RubyNecklace, amount = 1 });

        }
        if (CheckForItem(Item.ItemType.SilverSword, 1) && CheckForItem(Item.ItemType.Emerald, 1))
        {
            craftInv.AddCraftable(new Item { itemType = Item.ItemType.EmbroidedSword, amount = 1 });

        }
        uiCraft.RefreshCraftables();
        
    }
    public void CheckSmeltables()
    {
        if (CheckForItem(Item.ItemType.MetalOre, 1))
        {
            craftInv.AddCraftable(new Item { itemType = Item.ItemType.Brass, amount = 1 });
            craftInv.AddCraftable(new Item { itemType = Item.ItemType.Iron, amount = 1 });
        }
        if (CheckForItem(Item.ItemType.RefinedOre, 1))
        {
            craftInv.AddCraftable(new Item { itemType = Item.ItemType.Silver, amount = 1 });
        }
        uiCraft.RefreshCraftables();
    }
}


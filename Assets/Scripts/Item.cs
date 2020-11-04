using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Item 
{

    public enum ItemType
    {
        Rope,
        Coal,
        MetalOre,
        Wood,
        RefinedOre,
        WoodenHandle,
        WoodenBlade,
        WoodenSword,
        ReinforcedWoodSword,
        RefinedWoodSword,
        Iron,
        Brass,
        IronBlade,
        IronSword,
        BrassCharm,
        BrassNecklace,
        Silver,
        SilverBlade,
        SilverSword,

    }
    public ItemType itemType;
    public int amount;
    

    public List<Item> GetRecipe(ItemType myItem)
    {
        
        switch (myItem)
        {

            default:
            case ItemType.Rope: 
            case ItemType.Coal: 
            case ItemType.MetalOre: 
            case ItemType.Wood: 
                return null;
            case ItemType.Iron:
            case ItemType.Brass:
                return new List<Item>() { new Item { itemType = Item.ItemType.MetalOre, amount = 1 } };


            case ItemType.RefinedOre:                       return new List<Item>() { new Item { itemType = Item.ItemType.MetalOre, amount = 2 } };
            case ItemType.WoodenHandle:                     return new List<Item>() { new Item { itemType = Item.ItemType.Wood, amount = 1 } };
            case ItemType.WoodenBlade:                      return new List<Item>() { new Item { itemType = Item.ItemType.Wood, amount = 2 } };
            case ItemType.IronBlade:                        return new List<Item>() { new Item { itemType = Item.ItemType.Iron, amount = 2 } };
            case ItemType.BrassCharm:                       return new List<Item>() { new Item { itemType = Item.ItemType.Brass, amount = 1 } };
            case ItemType.Silver:                           return new List<Item>() { new Item { itemType = Item.ItemType.RefinedOre, amount = 2 } };
            case ItemType.SilverBlade:                      return new List<Item>() { new Item { itemType = Item.ItemType.Silver, amount = 2 } };
            case ItemType.BrassNecklace:                    return new List<Item>() { new Item { itemType = Item.ItemType.Brass, amount = 2 } };



            case ItemType.WoodenSword:                      return new List<Item>()
                                                                    {
                                                                        new Item { itemType = Item.ItemType.WoodenHandle, amount = 1 },
                                                                        new Item { itemType = Item.ItemType.WoodenBlade, amount = 1 },
                                                                        new Item { itemType = Item.ItemType.Rope, amount = 1 }
                                                                    };

                
            case ItemType.ReinforcedWoodSword:              return new List<Item>()
                                                                    {
                                                                        new Item { itemType = Item.ItemType.WoodenSword, amount = 1 },
                                                                        new Item { itemType = Item.ItemType.MetalOre, amount = 1 },
                                                                        
                                                                    };
                
            case ItemType.RefinedWoodSword:                 return new List<Item>()
                                                                    {
                                                                        new Item { itemType = Item.ItemType.ReinforcedWoodSword, amount = 1 },
                                                                        new Item { itemType = Item.ItemType.RefinedOre, amount = 1 },
                                                                        
                                                                    };
                
            case ItemType.IronSword:                        return new List<Item>()
                                                                    {
                                                                        new Item { itemType = Item.ItemType.Iron, amount = 1 },
                                                                        new Item { itemType = Item.ItemType.WoodenHandle, amount = 1 },
                                                                        
                                                                    };
                
                
            case ItemType.SilverSword:                      return new List<Item>()
                                                                    {
                                                                        new Item { itemType = Item.ItemType.SilverBlade, amount = 1 },
                                                                        new Item { itemType = Item.ItemType.WoodenHandle, amount = 1 },
                                                                        
                                                                    };
                
        }
    }
    
    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Rope:                  return ItemAssets.Instance.RopeSprite;
            case ItemType.Coal:                  return ItemAssets.Instance.CoalSprite;
            case ItemType.MetalOre:              return ItemAssets.Instance.MetalOreSprite;
            case ItemType.Wood:                  return ItemAssets.Instance.WoodSprite;
            case ItemType.RefinedOre:            return ItemAssets.Instance.RefinedOreSprite;
            case ItemType.WoodenHandle:          return ItemAssets.Instance.WoodenHandleSprite;
            case ItemType.WoodenBlade:           return ItemAssets.Instance.WoodenBladeSprite;
            case ItemType.WoodenSword:           return ItemAssets.Instance.WoodenSwordSprite;
            case ItemType.ReinforcedWoodSword:   return ItemAssets.Instance.ReinforcedWoodSwordSprite;
            case ItemType.RefinedWoodSword:      return ItemAssets.Instance.RefinedWoodSwordSprite;
            case ItemType.Iron:                  return ItemAssets.Instance.IronSprite;
            case ItemType.Brass:                 return ItemAssets.Instance.BrassSprite;
            case ItemType.IronBlade:             return ItemAssets.Instance.IronBladeSprite;
            case ItemType.IronSword:             return ItemAssets.Instance.IronSwordSprite;
            case ItemType.BrassCharm:            return ItemAssets.Instance.BrassCharmSprite;
            case ItemType.BrassNecklace:         return ItemAssets.Instance.BrassNecklaceSprite;
            case ItemType.Silver:                return ItemAssets.Instance.SteelSprite;
            case ItemType.SilverBlade:           return ItemAssets.Instance.SteelBladeSprite;
            case ItemType.SilverSword:           return ItemAssets.Instance.SteelSwordSprite;


        }
    }
    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Rope: 
            case ItemType.Coal: 
            case ItemType.MetalOre: 
            case ItemType.Silver: 
            case ItemType.Wood:
            case ItemType.Iron:
            case ItemType.Brass:
            case ItemType.RefinedOre:
                return true;
            
            case ItemType.WoodenHandle: 
            case ItemType.WoodenBlade: 
            case ItemType.ReinforcedWoodSword: 
            case ItemType.RefinedWoodSword: 
            case ItemType.IronBlade: 
            case ItemType.BrassCharm: 
            case ItemType.BrassNecklace: 
            case ItemType.SilverBlade:
            case ItemType.SilverSword:

                return false;

        }
    }
}

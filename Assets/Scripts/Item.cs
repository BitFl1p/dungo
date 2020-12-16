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
        Ruby,
        BrassNecklace,
        RubyNecklace,
        Silver,
        SilverBlade,
        SilverSword,
        Emerald,
        EmbroidedSword,

    }
    public ItemType itemType;
    public int amount;
    public string GetTooltip(ItemType myItem)
    {
        switch (myItem)
        {
            default: return "the fuck are you doing here";
            case ItemType.BrassNecklace: return "";
            case ItemType.BrassCharm:
            case ItemType.Coal:
            case ItemType.Brass: return "useless item left over from when I had bigger plans for this game";

            case ItemType.Rope: return "A simple plain rope";
            case ItemType.MetalOre: return "A chunk of metal ore";
            case ItemType.Wood: return "Some wood";
            case ItemType.Iron: return "A smelted ingot of iron";
            
            case ItemType.RefinedOre: return "A refined version of the metal ore you had";
            case ItemType.WoodenHandle: return "A sword handle made of wood";
            case ItemType.WoodenBlade:  return "A sword blade made of wood";
            case ItemType.IronBlade:return "A blade made of what I can only describe as rust";
            case ItemType.Silver:return "A polished ingot of silver more well made than this game";
            case ItemType.SilverBlade:return "The blade of a sword made out of silver and destined to greatness";
            case ItemType.WoodenSword:return "A rudimentary sword made of wood. Similar to one of those toys you'd see at the fare";
            case ItemType.ReinforcedWoodSword:return "A wood sword but the blade is coated in rocks in a way that resembles barbed wire";
            case ItemType.RefinedWoodSword:return "A wood sword but it's handle has been replaced with a less wobbly stone one.";
            case ItemType.IronSword:return "A sword covered in what I can only describe as 'Solid Tetanus.' Oh wait you don't know what that is yet do you?";
            case ItemType.SilverSword:return "A sword forged almost to perfection, it still holds some potential to improve";
            case ItemType.RubyNecklace:return "A magic necklace that lets you dash. Quite useful for dodging things";
            case ItemType.EmbroidedSword:return "A perfect legendary sword with a magic emerald in the middle, able to unleash a ravenous attack when charged.";


    
    }
    }
    
    

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
                                                                        new Item { itemType = Item.ItemType.Silver, amount = 1 },
                                                                        
                                                                    };
            case ItemType.RubyNecklace:                     return new List<Item>()
                                                                    {
                                                                        new Item { itemType = Item.ItemType.BrassNecklace, amount = 1 },
                                                                        new Item { itemType = Item.ItemType.Ruby, amount = 1 },
                                                                    };
            case ItemType.EmbroidedSword:
                return new List<Item>()
                                                                    {
                                                                        new Item { itemType = Item.ItemType.SilverSword, amount = 1 },
                                                                        new Item { itemType = Item.ItemType.Emerald, amount = 1 },
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
            case ItemType.RubyNecklace:          return ItemAssets.Instance.RubyNecklaceSprite;
            case ItemType.Ruby:                  return ItemAssets.Instance.RubySprite;
            case ItemType.Emerald:               return ItemAssets.Instance.EmeraldSprite;
            case ItemType.EmbroidedSword:        return ItemAssets.Instance.EmbroidedSwordSprite;


        }
    }
    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Emerald:
            case ItemType.Rope: 
            case ItemType.Coal: 
            case ItemType.MetalOre: 
            case ItemType.Silver: 
            case ItemType.Wood:
            case ItemType.Iron:
            case ItemType.Brass:
            case ItemType.RefinedOre:
            case ItemType.Ruby:
                return true;
            case ItemType.EmbroidedSword:
            case ItemType.WoodenHandle: 
            case ItemType.WoodenBlade: 
            case ItemType.ReinforcedWoodSword: 
            case ItemType.RefinedWoodSword: 
            case ItemType.IronBlade: 
            case ItemType.BrassCharm: 
            case ItemType.BrassNecklace: 
            case ItemType.SilverBlade:
            case ItemType.SilverSword:
            case ItemType.RubyNecklace:

                return false;

        }
    }
}

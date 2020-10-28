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
        BrassCharm,
        BrassNecklace,
        Steel,
        SteelBlade,
        SteelSword,

    }
    public ItemType itemType;
    public int amount;
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
            case ItemType.BrassCharm:            return ItemAssets.Instance.BrassCharmSprite;
            case ItemType.BrassNecklace:         return ItemAssets.Instance.BrassNecklaceSprite;
            case ItemType.Steel:                 return ItemAssets.Instance.SteelSprite;
            case ItemType.SteelBlade:            return ItemAssets.Instance.SteelBladeSprite;
            case ItemType.SteelSword:            return ItemAssets.Instance.SteelSwordSprite;


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
            case ItemType.Steel: 
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
            case ItemType.SteelBlade:

                return false;

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    public Transform pfItemWorld;
    public Sprite RopeSprite;
    public Sprite CoalSprite;
    public Sprite MetalOreSprite;
    public Sprite WoodSprite;
    public Sprite RefinedOreSprite;
    public Sprite WoodenHandleSprite;
    public Sprite WoodenBladeSprite;
    public Sprite WoodenSwordSprite;
    public Sprite ReinforcedWoodSwordSprite;
    public Sprite RefinedWoodSwordSprite;
    public Sprite IronSprite;
    public Sprite BrassSprite;
    public Sprite IronBladeSprite;
    public Sprite BrassCharmSprite;
    public Sprite BrassNecklaceSprite;
    public Sprite SteelSprite;
    public Sprite SteelBladeSprite;
    public Sprite SteelSwordSprite;
}

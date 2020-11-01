using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICrafting : MonoBehaviour
{
    
    private UnityEngine.Transform itemSlotContainer;
    private UnityEngine.Transform itemSlotTemplate;
    [SerializeField] private CraftingItems crafting;
    
    private CraftableInventory craftInv;

    // Start is called before the first frame update
    private void Awake()
    {
        
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
    }
    
    public void refreshCraftables()
    {
        
        foreach (UnityEngine.Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = -5;
        int y = 2;
        float offsetX = 30f;
        float offsetY = 20f;

        float itemSlotCellSize = 60f;
        foreach (Item item in craftInv.itemList)
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                
            };
            
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize + offsetX, y * itemSlotCellSize + offsetY);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            

            y++;
        }
    }
        
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICrafting : MonoBehaviour
{

    [SerializeField] private UnityEngine.Transform itemSlotContainer;
    [SerializeField] private UnityEngine.Transform itemSlotTemplate;
    [SerializeField] private CraftingItems crafting;
    
    private CraftableInventory craftInv;
    
    // Start is called before the first frame update

    public void SetCraftInv(CraftableInventory craftInv)
    {
        this.craftInv = craftInv;
        craftInv.OnCraftableListChanged += CraftableInventory_OnCraftableListChanged;
        RefreshCraftables();
    }
    private void CraftableInventory_OnCraftableListChanged(object sender, System.EventArgs e)
    {
        RefreshCraftables();
    }
    public void RefreshCraftables()
    {
        
            foreach (UnityEngine.Transform child in itemSlotContainer)
            {
                if (child == itemSlotTemplate) continue;
                Destroy(child.gameObject);
            }
            int x = 0;
            int y = 0;
            float offsetX = 30f;
            float offsetY = 20f;

            float itemSlotCellSize = 60f;
            foreach (Item item in craftInv.itemList)
            {
                RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
                itemSlotRectTransform.gameObject.SetActive(true);
                itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
                {

                    crafting.CraftItem(item.GetRecipe(item.itemType), new List<Item>() { item });
                };

                itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize + offsetX, y * itemSlotCellSize + offsetY);
                Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
                image.sprite = item.GetSprite();


                y--;
            }
    }
        
        
}
    


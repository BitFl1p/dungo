using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UI_Inventory : MonoBehaviour
{
    
    private Inventory inventory;
    private UnityEngine.Transform itemSlotContainer;
    private UnityEngine.Transform itemSlotTemplate;
    private PlayerController player;
    public bool uIInventoryActive;
    public float moveSpeed;
    private RectTransform myRectTrans;
    private Vector2 myOffScreenPos;
    private Vector2 myOnScreenPos;
    public bool canOpenInv;

    private void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
        player = FindObjectOfType<PlayerController>();
        uIInventoryActive = false;
        myRectTrans = gameObject.GetComponent<RectTransform>();
        myOnScreenPos = new Vector2(myRectTrans.anchoredPosition.x, myRectTrans.anchoredPosition.y);
        myOffScreenPos = new Vector2(myRectTrans.anchoredPosition.x + 1000, myRectTrans.anchoredPosition.y);
        myRectTrans.anchoredPosition = myOffScreenPos;

    }
    private void Update()
    {
        if (!canOpenInv) { myRectTrans.anchoredPosition = myOffScreenPos; uIInventoryActive = false; Time.timeScale = 1f; return; }
        if (canOpenInv)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {

                switch (uIInventoryActive)
                {
                    case true:
                        myRectTrans.anchoredPosition = myOffScreenPos;

                        uIInventoryActive = false;
                        break;
                    case false:
                        myRectTrans.anchoredPosition = myOnScreenPos;

                        uIInventoryActive = true;
                        break;
                }

            }
            if (uIInventoryActive) { Time.timeScale = 0.5f; player.canMove = false; } else { Time.timeScale = 1f; player.canMove = true; }
        }
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }
    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }
    public void RefreshInventoryItems()
    {
        foreach(UnityEngine.Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = -5;
        int y = 2;
        float offsetX = 30f;
        float offsetY = 20f;

        float itemSlotCellSize = 60f;
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.GetComponent<TooltipHolder>().item = item;
            itemSlotRectTransform.GetComponent<TooltipHolder>().SetCraft(true);

            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
                inventory.RemoveItem(item);
                ItemWorld.DropItem(player.GetComponent<UnityEngine.Transform>().transform.position, duplicateItem);
            };
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize+offsetX, y * itemSlotCellSize+offsetY);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("text").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }
            
            x++;
            if (x > 4)
            {
                x = -5;
                y--;
            }
        }
        

    }
}

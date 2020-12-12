using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour
{
    private static Tooltip instance;
    private TMP_Text tooltipText;
    private RectTransform backgroundRectTransform;
    public bool crafting;
    [SerializeField]private RectTransform canvasRectTransform;
    
    void Awake()
    {
        instance = this;
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>(); 
        tooltipText = transform.Find("text").GetComponent<TMP_Text>();
        ShowTooltip("help");
        HideTooltip();
        
    }
    private void ShowTooltip(string tooltipString)
    {
        transform.SetAsLastSibling();
        gameObject.SetActive(true);
        tooltipText.text = tooltipString;
        float textPaddingSize = 8f;
        Vector2 backgroundSize = new Vector2(tooltipText.renderedWidth + textPaddingSize * 2, tooltipText.preferredHeight + textPaddingSize * 2);
        backgroundRectTransform.sizeDelta = backgroundSize;
    }
    void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out localPoint);
        transform.localPosition = new Vector2(localPoint.x - tooltipText.renderedWidth * -1.2f, localPoint.y - tooltipText.preferredHeight * 1.2f);
        Vector2 anchoredPosition = transform.GetComponent<RectTransform>().anchoredPosition;
        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }
        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }
        transform.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
    }
    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }
    public static void ShowTooltip_Static(string tooltipString)
    {
        instance.ShowTooltip(tooltipString);
    }
    public static void HideTooltip_Static()
    {
        instance.HideTooltip();
    }
    public static void SetCraftBool(bool craft)
    {
        instance.crafting = craft;
    }
}

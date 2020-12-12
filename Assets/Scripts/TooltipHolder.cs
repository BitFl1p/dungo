using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipHolder : MonoBehaviour
{
    public Item item;
    // Start is called before the first frame update
    private void Awake()
    {
        GetComponent<Button_UI>().MouseOverOnceTooltipFunc = () => Tooltip.ShowTooltip_Static(item.GetTooltip(item.itemType));

        GetComponent<Button_UI>().MouseOutOnceTooltipFunc = () => Tooltip.HideTooltip_Static();
    }
    public void SetCraft(bool craft)
    {
        Tooltip.SetCraftBool(craft);
    }


}

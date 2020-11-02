using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ItemWorld : MonoBehaviour
{
    
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        UnityEngine.Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);
        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);
        return itemWorld;
    }
    public static ItemWorld DropItem(Vector3 position, Item item)
    {
        Vector3 randomDir = new Vector3(Random.Range(1f, -1f), Random.Range(1f, -1f), 0f).normalized;
        ItemWorld itemWorld = SpawnItemWorld(position + randomDir*1.5f,item);
        itemWorld.GetComponent<Rigidbody2D>().AddForce(randomDir * 5f, ForceMode2D.Impulse);
        return itemWorld;
    }
    private Item item;
    private SpriteRenderer spriteRenderer;
    private TextMeshPro textMeshPro;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }
    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        if(item.amount> 1)
        {
            textMeshPro.SetText(item.amount.ToString());
        }
        else
        {
            textMeshPro.SetText("");
        }
        
    }
    public Item GetItem()
    {
        return item;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}

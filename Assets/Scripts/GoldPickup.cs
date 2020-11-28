using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickup : MonoBehaviour
{
    public int value;
    public MoneyManager theMN;
    // Start is called before the first frame update
    void Start()
    {
        theMN = FindObjectOfType<MoneyManager>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            theMN.AddMoney(value);
            Destroy(gameObject);
        }
    }
}

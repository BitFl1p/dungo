using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHealthManager : MonoBehaviour
{
    public int enemyMaxHealth;
    public int enemyCurrentHealth;
    public int expToGive;
    [Serializable]
    public struct ItemWithChance
    {
        public Item item;
        public float chance;
    }
    public GameObject pfItemWorld;
    public List<ItemWithChance> Drops;
    //public Dictionary<Item,float> ActualDrops;
    public string enemyQuestName;
    private QuestManager theQM;
    // Start is called before the first frame update
    void Start()
    {
        theQM = FindObjectOfType<QuestManager>();
        enemyCurrentHealth = enemyMaxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCurrentHealth <= 0)
        {
            theQM.enemyKilled = enemyQuestName;

            
            float rand = UnityEngine.Random.Range(0, 100);
            foreach (ItemWithChance thisItem in Drops)
            {
                rand = UnityEngine.Random.Range(0, 100);
                if (rand >= thisItem.chance)
                {
                    ItemWorld.SpawnItemWorld(transform.position, thisItem.item);
                    
                }
            }
            Destroy(gameObject);
        }
    }
    public void HurtEnemy(int damage) { enemyCurrentHealth -= damage; }
    public void SetMaxHealth() { enemyCurrentHealth = enemyMaxHealth; }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int enemyMaxHealth;
    public int enemyCurrentHealth;
    private PlayerStats playerStats;
    public int expToGive;

    public string enemyQuestName;
    private QuestManager theQM;
    // Start is called before the first frame update
    void Start()
    {
        theQM = FindObjectOfType<QuestManager>();
        enemyCurrentHealth = enemyMaxHealth;
        playerStats = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCurrentHealth <= 0)
        {
            theQM.enemyKilled = enemyQuestName;
            playerStats.AddExperience(expToGive);
            Destroy(gameObject);

        }
    }
    public void HurtEnemy(int damage) { enemyCurrentHealth -= damage; }
    public void SetMaxHealth() { enemyCurrentHealth = enemyMaxHealth; }
}


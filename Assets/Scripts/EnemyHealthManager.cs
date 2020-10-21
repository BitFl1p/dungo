using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int enemyMaxHealth;
    public int enemyCurrentHealth;
    private PlayerStats playerStats;
    public int expToGive;
    // Start is called before the first frame update
    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
        playerStats = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCurrentHealth <= 0)
        {
            playerStats.AddExperience(expToGive);
            Destroy(gameObject);

        }
    }
    public void HurtEnemy(int damage) { enemyCurrentHealth -= damage; }
    public void SetMaxHealth() { enemyCurrentHealth = enemyMaxHealth; }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider healthBar;
    public Text hpText;
    public Text levelText;
    public PlayerHealthManager playerHealth;
    private PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("UI").Length > 1)
        {
            Object.Destroy(gameObject);

        }
        else
        {
            DontDestroyOnLoad(transform.gameObject);

        }
        playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.maxValue = playerHealth.playerMaxHealth;
        healthBar.value = playerHealth.playerCurrentHealth;
        hpText.text = "HP: " + playerHealth.playerCurrentHealth + "/" + playerHealth.playerMaxHealth;
        levelText.text = "Lvl: " + playerStats.currentLevel;
    }
}

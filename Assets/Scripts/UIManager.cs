using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Animator healthBar;
    public Text levelText;
    public PlayerHealthManager playerHealth;
    private PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        //healthBar = GetComponent<Animator>();
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
        healthBar.SetFloat("Health", playerHealth.playerCurrentHealth);
        levelText.text = "Lvl: " + playerStats.currentLevel;
    }
}

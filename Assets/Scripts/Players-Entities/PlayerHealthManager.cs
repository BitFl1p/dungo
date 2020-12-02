using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{

    public int playerMaxHealth;
    public int playerCurrentHealth;
    private bool flashActive;
    public float flashLength;
    private float flashCount;
    private SpriteRenderer playerSprite;
    private SFXManager sfxMan;
    public bool invincible;
    int scene = 0;

    // Start is called before the first frame update
    void Start()
    {
        sfxMan = FindObjectOfType<SFXManager>();
        playerCurrentHealth = playerMaxHealth;
        playerSprite = GetComponent<SpriteRenderer>();
        //Debug.Log("Yeet");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex != scene)
        {
            SetMaxHealth();
        }
        scene = SceneManager.GetActiveScene().buildIndex;
        if (playerCurrentHealth <=0) 
        {
            sfxMan.SFX[2].Play();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            SetMaxHealth();

        }
        if (flashActive)
        {
            invincible = true;
            flashCount -= Time.deltaTime;
            playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b,1f);
            if (flashCount <= 0) { flashActive = false; }
            if(flashCount > flashLength * 0.66f) { playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0.5f); }
            else if(flashCount > flashLength * 0.33f) { playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f); }
            else if (flashCount > flashLength * 0f) { playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0.5f); }
            else
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
                invincible = false;
            }
        }
    }
    public void HurtPlayer(int damage)
    {
        if (!invincible)
        {
            playerCurrentHealth -= damage;
            flashActive = true;
            flashCount = flashLength;
            sfxMan.SFX[1].Play();
            invincible = true;
        }
        
        
    }

    public void SetMaxHealth() { playerCurrentHealth = playerMaxHealth; }
}

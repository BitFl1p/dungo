using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{

    public int playerMaxHealth;
    public int playerCurrentHealth;
    private bool flashActive;
    public float flashLength;
    private float flashCount;
    private SpriteRenderer playerSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        playerSprite = GetComponent<SpriteRenderer>();
        //Debug.Log("Yeet");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCurrentHealth <=0) 
        {
            gameObject.SetActive(false);

        }
        if (flashActive)
        {
            
            flashCount -= Time.deltaTime;
            playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b,1f);
            if (flashCount <= 0) { flashActive = false; }
            if(flashCount > flashLength * 0.66f) { playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0.5f); }
            else if(flashCount > flashLength * 0.33f) { playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f); }
            else if (flashCount > flashLength * 0f) { playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0.5f); }
            else
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }
        }
    }
    public void HurtPlayer(int damage)
    {
        playerCurrentHealth -= damage;
        flashActive = true;
        flashCount = flashLength;

    }

    public void SetMaxHealth() { playerCurrentHealth = playerMaxHealth; }
}

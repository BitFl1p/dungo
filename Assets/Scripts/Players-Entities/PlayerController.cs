﻿
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public GameObject dialogueButton;
    public GameObject inventoryButton;
    public Vector2 knockers;
    public float drag;
    private enum State
    {
        Normal, Attacking, Chattacking, Dashing
    }
    private float currentMoveSpeed;
    private Animator anim;
    public float speed;
    private bool playerMoving;
    public Vector2 lastMove;
    private Vector2 moveInput;
    private Rigidbody2D myRigidbody;
    private bool attacking;
    public float attackTime;
    private float attackTimeCounter;
    public string startPoint;
    public float diagonalMoveModifier;
    public bool canMove;
    [SerializeField] private SFXManager sfxMan;
    private Inventory inventory;
    private CraftableInventory craftInventory;
    [SerializeField] private Vector2 mousePos;
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private UICrafting uiCrafting;
    [SerializeField] private CraftingItems craftItems;
    public Camera theCamera;
    private float fifthHeight;
    private float fifthWidth;
    private State state = State.Normal;
    private bool containsWeapon;
    [HideInInspector] public float wepNum = 0;
    public float knockback;
    public int damage;
    public float dashDistance = 3f;
    public float dashMaxCount = 2; public float dashCount;
    public Vector2 lastDir;
    public float dashCooldown; public float dashCooldownCount;
    public GameObject dashEffect;
    public bool canDash;
    float chargeTime; float chargeClock = 0.25f; bool spriteWhite;
    private SpriteRenderer myRenderer;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;
    float timeSinceAttack;
    bool canChattack;
    [SerializeField] private Animator chargeFwoosh;
    bool fwooshPlayed = false;
    float charFwooshTimer = 0f;
    public bool canAttack = true;
    public PauseManager pauseMan;

    // Start is called before the first frame update
    private void Awake()
    {
        
        inventory = new Inventory(UseItem,this);
        craftInventory = new CraftableInventory(UseItem,this);
        uiInventory.SetInventory(inventory);
        uiCrafting.SetCraftInv(craftInventory);
        craftItems.SetCraftInv(craftInventory);
        craftItems.SetInv(inventory);
        uiCrafting.RefreshCraftables();
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Universal Render Pipeline/2D/Sprite-Lit-Default"); // or whatever sprite shader is being used
        state = State.Normal;
        fifthHeight = theCamera.orthographicSize / 3;
        fifthWidth = theCamera.orthographicSize * (Screen.width / Screen.height) / 3;
        lastMove = new Vector2(0f, -1f);
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        normalSprite();
        sfxMan = FindObjectOfType<SFXManager>();
        if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            Object.Destroy(gameObject);

        }
        else
        {
            DontDestroyOnLoad(transform.gameObject);

        } // check if player exists in the scene already or not
        canMove = true;

    }
    void Start()
    {
        CheckGear();
    }
    private void UseItem(Item item)
    {
        //switch (item.itemType)
        //{
            // gotta add some usability to my items but not today because FUCK THAT
            // inventory.RemoveItem(new Item { itemType = Item.ItemType.someShit, amount = 1 })
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (uiInventory.uIInventoryActive || pauseMan.paused)
        {
            canAttack = false;
        }
        else
        {
            canAttack = true;
        }
        sfxMan = FindObjectOfType<SFXManager>();
        
        if (canAttack)
        {
            timeSinceAttack += Time.deltaTime;
            if (Input.GetMouseButton(0) && timeSinceAttack >= 0.25)
            {
                anim.SetBool("PlayerAttacking", false);
                anim.SetBool("PlayerChattacking", false);
                canMove = false;
                chargeTime += Time.deltaTime;
                if (chargeTime > chargeClock)
                {
                    chargeClock += 0.25f;
                    if (spriteWhite)
                    {
                        normalSprite();
                        spriteWhite = false;

                    }
                    else
                    {
                        spriteWhite = true;
                        whiteSprite();
                    }
                }
            }


            if (chargeTime >= 1f && fwooshPlayed == false)
            {
                fwooshPlayed = true;
                chargeFwoosh.gameObject.SetActive(true);
            }
            if (chargeTime >= 1f && Input.GetMouseButtonUp(0) && canChattack)
            {
                fwooshPlayed = false;
                chargeTime = 0f;
                normalSprite();
                chargeClock = 0.25f;
                SetLastMoveToMouse();
                myRigidbody.velocity = Vector2.zero;
                anim.SetFloat("LastMoveX", lastMove.x);
                anim.SetFloat("LastMoveY", lastMove.y);
                attackTimeCounter = attackTime;
                state = State.Chattacking;
                sfxMan.SFX[0].Play();
                damage = 6;
                knockback = 6;
                myRigidbody.velocity = Vector2.zero;
                anim.SetBool("PlayerChattacking", true);
                timeSinceAttack = 0;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                fwooshPlayed = false;
                chargeTime = 0f;
                normalSprite();
                chargeClock = 0.5f;
                SetLastMoveToMouse();
                myRigidbody.velocity = Vector2.zero;
                anim.SetFloat("LastMoveX", lastMove.x);
                anim.SetFloat("LastMoveY", lastMove.y);

                attackTimeCounter = attackTime;
                state = State.Attacking;
                myRigidbody.velocity = Vector2.zero;
                anim.SetBool("PlayerAttacking", true);
                timeSinceAttack = 0;
                sfxMan.SFX[0].Play();
            }

        }
        
        if (!canMove) { myRigidbody.velocity = Vector2.zero; moveInput = Vector2.zero; anim.SetBool("PlayerMoving", false);return; }
        
        switch (state)
        {

            case State.Normal:

                anim.SetBool("PlayerAttacking", false);
                moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
                if (moveInput != Vector2.zero)
                {
                    playerMoving = true;
                    myRigidbody.velocity = new Vector2(moveInput.x * speed, moveInput.y * speed);
                    lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                }
                else { myRigidbody.velocity = Vector2.zero; }
                
                
                if (dashCooldownCount > 0) { dashCooldownCount -= Time.deltaTime; }
                if (Input.GetMouseButtonDown(1))
                {
                    if(dashCooldownCount <= 0&&canDash)
                    {
                        StartCoroutine(theCamera.GetComponentInParent<CameraShake>().Shake(0.15f,0.1f));
                        GameObject newDash = Instantiate(dashEffect);
                        newDash.GetComponent<ParticleSystem>().Play();
                        newDash.transform.position = transform.position;
                        SetLastMoveToMouse();
                        lastDir = lastMove;
                        dashCount = dashMaxCount;
                        state = State.Dashing;
                        myRigidbody.velocity = Vector2.zero; moveInput = Vector2.zero; anim.SetBool("PlayerMoving", false);
                    }
                    
                    return;
                }
                break;
            case State.Attacking:
                playerMoving = false;
                moveInput = Vector2.zero;
                if (attackTimeCounter > 0) { attackTimeCounter -= Time.deltaTime * 2; }
                if (attackTimeCounter <= 0) { state = State.Normal; anim.SetBool("PlayerAttacking", false); }
                anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
                anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
                break;
            case State.Dashing:

                if(dashCount > 0)
                {
                    dashCooldownCount = dashCooldown;
                    dashCount -= Time.deltaTime;
                    moveInput = lastDir.normalized * dashDistance;
                    
                }
                else if (dashCount <= 0)
                {
                    myRigidbody.velocity = Vector2.zero;
                    state = State.Normal;
                    canMove = true;
                }
                break;
            case State.Chattacking:
                playerMoving = false;
                moveInput = Vector2.zero;
                if (attackTimeCounter > 0) { attackTimeCounter -= Time.deltaTime * 2; }
                if (attackTimeCounter <= 0) { state = State.Normal; anim.SetBool("PlayerChattacking", false); }
                anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
                anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
                break;
        }


        //lastMove = new Vector2(mousePos.x,mousePos.y);


        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("PlayerMoving", playerMoving);


        playerMoving = false;

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();

        }
    }


    public void CheckGear()
    {
        containsWeapon = false;
        if (CheckForItem(Item.ItemType.RubyNecklace, 0))
        {
            canDash = true;
        }
        else
        {
            canDash = false;
        }
        foreach (Item item in inventory.GetItemList())
        {
            switch (item.itemType)
            {

                case Item.ItemType.EmbroidedSword:
                    {
                        if (wepNum <= 6)
                        {
                            canChattack = true;
                            knockback = 4;
                            damage = 3;
                            wepNum = 6;
                            containsWeapon = true;
                            anim.SetInteger("Weapon", 6);

                        }
                        break;
                    }
                case Item.ItemType.SilverSword:
                    {
                        if (wepNum <= 5)
                        {
                            canChattack = false;
                            knockback = 4;
                            damage = 3;
                            wepNum = 5;
                            containsWeapon = true;
                            anim.SetInteger("Weapon", 5);

                        }
                        break;
                    }
                case Item.ItemType.IronSword:
                    {
                        if (wepNum <= 4)
                        {
                            canChattack = false;
                            knockback = 3;
                            damage = 3;
                            wepNum = 4;
                            containsWeapon = true;
                            anim.SetInteger("Weapon", 4);

                        }
                        break;
                    }
                case Item.ItemType.RefinedWoodSword:
                    {
                        if (wepNum <= 3)
                        {
                            canChattack = false;
                            knockback = 3;
                            damage = 2;
                            wepNum = 3;
                            containsWeapon = true;
                            anim.SetInteger("Weapon", 3);

                        }
                        break;
                    }
                
                
                case Item.ItemType.ReinforcedWoodSword:
                    {
                        if (wepNum <= 2)
                        {
                            canChattack = false;
                            knockback = 3;
                            damage = 1;
                            wepNum = 2;
                            containsWeapon = true;
                            anim.SetInteger("Weapon", 2);

                        }
                        break;
                    }
                case Item.ItemType.WoodenSword:
                    {
                        if (wepNum <= 1)
                        {
                            canChattack = false;
                            knockback = 2;
                            damage = 1;
                            wepNum = 1;
                            containsWeapon = true;
                            anim.SetInteger("Weapon", 1);

                        }
                        break;
                    }
            }
        }
       
        
        if (!containsWeapon)
        {
            canChattack = false;
            knockback = 1;
            damage = 1;
            wepNum = 0;
            anim.SetInteger("Weapon", 0);
        }
    }
    public bool CheckForItem(Item.ItemType requiredItemType, int quantity)
    {
        foreach (Item item in inventory.GetItemList())
        {
            if (item.itemType == requiredItemType)
            {
                if (item.amount >= quantity)
                {
                    return true;
                }
            }

        }
        return false;
    }
    

    
    private void FixedUpdate()
    {
        

        myRigidbody.velocity = moveInput * speed + knockers;
        if(knockers.x > drag)
        {
            knockers.x -= drag;
        }
        else if (knockers.x < -drag)
        {
            knockers.x += drag;
        }
        else
        {
            knockers.x = 0;
        }
        if(knockers.y > 0)
        {
            knockers.y -= drag;
        }
        else if(knockers.y < -drag)
        {
            knockers.y += drag;
        }
        else
        {
            knockers.y = 0;
        }





    }
    
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    public void SetLastMoveToMouse()
    {
        Vector3 mousePos = GetMouseWorldPosition();


        if (mousePos.y > fifthHeight + transform.position.y)
        {
            lastMove.y = 1;
        }
        else if (mousePos.y < -fifthHeight + transform.position.y)
        {
            lastMove.y = -1;
        }
        else if (mousePos.y > -fifthHeight * 2 + transform.position.y && mousePos.y < fifthHeight * 2 + transform.position.y)
        {
            lastMove.y = 0;
        }

        if (mousePos.x > fifthWidth + transform.position.x)
        {
            lastMove.x = 1;
        }
        else if (mousePos.x < -fifthWidth + transform.position.x)
        {
            lastMove.x = -1;
        }
        else if (mousePos.x > -fifthWidth * 2 + transform.position.x && mousePos.x < fifthWidth * 2 + transform.position.x)
        {
            lastMove.x = 0;
        }
        if(lastMove.x == 0 && lastMove.y == 0)
        {
            lastMove.y = -1;
        }
    }
    void whiteSprite()
    {
        myRenderer.material.shader = shaderGUItext;
        myRenderer.color = Color.white;
    }
    void normalSprite()
    {
        myRenderer.material.shader = shaderSpritesDefault;
        myRenderer.color = Color.white;
    }
}

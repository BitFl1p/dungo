
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using UnityEditor.Events;
using UnityEngine;
using TMPro;



public class PlayerController : MonoBehaviour
{
    private enum State
    {
        Normal, Attacking, Dashing
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
    private SFXManager sfxMan;
    private Inventory inventory;
    private CraftableInventory craftInventory;
    [SerializeField] private Vector2 mousePos;
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private UICrafting uiCrafting;
    [SerializeField] private CraftingItems craftItems;
    public Camera theCamera;
    [SerializeField] private float fifthHeight;
    [SerializeField] private float fifthWidth;
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
    

    // Start is called before the first frame update
    private void Awake()
    {
        inventory = new Inventory(UseItem);
        craftInventory = new CraftableInventory(UseItem);
        uiInventory.SetInventory(inventory);
        uiCrafting.SetCraftInv(craftInventory);
        craftItems.SetCraftInv(craftInventory);
        craftItems.SetInv(inventory);
        uiCrafting.RefreshCraftables();

        state = State.Normal;
        fifthHeight = theCamera.orthographicSize / 3;
        fifthWidth = theCamera.orthographicSize * (Screen.width / Screen.height) / 3;
        lastMove = new Vector2(0f, -1f);
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();

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

    }
    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            // gotta add some usability to my items but not today because FUCK THAT
            // inventory.RemoveItem(new Item { itemType = Item.ItemType.someShit, amount = 1 })
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!canMove) { myRigidbody.velocity = Vector2.zero; moveInput = Vector2.zero; anim.SetBool("PlayerMoving", false); return; }
        checkWeapon();
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
                
                if (Input.GetMouseButtonDown(0))
                {
                    SetLastMoveToMouse();
                    myRigidbody.velocity = Vector2.zero;
                    anim.SetFloat("LastMoveX", lastMove.x);
                    anim.SetFloat("LastMoveY", lastMove.y);

                    attackTimeCounter = attackTime;
                    state = State.Attacking;
                    myRigidbody.velocity = Vector2.zero;
                    anim.SetBool("PlayerAttacking", true);

                    sfxMan.PlayerAttack.Play();



                    return;
                }
                if (dashCooldownCount > 0) { dashCooldownCount -= Time.deltaTime; }
                if (Input.GetMouseButtonDown(1))
                {
                    if(dashCooldownCount <= 0)
                    {
                        //StartCoroutine(theCamera.GetComponentInParent<CameraShake>().Shake(0.15f,1f));
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


    private void checkWeapon()
    {
        containsWeapon = false;
        if (CheckForItem(Item.ItemType.WoodenSword, 0) && wepNum <= 1)
        {
            knockback = 2;
            damage = 1;
            wepNum = 1;
            containsWeapon = true;
            anim.SetInteger("Weapon", 1);
        }
        else if (CheckForItem(Item.ItemType.ReinforcedWoodSword, 0) && wepNum <= 2)
        {
            knockback = 2;
            damage = 2;
            wepNum = 2;
            containsWeapon = true;
            anim.SetInteger("Weapon", 2);
        }
        else if (CheckForItem(Item.ItemType.RefinedWoodSword, 0) && wepNum <= 3)
        {
            knockback = 2;
            damage = 3;
            wepNum = 3;
            containsWeapon = true;
            anim.SetInteger("Weapon", 3);
        }
        else if (CheckForItem(Item.ItemType.IronSword, 0) && wepNum <= 4)
        {
            knockback = 3;
            damage = 3;
            wepNum = 4;
            containsWeapon = true;
            anim.SetInteger("Weapon", 4);
        }
        else if (CheckForItem(Item.ItemType.SilverSword, 0) && wepNum <= 5)
        {
            knockback = 3;
            damage = 4;
            wepNum = 5;
            containsWeapon = true;
            anim.SetInteger("Weapon", 5);
        }
        else if (!containsWeapon)
        {
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
        myRigidbody.velocity = moveInput * speed;
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
}

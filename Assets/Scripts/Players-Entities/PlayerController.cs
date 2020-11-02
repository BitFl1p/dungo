
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor.Events;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    private enum State
    {
        Normal, Attacking,
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
        fifthHeight = theCamera.orthographicSize/3;
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
        if (!canMove) { myRigidbody.velocity = Vector2.zero; return; }
        
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
                Vector3 mousePos = GetMouseWorldPosition();
                if (Input.GetMouseButtonDown(0))
                {
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
                break;
            case State.Attacking:
                playerMoving = false;
                moveInput = Vector2.zero;
                if (attackTimeCounter > 0) { attackTimeCounter -= Time.deltaTime * 2; }
                if (attackTimeCounter <= 0) { state = State.Normal; anim.SetBool("PlayerAttacking", false); }
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
        if(itemWorld != null)
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
            
        }
    }
    private void FixedUpdate()
    {
        myRigidbody.velocity = moveInput*speed;
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
}

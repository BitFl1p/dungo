using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

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
    [SerializeField] private UI_Inventory uiInventory;
    // Start is called before the first frame update
    private void Awake()
    {
        inventory = new Inventory(UseItem);
        uiInventory.SetInventory(inventory);
        
    }
    void Start()
    {
        lastMove = new Vector2(0f, -1f);
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        sfxMan = FindObjectOfType<SFXManager>();
        if (GameObject.FindGameObjectsWithTag("Player").Length > 1) {
            Object.Destroy(gameObject);

        } else {
            DontDestroyOnLoad(transform.gameObject);
            
        } // check if player exists in the scene already or not
        canMove = true;
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
        playerMoving = false;
        if (!canMove) { myRigidbody.velocity = Vector2.zero; return; }
        if (!attacking)
        {

            anim.SetBool("PlayerAttacking", false);



            /*if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) { playerMoving = true; lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); } // player movement
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f && Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f)
            {
                currentMoveSpeed = speed * diagonalMoveModifier;
            } else
            {
                currentMoveSpeed = speed;
            }
            myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * currentMoveSpeed, Input.GetAxisRaw("Vertical") * currentMoveSpeed);
            //transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime, 0f));
            */
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            if (moveInput != Vector2.zero)
            {
                playerMoving = true;
                myRigidbody.velocity = new Vector2(moveInput.x * speed, moveInput.y * speed);
                lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            }
            else { myRigidbody.velocity = Vector2.zero; }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                attackTimeCounter = attackTime;
                attacking = true;
                myRigidbody.velocity = Vector2.zero;
                anim.SetBool("PlayerAttacking", true);

                sfxMan.PlayerAttack.Play();

            }
        }
        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
        
        if (attackTimeCounter > 0) { attackTimeCounter -= Time.deltaTime * 2;}
        if (attackTimeCounter <= 0) { attacking = false; anim.SetBool("PlayerAttacking", false); }
        
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
}

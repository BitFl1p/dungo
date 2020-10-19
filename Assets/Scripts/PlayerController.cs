using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    private Animator anim;
    public float speed;
    private bool playerMoving;
    public Vector2 lastMove;
    private Rigidbody2D myRigidbody;
    private bool attacking;
    public float attackTime;
    private float attackTimeCounter;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        if (GameObject.FindGameObjectsWithTag("Player").Length > 1) {
            Object.Destroy(gameObject);

        } else {
            DontDestroyOnLoad(transform.gameObject);
            
        } // check if player exists in the scene already or not
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!attacking)
        {

            anim.SetBool("PlayerAttacking", false);


            playerMoving = false;
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) { playerMoving = true; lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); } // player movement
            myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);
            //transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime, 0f));
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                attackTimeCounter = attackTime;
                attacking = true;
                myRigidbody.velocity = Vector2.zero;
                anim.SetBool("PlayerAttacking", true);

            }
        }
        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
        
        if (attackTimeCounter > 0) { attackTimeCounter -= Time.deltaTime * 5;}
        if (attackTimeCounter <= 0) { attacking = false; anim.SetBool("PlayerAttacking", false); }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerMovement : MonoBehaviour
    

{

    public float moveSpeed;
    private Rigidbody2D myRB;
    private bool walking;
    public float walkTime;
    public float waitTime;
    private float walkCounter;
    private float waitCounter;

    private int walkDir;
    public Collider2D walkZone;
    private Vector2 minWalkPoint;
    private Vector2 maxWalkPoint;
    private bool hasWalkZone;
    public bool canMove;
    private DialogueManager theDM;
    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        canMove = true;
        myRB = GetComponent<Rigidbody2D>();
        waitCounter = waitTime;
        walkCounter = walkTime;
        ChooseDir();
        if (walkZone != null)
        {
            minWalkPoint = walkZone.bounds.min;
            maxWalkPoint = walkZone.bounds.max;
            hasWalkZone = true;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if (!theDM.dialogActive) { canMove = true; }
        if (!canMove){myRB.velocity = Vector2.zero; return; }
        if (walking)
        {
            walkCounter -= Time.deltaTime;
            
            switch (walkDir)
            {
                case 0:
                    myRB.velocity = new Vector2(0,moveSpeed);
                    if (hasWalkZone&&transform.position.y>maxWalkPoint.y) { walking = false; waitCounter = waitTime; }
                    break;
                case 1:
                    myRB.velocity = new Vector2(moveSpeed, 0);
                    if (hasWalkZone && transform.position.x > maxWalkPoint.x) { walking = false; waitCounter = waitTime; }
                    break;
                case 2:
                    myRB.velocity = new Vector2(0, -moveSpeed);
                    if (hasWalkZone && transform.position.y < minWalkPoint.y) { walking = false; waitCounter = waitTime; }
                    break;
                case 3:
                    myRB.velocity = new Vector2(-moveSpeed, 0);
                    if (hasWalkZone && transform.position.x < minWalkPoint.x) { walking = false; waitCounter = waitTime; }
                    break;
            }
            if (walkCounter <= 0) { walking = false; waitCounter = waitTime; }
        }
        else
        {
            myRB.velocity = Vector2.zero;
            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0) { ChooseDir(); }
        }
        
    }

    public void ChooseDir()
    {
        walkDir = Random.Range(0, 4);
        walking = true;
        walkCounter = walkTime;
    }
}

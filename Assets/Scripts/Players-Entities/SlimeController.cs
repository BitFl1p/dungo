using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlimeController : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D myRB;
    private bool moving;
    private float timeBetweenMoveCounter;
    public float timeBetweenMove;
    public float timeToMove;
    private float timeToMoveCounter;
    private Vector3 moveDir;
    public bool canMove = true;
    public CircleCollider2D view;
    //public float waitToReload;
    //private bool reloading;
    //private GameObject thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        timeBetweenMoveCounter = Random.Range(timeBetweenMove*0.75f, timeBetweenMove * 1.25f);
        timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
        canMove = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {

            if (moving)
            {
                timeToMoveCounter -= Time.deltaTime;
                myRB.velocity = moveDir;
                if (timeToMoveCounter <= 0f)
                {
                    myRB.velocity = Vector2.zero;
                    moving = false;
                    timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
                }
            }
            else
            {
                timeBetweenMoveCounter -= Time.deltaTime;

                if (timeBetweenMoveCounter <= 0f)
                {
                    moving = true;
                    timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
                    moveDir = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0f);
                }
            }
        }
        //if (reloading)
        //{
        //    waitToReload -= Time.deltaTime;
        //    if (waitToReload <= 0)
        //    {
        //        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //        thePlayer.SetActive(true);
        //    }
        //
        //}
    }

}

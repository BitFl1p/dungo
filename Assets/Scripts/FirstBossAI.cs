using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]

public class FirstBossAI : MonoBehaviour
{
    public Vector3[] locations;
    public int index = 0;
    public float speed;
    public Vector2 center;
    public float attackTime;
    private float attackTimeCount;
    bool attacking;
    public bool walkInEditMode = true;
    void Start()
    {
        attackTimeCount = attackTime;
        walkInEditMode = false;
    }
    void Update()
    {
        if (walkInEditMode)
        {
            for (int i = 0; i < locations.Length - 1; i++)
            {
                Debug.DrawLine(locations[i], locations[i + 1], Color.green);
            }
        }
        else
        {
            if (!attacking && attackTimeCount <= 0.7)
            {
                GetComponent<Animator>().SetBool("Attack", false);
                GetComponent<Animator>().SetBool("DownAttack", false);

            }
            if (!attacking && attackTimeCount <= 0.5)
            {
                GetComponent<Animator>().SetBool("AttackTwice", false);
            }
            attackTimeCount -= Time.deltaTime;
            if (transform.position.x >= center.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (transform.position.x >= locations[index].x - 1 && transform.position.x <= locations[index].x + 1 || transform.position.x >= locations[index].y - 1 && transform.position.x <= locations[index].y + 1)
            {
                index++;
            }
            else
            {
                transform.position += (locations[index] - transform.position).normalized * speed;
            }
            if (index >= locations.Length)
            {
                index = 0;
            }


            if (attackTimeCount <= 0)
            {
                attackTimeCount = 1;
                attacking = true;

            }
            if (attacking)
            {
                attackTimeCount -= Time.deltaTime;
                transform.position += (new Vector3(center.x, center.y) - transform.position).normalized * speed;
                if (attackTimeCount <= 0)
                {
                    switch (Random.Range(1, 4))
                    {
                        case 1:
                            GetComponent<Animator>().SetBool("Attack", true);
                            break;
                        case 2:
                            GetComponent<Animator>().SetBool("DownAttack", true);

                            break;
                        case 3:
                            GetComponent<Animator>().SetBool("Attack", true);
                            GetComponent<Animator>().SetBool("AttackTwice", true);
                            break;
                        case 4:
                            GetComponent<Animator>().SetBool("DownAttack", true);
                            GetComponent<Animator>().SetBool("AttackTwice", true);
                            break;
                    }
                    attacking = false;
                    attackTimeCount = attackTime;

                }
            }
        }

    }


}
//public UnityEngine.Transform target;
//    public float speed = 200f;
//    public float nextWaypointDistance = 3f;

//    Path path;
//    int currentWaypoint = 0;
//    bool reachedEndOfPath = false;
//    Seeker seeker;
//    Rigidbody2D rb;
//    float count;
//    bool stillCounting = true;
//    float max = 3;
//    bool attack = false;

//    // Start is called before the first frame update
//    void Start()
//    {
//        target = FindObjectOfType<PlayerController>().GetComponent<UnityEngine.Transform>();
//        seeker = GetComponent<Seeker>();
//        rb = GetComponent<Rigidbody2D>();


//    }
//    private void Update()
//    {
//        if (stillCounting) 
//        { 
//            count += Time.deltaTime; 
//            if (count <= max)
//            {
//                stillCounting = false;
//                count = 0;
//                attack = true;
//            } 
//        }
//        if (attack)
//        {
//            InvokeRepeating("UpdatePath", 0f, .5f);
//        }
//        if (rb.velocity.x < 0)
//        {
//            transform.localScale = new Vector3(-1, 1, 1);
//        }
//        else
//        {
//            transform.localScale = new Vector3(1, 1, 1);
//        }
//    }
//    void UpdatePath()
//    {


//        if (seeker.IsDone())
//        {
//            seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
//        }

//    }
//    // Update is called once per frame

//    void OnPathComplete(Path p)
//    {
//        if (!p.error)
//        {
//            path = p;
//            currentWaypoint = 0;
//        }
//    }

//    public void PlayerSeen()
//    {
//        if (path == null)
//        {
//            return;
//        }
//        if (currentWaypoint >= path.vectorPath.Count)
//        {
//            reachedEndOfPath = true;
//            return;
//        }
//        else
//        {
//            reachedEndOfPath = false;
//        }
//        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
//        Vector2 force = direction * speed * Time.deltaTime;

//        rb.AddForce(force);
//        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
//        if (distance < nextWaypointDistance)
//        {
//            currentWaypoint++;
//        }
//    }
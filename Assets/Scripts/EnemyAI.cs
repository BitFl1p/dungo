using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyAI : MonoBehaviour
{
    public UnityEngine.Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public bool slidy;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    Rigidbody2D rb;
    bool leftLast; bool rightLast;
    
    
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().GetComponent<UnityEngine.Transform>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        
        InvokeRepeating("UpdatePath", 0f, .5f);
    }
    private void Update()
    {
        if(rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1,1,1);
            leftLast = true;
            rightLast = false;
        }
        else if (rb.velocity.x > 0)
        {

            transform.localScale = new Vector3(1, 1, 1);
            rightLast = true;
            leftLast = false;
        }
        else if (rb.velocity.x == 0)
        {
            if (leftLast)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (rightLast)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        
    }
    void UpdatePath()
    {
        

        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
        }

    }
    // Update is called once per frame
    
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    
    public void PlayerSeen()
    {
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        if (slidy)
        {
            rb.AddForce(force);
        }
        else
        {
            rb.velocity = force;
        }
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
    
            
        
}

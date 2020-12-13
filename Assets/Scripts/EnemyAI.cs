using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyAI : MonoBehaviour
{
    public float drag;
    public Vector2 knockback;
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
    public bool seen = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().GetComponent<UnityEngine.Transform>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        
        InvokeRepeating("UpdatePath", 0f, .5f);
    }
    
    void UpdatePath()
    {
        if (seen)
        {
            if (seeker.IsDone())
            {
                seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
            }
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
    void Update()
    {
        if (rb.velocity.x - knockback.x < 0)
        {
            
            transform.localScale = new Vector3(-1, 1, 1);
            leftLast = true;
            rightLast = false;
        }
        else if (rb.velocity.x + knockback.x > 0)
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

    public void PlayerSeen()
    {
        seen = true;
        
        

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
            rb.AddForce(force+knockback);
        }
        else
        {
            rb.velocity = force+knockback;
            rb.AddForce((force + knockback) / 10);
        }
        if (knockback.x > drag)
        {
            knockback.x -= drag;
        }
        else if (knockback.x < -drag)
        {
            knockback.x += drag;
        }
        else
        {
            knockback.x = 0;
        }
        if (knockback.y > 0)
        {
            knockback.y -= drag;
        }
        else if (knockback.y < -drag)
        {
            knockback.y += drag;
        }
        else
        {
            knockback.y = 0;
        }
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
    
            
        
}

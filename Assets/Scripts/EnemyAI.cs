using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyAI : MonoBehaviour
{
    public UnityEngine.Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    Rigidbody2D rb;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        

        if (path == null)
        {
            return;
        }
        if(currentWaypoint>= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } else
        {
            reachedEndOfPath = false;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        
        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Swing")
        {
            Vector3 force = new Vector3(transform.position.x - other.transform.position.x, transform.position.y - other.transform.position.y).normalized * other.GetComponent<HurtEnemy>().knockback;

            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMovement : MonoBehaviour
{
    public UnityEngine.Transform player;
    public Vector3 offset;
    private Rigidbody2D myRB;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myRB.velocity = (player.transform.position+offset - transform.position)*moveSpeed;
        if (transform.position.x - player.transform.position.x >= 5 || transform.position.y - player.transform.position.y >= 5 || transform.position.x - player.transform.position.x <= -5 || transform.position.y - player.transform.position.y <= -5)
        {
            transform.position = player.transform.position+offset;
        }
        
    }
}

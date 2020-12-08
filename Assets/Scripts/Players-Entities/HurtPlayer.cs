using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public int damage;
    
    public float knockback = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "Player")
        {
            other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damage);
            other.gameObject.GetComponent<PlayerController>().knocked = true;


            Vector3 direction = (other.transform.position - transform.position) * knockback * 10;
            direction.Normalize();
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(direction, ForceMode2D.Impulse);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damage);
            other.gameObject.GetComponent<PlayerController>().knocked = true;


            Vector3 direction = (other.transform.position - transform.position) * knockback * 10;
            direction.Normalize();
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(direction, ForceMode2D.Impulse);

        }
    }
}

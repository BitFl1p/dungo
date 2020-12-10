using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayerCollision : MonoBehaviour
{
    public int damage;
    
    public float knockback = 1;

    // Start is called before the first frame update

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "Player")
        {
            other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damage);
            


            Vector3 direction = ((other.transform.position - transform.position) * knockback).normalized * 50;
            
            other.gameObject.GetComponent<PlayerController>().knockers = direction;

        }
    }

}

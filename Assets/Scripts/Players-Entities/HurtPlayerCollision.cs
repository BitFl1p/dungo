using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayerCollision : MonoBehaviour
{
    public int damage;
    
    public float knockback = 1;
    public float knockCoefficient = 10;

    // Start is called before the first frame update

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "Player")
        {
            other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damage);
            other.gameObject.GetComponent<PlayerController>().knockers = new Vector2(other.transform.position.x - transform.position.x, other.transform.position.y - transform.position.y).normalized * knockback * knockCoefficient;
        }
    }

}

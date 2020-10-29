using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public int damage;
    public GameObject damageNumber;
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
            var clone = Instantiate(damageNumber, other.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().damage = damage;
            Vector3 direction = (other.transform.position - transform.position) * knockback * 10;
            direction.Normalize();
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(direction, ForceMode2D.Impulse);
            
        }
    }
}

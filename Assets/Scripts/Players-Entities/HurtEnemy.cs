using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public int damage;
    public GameObject damageBurst;
    public Transform hitPoint;
    public GameObject damageNumber;
    public float knockback = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy") 
        {

            //other.gameObject.SetActive(false); 
            other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damage);
            Instantiate(damageBurst, hitPoint.transform.position, hitPoint.transform.rotation);
            var clone = Instantiate(damageNumber, hitPoint.transform.position, Quaternion.Euler (Vector3.zero));
            clone.GetComponent<FloatingNumbers>().damage = damage;
            Vector3 direction = (other.transform.position - transform.position) * knockback * 10;
            direction.Normalize();
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(direction, ForceMode2D.Impulse);
            other.gameObject.GetComponent<SlimeController>().canMove = true;


        }
    }

}

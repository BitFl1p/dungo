﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public int damage;
    public GameObject damageBurst;
    public UnityEngine.Transform hitPoint;
    public PlayerController thePlayer;
    public float knockback;
    public float knockCoefficient = 10;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy") 
        {
            
            knockback = thePlayer.knockback;
            damage = thePlayer.damage;
            //other.gameObject.SetActive(false); 
            other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(thePlayer.damage);
            Instantiate(damageBurst, hitPoint.transform.position, hitPoint.transform.rotation);
            other.GetComponent<EnemyAI>().knockback = new Vector2(other.transform.position.x - transform.position.x, other.transform.position.y - transform.position.y).normalized * knockback*knockCoefficient;
            



        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ImGonnaExplodeAndMurdalizeYouBitch : MonoBehaviour
{
    public GameObject explosion;
    public float speed = 5;
    public GameObject player;
    Vector2 direction;
    public float count;
    public float lifespan = 1;
    void Awake()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        count = 0;
    }
    void Update()
    {
        count += Time.deltaTime;
        if(count >= lifespan)
        {
            Instantiate(explosion).transform.position = transform.position;
            Destroy(gameObject);
        }
        else
        {
            Vector3 diff = (player.transform.position - transform.position);
            float angle = Mathf.Atan2(diff.y, diff.x);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg), .1f);
            transform.position += transform.right * speed * Time.deltaTime;
        }
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Instantiate(explosion).transform.position = player.transform.position;
            Destroy(gameObject);
        }
    }
    public Vector2 VectorFromAngle(float theta)
    {
        return new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)); // Trig is fun
    }
}

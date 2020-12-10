using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBossAi : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3[] locations;
    public int index = 0;
    public float speed;
    public Vector2 center;
    public float attackTime;
    private float attackTimeCount;
    bool attacking;
    public bool walkInEditMode = true;
    void Start()
    {
        attackTimeCount = attackTime;
        walkInEditMode = false;
    }
    void Update()
    {
        if (walkInEditMode)
        {
            for (int i = 0; i < locations.Length - 1; i++)
            {
                Debug.DrawLine(locations[i], locations[i + 1], Color.green);
            }
        }
        else
        {
            if (!attacking && attackTimeCount <= 0.7)
            {
                GetComponent<Animator>().SetBool("Attack", false);
                GetComponent<Animator>().SetBool("DownAttack", false);

            }
            if (!attacking && attackTimeCount <= 0.5)
            {
                GetComponent<Animator>().SetBool("AttackTwice", false);
            }
            attackTimeCount -= Time.deltaTime;
            if (transform.position.x >= center.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (transform.position.x >= locations[index].x - 1 && transform.position.x <= locations[index].x + 1 || transform.position.x >= locations[index].y - 1 && transform.position.x <= locations[index].y + 1)
            {
                index++;
            }
            else
            {
                if (!attacking)
                {
                    transform.position += (locations[index] - transform.position).normalized * speed;
                }
                
            }
            if (index >= locations.Length)
            {
                index = 0;
            }


            if (attackTimeCount <= 0)
            {
                attackTimeCount = 1;
                attacking = true;

            }
            if (attacking)
            {
                attackTimeCount -= Time.deltaTime;
                transform.position += (new Vector3(center.x, center.y) - transform.position).normalized * speed;
                if (attackTimeCount <= 0)
                {
                    switch (Random.Range(1, 4))
                    {
                        case 1:
                            GetComponent<Animator>().SetBool("Attack", true);
                            break;
                        case 2:
                            GetComponent<Animator>().SetBool("DownAttack", true);

                            break;
                        case 3:
                            GetComponent<Animator>().SetBool("Attack", true);
                            GetComponent<Animator>().SetBool("AttackTwice", true);
                            break;
                        case 4:
                            GetComponent<Animator>().SetBool("DownAttack", true);
                            GetComponent<Animator>().SetBool("AttackTwice", true);
                            break;
                    }
                    attacking = false;
                    attackTimeCount = attackTime;

                }
            }
        }

    }

}

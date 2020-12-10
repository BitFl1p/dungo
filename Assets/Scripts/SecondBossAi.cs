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
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
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
            if (attacking && attackTimeCount <= 0.7)
            {
                anim.SetBool("Attack", false);
                anim.SetBool("DownAttack", false);

            }
            if (attacking && attackTimeCount <= 0.5)
            {
                anim.SetBool("AttackTwice", false);
                
            }
            attackTimeCount -= Time.deltaTime;
            if (transform.position.x >= locations[index].x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (transform.position.x >= locations[index].x - 1 && transform.position.x <= locations[index].x + 1 || transform.position.x >= locations[index].y - 1 && transform.position.x <= locations[index].y + 1)
            {
                if(index == 0)
                {
                    index++;
                }
                if(Random.Range(1,3) == 1)
                {
                    index++;
                }
                else
                {
                    index--;
                }
                
            }
            
                if (!attacking)
                {
                    transform.position += (locations[index] - transform.position).normalized * speed * Time.deltaTime;
                    anim.SetBool("Moving", true);
                }
                else
                {
                    anim.SetBool("Moving", false);

                }
                
            
            if (index >= locations.Length)
            {
                index = 0;
            }


            if (attackTimeCount <= 0 && !attacking)
            {
                attacking = true;
                attackTimeCount = 1;
                

            }
            if (attackTimeCount <= 0 && attacking)
            {
                attacking = false;
                attackTimeCount = attackTime;
                

            }
            if (attacking)
            {
                attackTimeCount -= Time.deltaTime;
                
                if (attackTimeCount <= 0)
                {
                    switch (Random.Range(1, 4))
                    {
                        case 1:
                            anim.SetBool("Attack", true);
                            break;
                        case 2:
                            anim.SetBool("DownAttack", true);

                            break;
                        case 3:
                            anim.SetBool("Attack", true);
                            anim.SetBool("AttackTwice", true);
                            break;
                        case 4:
                            anim.SetBool("DownAttack", true);
                            anim.SetBool("AttackTwice", true);
                            break;
                    }
                    
                    attackTimeCount = attackTime;

                }
            }
        }

    }

}

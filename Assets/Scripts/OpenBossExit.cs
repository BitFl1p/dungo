using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBossExit : MonoBehaviour
{
    private EnemyHealthManager myHP;
    [SerializeField]public Collider2D hello;
    // Start is called before the first frame update
    void Start()
    {
        myHP = GetComponent<EnemyHealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(myHP.enemyCurrentHealth <= 0)
        {
            //ssdasdasdasdassdffhdedfhfddfhasdfsdf
            hello.isTrigger = true;


        }
        else
        {
            hello.isTrigger = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    private EnemyAI enemyAI;
    // Start is called before the first frame update
    void Start()
    {
        enemyAI = GetComponentInParent<EnemyAI>();
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            enemyAI.PlayerSeen();
        }
    }
}

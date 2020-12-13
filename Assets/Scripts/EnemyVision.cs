using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    private EnemyAI enemyAI;
    private PinguAI pinguAI;
    // Start is called before the first frame update
    void Start()
    {
        enemyAI = GetComponentInParent<EnemyAI>();
        pinguAI = GetComponentInParent<PinguAI>();
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (enemyAI != null)
            {
                enemyAI.PlayerSeen();
                enemyAI.seen = true;
            }
            if(pinguAI != null)
            {
                pinguAI.PlayerSeen();
                pinguAI.seen = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (enemyAI != null)
            {
                enemyAI.seen = false;
            }
            if (pinguAI != null)
            {
                pinguAI.seen = false;
            }
        }
    }
}

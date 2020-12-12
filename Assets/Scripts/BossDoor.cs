using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject boss;
    public SpriteRenderer exit;
    void Update()
    {
        if(boss == null)
        {
            exit.enabled = true;
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else
        {
            exit.enabled = false;
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}

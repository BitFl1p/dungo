using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class RenderDistance : MonoBehaviour
{
    Light2D me;
    
    // Start is called before the first frame update
    void Start()
    {
        me = GetComponent<Light2D>(); 
        me.enabled = false;
    }

    // Update is called once per frame
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "RenderDistance")
        {
            me.enabled = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "RenderDistance")
        {
            me.enabled = false;
        }
    }
}

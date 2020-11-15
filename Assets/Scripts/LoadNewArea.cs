using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour
{
    public int levelToLoad;
    public string exitPoint;
    private PlayerController thePlayer;

    void Start()
    {

        thePlayer = FindObjectOfType<PlayerController>();


    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.name == "Player") 
        { 
            GameObject transition = GameObject.FindGameObjectWithTag("Transition");
            transition.GetComponent<Animator>().Play("Transition_Start");
            transition.GetComponent<AnimationEvents>().levelToLoad = levelToLoad;
        }
        thePlayer.startPoint = exitPoint;
    }
}

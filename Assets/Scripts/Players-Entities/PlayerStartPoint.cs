using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour
{
    private PlayerController thePlayer;
    private CameraController theCamera;

    public Vector2 startDirection;
    public string pointName;
    
    // Start is called before the first frame update
    void Start()
    {
        theCamera = FindObjectOfType<CameraController>();
        thePlayer = FindObjectOfType<PlayerController>();
        if (thePlayer.startPoint == pointName)
        {

            
            thePlayer.lastMove = startDirection;
            thePlayer.transform.position = new Vector3(transform.position.x, transform.position.y, thePlayer.transform.position.z);
            theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z);
        }
    }


    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour
{
    private PlayerController thePlayer;
    private CameraController theCamera;

    public Vector2 startDirection;
    // Start is called before the first frame update
    void Start()
    {
        
        thePlayer = FindObjectOfType<PlayerController>();
        theCamera = FindObjectOfType<CameraController>();
        thePlayer.lastMove = startDirection;
        thePlayer.transform.position = new Vector3(transform.position.x, transform.position.y, thePlayer.transform.position.z);
        theCamera.transform.position = new Vector3 (transform.position.x,transform.position.y, theCamera.transform.position.z);
    }

    // Update is called once per frame
    
}

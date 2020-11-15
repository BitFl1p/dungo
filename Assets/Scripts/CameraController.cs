using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CameraController : MonoBehaviour
{
    public GameObject followTarget;
    private Vector3 targetPosition;
    public float moveSpeed;
    public BoxCollider2D boundBox;
    private Vector3 minBounds;
    private Vector3 maxBounds;

    private Camera theCamera;
    private float halfHeight;
    private float halfWidth;
    private bool boundsExist;


    // Start is called before the first frame update
    void Start()
    {
        

        if (boundBox = null) { boundsExist = false; }
        else { boundsExist = true; }
        if (boundsExist)
        {
            boundBox = FindObjectOfType<Bounds>().GetComponent<BoxCollider2D>();
            minBounds = boundBox.bounds.min;
            maxBounds = boundBox.bounds.max;
        }
       
        theCamera = GetComponent<Camera>();
        
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * (Screen.width / Screen.height);
        //GetComponent<PixelPerfectCamera>().refResolutionX = Screen.width/2;
        //GetComponent<PixelPerfectCamera>().refResolutionY = Screen.height/2;
    }

    // Update is called once per frame
    void Update()
    {
        if (boundBox = null) { boundsExist = false; }
        else { boundsExist = true; }
        targetPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if (boundsExist)
        {
            
            minBounds = boundBox.bounds.min;
            maxBounds = boundBox.bounds.max;
            float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
            float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        } 
        
        
    }
    public void SetBounds(BoxCollider2D newBounds)
    {
        boundBox = newBounds;
        minBounds = boundBox.bounds.min;
        maxBounds = boundBox.bounds.max;
    }
}

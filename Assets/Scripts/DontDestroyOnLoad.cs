using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public string myTag;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag(myTag).Length > 1)
        {
            Object.Destroy(gameObject);

        }
        else
        {
            DontDestroyOnLoad(gameObject);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

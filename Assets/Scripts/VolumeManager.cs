using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    public VolumeController[] vcObjects;
    public float maxVolumeLevel = 1f;
    public float currentVolumeLevel;
    // Start is called before the first frame update
    void Start()
    {
        vcObjects = FindObjectsOfType<VolumeController>();
        if(currentVolumeLevel > maxVolumeLevel) { currentVolumeLevel = maxVolumeLevel; }
        for(int i = 1; i < vcObjects.Length; i++)
        {
            vcObjects[i].SetAudioLevel(currentVolumeLevel);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

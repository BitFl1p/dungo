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
    }

    // Update is called once per frame
    void Update()
    {
       
        if (currentVolumeLevel > maxVolumeLevel) { currentVolumeLevel = maxVolumeLevel; }
        foreach (VolumeController vCon in vcObjects)
        {
            vCon.SetAudioLevel(currentVolumeLevel);
            
        }
    }
}

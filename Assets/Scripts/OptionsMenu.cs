using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    private VolumeManager vMan;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private MainMenu menu;
    // Start is called before the first frame update
    private void Start()
    {
        vMan = FindObjectOfType<VolumeManager>();
    }
    public void Back()
    {
        menu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    public void Volume()
    {
        vMan.currentVolumeLevel = volumeSlider.value;
    }
}

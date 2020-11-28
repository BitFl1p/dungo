using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private OptionsMenu options;
    [SerializeField] private PauseManager pauseMan;
    [SerializeField] private Animator transition;

    public void PlayGame()
    {
        transition.Play("Transition_Start");
        
    }
    public void OpenOptions()
    {
        options.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Resume()
    {
        pauseMan.paused = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AnimationEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void TurnSelfOff()
    {
        gameObject.SetActive(false);
    }
    void DestroySelf()
    {
        Destroy(gameObject);
    }
    void TurnOnPet()
    {
        FindObjectOfType<PetMovement>().enabled = true;
    }
    public AudioSource FirstSound;
    public AudioSource SecondSound;
    public MusicController mCon;
    void PlayFirstSound()
    {
        FirstSound.Play();
    }
    void PlaySecondSound()
    {
        SecondSound.Play();
        mCon.musicCanPlay = true;
        mCon.currentTrack = 0;
    }
    public int levelToLoad; public LoadNewArea levelLoader; public Animator transition; public Canvas canvas;
    void Transition()
    {
        
        if (levelLoader == null)
        {
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            SceneManager.LoadScene(levelLoader.levelToLoad);
        }
        canvas.worldCamera = FindObjectOfType<Camera>();
        transition.Play("Transition_End");
    }
    public GameObject globalVolume;
    void RemovePost()
    {

        globalVolume.SetActive(false);

        
    }
}

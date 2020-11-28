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
    public GameObject poof;
    void PlayFirstSound()
    {
        FirstSound.Play();
        GameObject poof1 = Instantiate(poof);
        poof1.transform.position = new Vector3(7.333764f, 0f, 0f);
        GameObject poof2 = Instantiate(poof);
        poof2.transform.position = new Vector3(7.333764f, 3.595856f, 0f);
        GameObject poof3 = Instantiate(poof);
        poof3.transform.position = new Vector3(7.333764f, -3.218905f, 0f);
        GameObject poof4 = Instantiate(poof);
        poof4.transform.position = new Vector3(-7.399622f, 0f, 0f);
        poof4.transform.localScale = new Vector3(-1, 1, 1);
        GameObject poof5 = Instantiate(poof);
        poof5.transform.position = new Vector3(-7.399622f, 3.595856f, 0f);
        poof5.transform.localScale = new Vector3(-1, 1, 1);
        GameObject poof6 = Instantiate(poof);
        poof6.transform.position = new Vector3(-7.399622f, -3.218905f, 0f);
        poof6.transform.localScale = new Vector3(-1, 1, 1);
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

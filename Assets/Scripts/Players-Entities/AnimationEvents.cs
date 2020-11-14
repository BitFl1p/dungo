using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}

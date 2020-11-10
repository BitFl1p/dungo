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
}

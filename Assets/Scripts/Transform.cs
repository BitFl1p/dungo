using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transform : MonoBehaviour
{
    public Animator anim;
    public DialogueHolder dHold;
    public DialogueManager dMan;
    [SerializeField] private float count;
    [SerializeField]private GameObject buddy;
    bool start = false; bool go = false;
    [SerializeField] private float animCount;

    // Start is called before the first frame update
    void Start()
    {
        count = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (start&&go)
        {
            anim.Play("BuddyTransform");
            animCount = 0.5f ;
            start = false;
        }
        else if(go&&!start)
        {
            if (animCount > 0)
            {
                animCount -= Time.deltaTime;
            }
            else
            {
                buddy.SetActive(true);
                buddy.transform.position = transform.position;
                buddy.GetComponent<PetMovement>().petNum = 0;
                Destroy(gameObject);
            }

        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (count > 0 && dMan.dialogActive)
        {
            count -= Time.deltaTime;
            return;
        }

        if (!dMan.dialogActive && count <= 0)
        {
            start = true;
            go = true;






        }

    }
}

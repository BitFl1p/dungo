using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    public string dialogue;
    private DialogueManager dMan;
    public string[] dialogueLines;
    // Start is called before the first frame update
    void Start()
    {
        dMan = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!dMan.dialogActive)
                {
                    dMan.dialogLines = dialogueLines;
                    dMan.currentLine = 0;
                    dMan.ShowDialogue();
                }
                if (transform.parent.GetComponent<VillagerMovement>() != null)
                {
                    transform.parent.GetComponent<VillagerMovement>().canMove = false;
                }
            }
            else
            {
                other.GetComponent<PlayerController>().dialogueButton.SetActive(true);
            }
                
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            other.GetComponent<PlayerController>().dialogueButton.SetActive(false);
        }
    }
}

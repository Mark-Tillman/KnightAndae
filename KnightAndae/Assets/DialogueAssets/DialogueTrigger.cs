using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue; //Dialogue for the NPC
    bool canTalk = false;
    public GameObject talkPrompt; //Reference to "Press 'E' to Talk" image

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(canTalk);
        if(canTalk && Input.GetKeyDown(KeyCode.E)) //If within range and press E, start dialogue.
        {
            talkPrompt.SetActive(false);
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            talkPrompt.SetActive(true);
            canTalk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canTalk = false;
            talkPrompt.SetActive(false);
        }
    }
}

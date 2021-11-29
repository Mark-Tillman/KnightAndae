using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText; //Reference the name text in the dialogue box
    public Text dialogueText; //Reference the dialogue text in the dialogue box
    public Animator animator; //Reference the dialogue box animator
    private Queue<string> sentences; //A queue to hold all of the sentences
    public AudioSource audiosource;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>(); //Create a queue for the sentences
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true); //Triggers animator to bring dialogue box onto the screen
        nameText.text = dialogue.name; //Set the name of the NPC in the dialogue box to the name assigned to the NPC
        sentences.Clear(); //Start by clearing all current dialogue in the queue

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence); //Add each sentence to the queue
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return; //End Dialogue if there are no more sentences
        }

        string sentence = sentences.Dequeue(); //Remove the last sentence displayed from the queue
        StopAllCoroutines(); //Stop displaying characters if not done already
        StartCoroutine(TypeSentence(sentence)); //Start displaying characters of the next sentence
        
    }

     IEnumerator TypeSentence(string sentence)
     {
        audiosource.Play();
        dialogueText.text = ""; //Start with just an empty string
        foreach (char letter in sentence.ToCharArray()) //Add each character to the dialogue box at the specified rate
        {
            
            dialogueText.text += letter; 
            yield return new WaitForSeconds(0.02f); //Rate to display characters;
        }
        audiosource.Stop();
     }

    void EndDialogue()
    {
        //Debug.Log("End of Conversation.");
        animator.SetBool("isOpen", false); //Trigger animation to remove dialogue from screen.
    }
}

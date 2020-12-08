using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log(dialogue.name);
        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();

    }

    public bool DisplayNextSentence()
    {
       
        string sentence=sentences.Dequeue();
        Debug.Log(sentence);

        if (sentences.Count == 0)
        {

            return EndDialogue();
        }

        return true;

    }

    bool EndDialogue()
    {
        return false;
    }
  
}

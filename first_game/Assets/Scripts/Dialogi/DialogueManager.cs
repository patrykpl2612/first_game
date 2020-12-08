using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
        dialogueText.name = "jbcPis";
            
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log(dialogue.name);
        nameText.text = dialogueText.name;
        PlayerControls.IsInputEnabled = false;

        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();

    }

    public bool DisplayNextSentence()
    {
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;

        if (sentences.Count == 0)
        {       
            return EndDialogue();
        }
        else
        {
            return true;
        }      
    }

    bool EndDialogue()
    {
        PlayerControls.IsInputEnabled = true;
        return false;
    }
  
}


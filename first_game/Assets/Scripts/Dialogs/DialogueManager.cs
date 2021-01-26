using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    private Queue<string> sentences;
    private GameObject NPC;
    void Start()
    {
        sentences = new Queue<string>();

            
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("DialogueOpen", true);
        nameText.text = dialogue.name;
        NPC = GameObject.Find(dialogue.name);
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
        //dialogueText.text = sentence;

        if (sentences.Count == 0)
        {
            
                return EndDialogue();
            
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentance(sentence));
            return true;
        }      
    }
    IEnumerator TypeSentance(string sentance) // Wyswietla dialog literka po literce
    {

        dialogueText.text = "";
        foreach (char letter in sentance.ToCharArray())
        {
            dialogueText.text += char.ToUpper(letter);

            for (int i = 0; i < 2; i++)
            {
                yield return null;
            }
            try
            {
                NPC.GetComponent<NPC>().PlayLetterSound();
            }
            catch
            {
                Debug.Log("dupa");
            }

        }
    }
    bool EndDialogue()
    {
            animator.SetBool("DialogueOpen", false);
            PlayerControls.IsInputEnabled = true;
            return false;
    }
  
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{


    public Dialogue dialogue;
    public GameObject Player;
    private bool talking;



    void Start()
    {
        Player = GameObject.Find("/Player");
        talking = false;
    }

    void Update()
    {
        
        if (talking == true && Input.GetButtonDown("Talk"))
        {
            talking = FindObjectOfType<DialogueManager>().DisplayNextSentence();
            
        }
        else if (Input.GetButtonDown("Talk") && Vector3.Distance(transform.position, Player.transform.position) < 4f)
        {
            
            TriggerDialogue();
            talking = true;
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}//https://www.youtube.com/watch?v=_nRzoTzeyxU&feature=emb_title
   
   



        

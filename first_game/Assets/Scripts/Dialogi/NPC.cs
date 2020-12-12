using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{


    public Dialogue dialogue;
    public GameObject Player;
    public AudioSource audioSource;
    private bool talking;
    private Vector3 LastPlayerPos;
    private Vector3 NewPlayerPos;
    private bool GoingToLocation = false;
    private Vector3 Location;

    public bool Follower = false; // 
    private bool Following = false;
    public float FollowingDistance = 3f;
    public int Fastness = 2;

    void Start()
    {
        Player = GameObject.Find("/Player");
        talking = false;
        LastPlayerPos = Player.transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) <= 4f)// sprawdzanie czy gracz jest wystarczajaco blisko by pogadac z npc
        {
            PlayerControls.InReachOfNpc = true;
        }                                           
        else
        {
            PlayerControls.InReachOfNpc = false;
        }

        if (Time.timeScale == 1f)
        {
            if (talking == true && Input.GetButtonDown("Talk"))
            {
                talking = FindObjectOfType<DialogueManager>().DisplayNextSentence();
                if (talking == false && Follower)
                {
                    LastPlayerPos = Player.transform.position;
                    Following = !Following;
                }
            }
            else if (Input.GetButtonDown("Talk") && Vector3.Distance(transform.position, Player.transform.position) < 4f)
            {
                TriggerDialogue();
                talking = true;
            }
        }
        

        if (Following)
        {
            if (Vector3.Distance(NewPlayerPos, LastPlayerPos) >= 4f)
            {
                Location = LastPlayerPos - transform.position;
                GoingToLocation = true;
                LastPlayerPos = NewPlayerPos;
            }

            NewPlayerPos = Player.transform.position;
            
            if (GoingToLocation)
            {

                if (Vector3.Distance(Location, transform.position) >= FollowingDistance)
                {
                    float x = 0;
                    float y = 0;
                    if (Location.x > 1)
                    {
                        x = 0.1f;
                    }
                    
                    if (Location.x < -1)
                    {
                        x = -0.1f;
                    }

                    if (Location.y > 1)
                    {
                        y = 0.1f;
                    }

                    if (Location.y < -1)
                    {
                        y = -0.1f;
                    }

                    x *= Fastness;
                    y *= Fastness;

                    Location = LastPlayerPos - transform.position;

                    transform.Translate(x,y,0f);
                }
                else
                {
                    GoingToLocation = false;
                }
            }
        }

        

    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
    public void PlayLetterSound()
    {
        audioSource.Play();
    }
}
   
   



        

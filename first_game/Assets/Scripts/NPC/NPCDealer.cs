using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDealer : MonoBehaviour
{
    [SerializeField] private UI_Inventory uiInventory;

    public float smooth = 1f;
    public Animator animator;
    private Quaternion targetRotation;
    private Inventory inventory;
    public GameObject inventoryObject;
    

    public int x = 1;
    //private int tempX = 1;
    // Start is called before the first frame update


    public GameObject ThePlayer;
    public PlayerControls player;
    public Transform playerTransform;
    public float Radius;
    private ItemWorld itemWorld;


    //do testow--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public Dialogue dialogue;
    public GameObject Player;
    public AudioSource audioSource;
    private bool talking;
    private Vector3 LastPlayerPos;
    private Vector3 NewPlayerPos;
    private Vector3 Location;
    //koniec testow--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void Start()
    {
        talking = false;
    }

    private int GetPrice(string itemType)
    {
        switch (itemType)
        {
            default:
            case "HealthPotion":    return 1;
            case "Coin":            return 1;
            case "Muszla1":         return 3;
            case "Muszla2":         return 4;
            case "Muszla3":         return 5;
            case "Muszla4":         return 6;
            case "Muszla5":         return 7;
            case "SodaCane":        return 8000;
        }
    }

    IEnumerator takeItem()
    {
        int priceForItem = GetPrice(itemWorld.GetItem().itemType.ToString());
        int amount = itemWorld.GetItem().amount;
        Item duplicateItem = new Item { itemType = Item.ItemType.Coin, amount = priceForItem * amount };
        animator.SetBool("walk", false);
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("jump", true);
        yield return new WaitForSeconds(1);
        animator.SetBool("jump", false);
        yield return new WaitForSeconds(1);
        ItemWorld.DropItem(this.transform.position, "S", duplicateItem);
        itemWorld.DestroySelf();
        yield return new WaitForSeconds(1);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        
        itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            
            StartCoroutine(takeItem());
            
        }
        else
        {
            x *= -1;
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(ThePlayer.transform.position, transform.position);


        if (dist > Radius)
        {
            animator.SetBool("idle", false);
            animator.SetBool("walk", true);
            Vector3 movement = new Vector3(x, 0, 0);
            movement *= Time.fixedDeltaTime;
            transform.Translate(movement);
        }
        else 
        { 
            animator.SetBool("walk", false); 
            animator.SetBool("idle", true); 
            if(player.transform.position.x < this.transform.position.x)
            {
                Vector3 newScale = transform.localScale;
                newScale.x = -1;
                transform.localScale = newScale;
                x = Mathf.Abs(x) * -1;
            }
            else
            {
                Vector3 newScale = transform.localScale;
                newScale.x = 1;
                transform.localScale = newScale;
                x = Mathf.Abs(x);
            }
        }
















        //do testow------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


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
            }
            else if (Input.GetButtonDown("Talk") && Vector3.Distance(transform.position, Player.transform.position) < 4f)
            {
                TriggerDialogue();
                talking = true;
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



    //koniec testow------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


}

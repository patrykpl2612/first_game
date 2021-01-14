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
    // Start is called before the first frame update


    public GameObject ThePlayer;
    public PlayerControls player;
    public float Radius;
    public Transform target;
    private ItemWorld itemWorld;

    void Start()
    {

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
        else { animator.SetBool("walk", false); animator.SetBool("idle", true); }





    }
}

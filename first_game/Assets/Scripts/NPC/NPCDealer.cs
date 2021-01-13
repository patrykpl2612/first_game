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


    void Start()
    {

    }

    private int GetPrice(string itemType)
    {
        switch (itemType)
        {
            default:
            case "HealthPotion":    return 1;
            case "Coin":            return 2;
            case "Muszla1":         return 3;
            case "Muszla2":         return 4;
            case "Muszla3":         return 5;
            case "Muszla4":         return 6;
            case "Muszla5":         return 7;
        }
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(2);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            StartCoroutine(ExampleCoroutine());
            int priceForItem = GetPrice(itemWorld.GetItem().itemType.ToString());
            int amount = itemWorld.GetItem().amount;

            Item duplicateItem = new Item { itemType = Item.ItemType.Coin, amount = priceForItem * amount };
            //transform.LookAt(target);
            ItemWorld.DropItem(this.transform.position, "S", duplicateItem);
            itemWorld.DestroySelf();
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
            animator.SetBool("walk", false);
           
            Vector3 movement = new Vector3(x, 0, 0);
            movement *= Time.fixedDeltaTime;
            transform.Translate(movement);
            
        }
        else animator.SetBool("walk", true);





    }
}

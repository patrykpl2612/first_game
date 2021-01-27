using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private UI_Inventory uiInventory;

    public static bool IsInputEnabled;

    public static bool InReachOfNpc = false;

    private Inventory inventory;
    public GameObject inventoryObject;
    public Item activeItem;
    public int activeItemNumber = 1;
    public GameObject ThePlayer;
    public float Radius;
    public PlayerControls player;


    public Item GetActiveItem()
    {
        return activeItem;
    }

    private void Awake()
    {
        inventory = new Inventory(UseItem);
        uiInventory.SetChest(this);
        uiInventory.SetInventory(inventory);
        inventoryObject.SetActive(false);

    }

    private void Update()
    {
        float dist = Vector3.Distance(ThePlayer.transform.position, transform.position);

        if (dist < Radius)
        {
            if (Input.GetButtonDown("Chest"))
            {
                if (inventoryObject.activeSelf == true)
                {
                    //animator.enabled = true;
                    inventoryObject.SetActive(false);
                    player.disableMovement = false;
                }
                else
                {
                    //animator.enabled = false;
                    inventoryObject.SetActive(true);
                    player.disableMovement = true;
                }

            }
        }
        else inventoryObject.SetActive(false);

        if (Input.GetButtonDown("Right") && inventoryObject.activeSelf == true)
        {
            activeItemNumber++;
            if (activeItemNumber > inventory.GetItemListLength()) activeItemNumber = inventory.GetItemListLength();
            int tmp = 0;
            foreach (Item item in inventory.GetItemList())
            {
                tmp++;
                if (tmp == activeItemNumber)
                {
                    activeItem = item;
                    break;
                }

            }
            uiInventory.RefreshInventoryItems();
        }

        if (Input.GetButtonDown("Left") && inventoryObject.activeSelf == true)
        {
            activeItemNumber--;
            if (activeItemNumber < 1) activeItemNumber = 1;
            int tmp = 0;
            foreach (Item item in inventory.GetItemList())
            {
                tmp++;
                if (tmp == activeItemNumber)
                {
                    activeItem = item;
                    break;
                }

            }
            uiInventory.RefreshInventoryItems();
        }

        if (Input.GetButtonDown("Down") && inventoryObject.activeSelf == true)
        {
            activeItemNumber += 4;
            if (activeItemNumber > inventory.GetItemListLength()) activeItemNumber -= 4;
            int tmp = 0;
            foreach (Item item in inventory.GetItemList())
            {
                tmp++;
                if (tmp == activeItemNumber)
                {
                    activeItem = item;
                    break;
                }

            }
            uiInventory.RefreshInventoryItems();
        }

        if (Input.GetButtonDown("Up") && inventoryObject.activeSelf == true)
        {
            activeItemNumber -= 4;
            if (activeItemNumber < 1) activeItemNumber += 4;
            int tmp = 0;
            foreach (Item item in inventory.GetItemList())
            {
                tmp++;
                if (tmp == activeItemNumber)
                {
                    activeItem = item;
                    break;
                }

            }
            uiInventory.RefreshInventoryItems();
        }

        if (Input.GetButtonDown("Drop") && activeItem.amount > 0) // dla Input.GetButton("Drop") && activeItem.amount > 0 SRA PIENIEDZMI JAK POYEBANYYYY
        {
            Item duplicateItem = new Item { itemType = activeItem.itemType, amount = activeItem.amount };
            inventory.RemoveItem(activeItem);
            ItemWorld.DropItem(this.transform.position, "S", duplicateItem);
        }

        if (Input.GetButtonDown("Use") && activeItem.amount > 0)
        {
            player.inventory.AddItem(activeItem);
            inventory.RemoveItem(activeItem);
        }

    }

    private void UseItem(Item item)
    {
        //Flash();
        //kod dla UseItem
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            // Touching Item
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

    void Start()
    {
        IsInputEnabled = true;
    }
}


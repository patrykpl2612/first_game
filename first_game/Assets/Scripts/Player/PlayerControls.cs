using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private UI_Inventory uiInventory;

    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public static bool IsInputEnabled;
    public GameObject Pushing;
    public string PushFacing;
    public static bool InReachOfNpc = false;

    public bool IsPushing = false; // czy cos pcha
    public bool Holding = false; // Czy coś trzyma
    public GameObject HeldItem;  // Co trzyma
    public float fastness;       // Prędkość poruszania się
    public string Facing = "S";  // Kierunek w który aktualnie patrzy

    public int default_animation_speed = 2;
    public float default_player_speed = 0.1f;

    private Inventory inventory;
    public GameObject inventoryObject;
    public Item activeItem;
    public int activeItemNumber = 1;
    public bool ItemTalking = false;

    public float flashTime;
    Color origionalColor;


    public Item GetActiveItem()
    {
        return activeItem;
    }

    private void Awake()
    {
        inventory = new Inventory(UseItem);
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);
        inventoryObject.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetButtonDown("Equipment"))
        {
            if (inventoryObject.activeSelf == true)
            {
                animator.enabled = true;
                inventoryObject.SetActive(false);
            }
            else
            {
                animator.enabled = false;
                inventoryObject.SetActive(true);
            }

        }

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
            ItemWorld.DropItem(this.transform.position, Facing, duplicateItem);
        }

        if (Input.GetButtonDown("Use") && activeItem.amount > 0)
        {
            inventory.UseItem(activeItem);
            inventory.RemoveItem(activeItem);
        }

        if (ItemTalking == true && Input.GetButtonDown("Talk"))
        {
            ItemTalking = FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }
    private void UseItem(Item item)
    {
        Flash();
        //kod dla UseItem
    }

    private void Flash()
    {
        GetComponent<Renderer>().material.color = Color.red;
        Invoke("ResetColor", flashTime);
    }

    private void ResetColor()
    {
        GetComponent<Renderer>().material.color = origionalColor;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            // Touching Item
            Item pickedUpItem = itemWorld.GetItem();
            inventory.AddItem(pickedUpItem);

            Dialogue dialog = new Dialogue();   
            string[] sentences = {pickedUpItem.Name(),""};
            ItemTalking = true;
            dialog.name = "ITEM FOUND!";
            dialog.sentences = sentences;
            FindObjectOfType<DialogueManager>().StartDialogue(dialog);      
            itemWorld.DestroySelf();
        }
    }

    void Start()
    {
        IsInputEnabled = true;
        origionalColor = GetComponent<Renderer>().material.color;
    }

    void FixedUpdate()
    {

        if (IsInputEnabled && this.GetComponent<PlayerMenu>().IsShowed() == false)
        {
            bool up = Input.GetButton("Up");
            bool down = Input.GetButton("Down");
            bool left = Input.GetButton("Left");
            bool right = Input.GetButton("Right");
            bool space = Input.GetButton("Run");
            bool putDown = Input.GetButton("PutDown");

            bool[] inputList = { up, down, left, right, putDown, space };
            string[] inputNames = { "Up", "Down", "Left", "Right" }; //lista booli do przeslania do animatora
            SendBoolInput(inputList, inputNames);


            ControlPlayer(inputList);

        }
    }

    void ControlPlayer(bool[] Inputlist)
    {
        int x = 0;      // od -1 do 1
        int y = 0;      // od -1 do 1    

        if (Inputlist[0] == true && Inputlist[1] == false && inventoryObject.activeSelf == false)
        {
            y = 1;
            Facing = "N";
        }
        else if (Inputlist[1] == true && Inputlist[0] == false && inventoryObject.activeSelf == false)
        {
            y = -1;
            Facing = "S";
        }

        if (Inputlist[2] == true && Inputlist[3] == false && inventoryObject.activeSelf == false)
        {
            x = -1;
            Facing = "W";
        }
        else if (Inputlist[3] == true && Inputlist[2] == false && inventoryObject.activeSelf == false)
        {
            x = 1;
            Facing = "E";
        }

        if (Inputlist[5])
        {
            fastness = default_player_speed * 2;
            animator.speed = default_animation_speed * 2;
        }
        else
        {
            fastness = default_player_speed;            // domyslne parametry gracza dotyczace jego predkosci i jego animacji
            animator.speed = default_animation_speed;
        }

        if (IsPushing && FacingIsPush(Inputlist[0], Inputlist[1], Inputlist[2], Inputlist[3], PushFacing))
        {
            Pushing.GetComponent<PushAble>().Push(PushFacing);
        }



        Vector3 movement = new Vector3(50 * x * fastness, 50 * y * fastness, 0);

        movement *= Time.fixedDeltaTime;
        transform.Translate(movement);


        if (Holding)
        {
            Pickup(HeldItem);
        }
        if (Holding && Inputlist[4] && x == 0 && y == 0 && InReachOfNpc == false)
        {
            Throw(HeldItem);

        }

    }

    void OnCollisionEnter2D(Collision2D Object)
    {
        GameObject CollidedObject = GameObject.Find(Object.collider.name);

        if (CollidedObject.GetComponent<PickupAble>() != null && Holding == false) // sprawdz czy obiekt mozna podniesc
        {
            Holding = true;
            HeldItem = CollidedObject;
        }

        if (CollidedObject.GetComponent<PushAble>() != null)
        {
            IsPushing = true;
            Pushing = CollidedObject;
            PushFacing = Facing;
        }


    }

    void OnCollisionExit2D(Collision2D Object)
    {
        GameObject CollidedObject = GameObject.Find(Object.collider.name);
        bool IsPushable = CollidedObject.GetComponent<PushAble>() != null;


        if (IsPushable)
        {
            IsPushing = false;
            Pushing = null;
        }

    }

    void Pickup(GameObject Object)
    {
        Object.transform.SetParent(transform, true);
        Object.transform.Translate((transform.position) - Object.transform.position);
        Object.transform.Translate(0f, Object.GetComponent<PickupAble>().Get("Height"), 0f);

        if (Facing == "W")
        {
            Vector3 newScale = Object.transform.localScale;
            if (newScale.x > 0) newScale.x *= -1;
            Object.transform.localScale = newScale;
        }
        else if (Facing == "E")
        {
            Vector3 newScale = Object.transform.localScale;
            if (newScale.x < 0) newScale.x *= -1;
            Object.transform.localScale = newScale;
        }

        Collider2D m_Collider = Object.GetComponent<Collider2D>();
        SpriteRenderer m_Renderer = Object.GetComponent<SpriteRenderer>();


        if (m_Collider.enabled)
        {
            m_Collider.enabled = false;
            m_Renderer.sortingOrder = 3;
        }
    }

    void Throw(GameObject Object)
    {
        Collider2D m_Collider = Object.GetComponent<Collider2D>();
        SpriteRenderer m_Renderer = Object.GetComponent<SpriteRenderer>();

        m_Renderer.sortingOrder = 1;
        m_Collider.enabled = true;
        Holding = false;
        HeldItem = null;
        float head = Object.GetComponent<PickupAble>().Get("Height");
        float rangeH = Object.GetComponent<PickupAble>().Get("RangeH");
        float rangeV = Object.GetComponent<PickupAble>().Get("RangeV");



        if (Facing == "W")
        {
            Object.transform.Translate(rangeH * -1f, 0f - head, 0f);  //rzut w lewo
        }
        if (Facing == "E")
        {
            Object.transform.Translate(rangeH, 0f - head, 0f);   //rzut w prawo
        }
        if (Facing == "N")
        {
            Object.transform.Translate(0f, rangeV - head, 0f); //rzut w górę
        }
        if (Facing == "S")
        {
            Object.transform.Translate(0f, (rangeV * -1f) - head, 0f); //rzut w dół
        }

        Object.transform.SetParent(null);
    }

    void SendBoolInput(bool[] inputList, string[] inputNames)
    {
        int i = 0;
        foreach (string inputName in inputNames)
        {
            animator.SetBool(inputName, inputList[i]);  //wysyła stan wartosci do animatora unity (mapowanie kontrolek,animacji)
            i += 1;
        }
    }

    bool FacingIsPush(bool up, bool down, bool left, bool right, string Facing) //zwraca true jesli tylko jeden z 4 jest true
    {
        if (Facing == "N" && up && left == false && right == false)
        {
            return true;
        }

        if (Facing == "E" && right && up == false && down == false)
        {
            return true;
        }

        if (Facing == "S" && down && left == false && right == false)
        {
            return true;
        }

        if (Facing == "W" && left && up == false && down == false)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

}


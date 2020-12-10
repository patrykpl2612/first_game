using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
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
    public bool Collided = false;  // Czy z czymś koliduje   
    public string Facing = "S";  // Kierunek w który aktualnie patrzy

    public int default_animation_speed = 2;
    public float default_player_speed = 0.1f;

    private bool paused;

    void Start()
    {
        IsInputEnabled = true;
        paused = false;
    }

    void FixedUpdate()
    {

        if (IsInputEnabled && paused==false)
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

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = togglePause();
        }
    }

    void OnGUI() //GUI przy paused game
    {
        if (paused)
        {   
               // paused = togglePause();
        }
    }

    void ControlPlayer(bool[] Inputlist)
    {
        int x = 0;      // od -1 do 1
        int y = 0;      // od -1 do 1    

        if (Inputlist[0] == true && Inputlist[1] == false)
        {
            y = 1;
            Facing = "N";
        }
        else if (Inputlist[1] == true && Inputlist[0] == false)
        {
            y = -1;
            Facing = "S";
        }

        if (Inputlist[2] == true && Inputlist[3] == false)
        {
            x = -1;
            Facing = "W";
        }
        else if (Inputlist[3] == true && Inputlist[2] == false)
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


        if (Collided == false)
        {
            Vector3 movement = new Vector3(50 * x * fastness, 50 * y * fastness, 0);

            movement *= Time.fixedDeltaTime;
            transform.Translate(movement);
        }

        if (Holding)
        {
            Pickup(HeldItem);
        }
        if (Holding && Inputlist[4] && x == 0 && y == 0 && InReachOfNpc == false)
        {
            Throw(HeldItem);
            
        }
        Collided = false;
    }

    void OnCollisionEnter2D(Collision2D Object)
    {
        Collided = true;
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
        if (CollidedObject.GetComponent<PushAble>() != null)
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

    bool togglePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            Debug.Log("Unpaused");
            return (false);
        }
        else
        {
            Time.timeScale = 0f;
            Debug.Log("Paused");
            return (true);
        }
    }
}


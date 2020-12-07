using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public GameObject HeldItem;
    public Vector2 speed = new Vector2(50, 50);
    public bool Holding = false;
    public float fastness;
    public bool Collided=false;
    public int x, y;
    public string Facing = null;



    public int default_animation_speed = 2;
    public float default_player_speed = 0.1f;


    void FixedUpdate()
    {
  
         x = 0;      // od -1 do 1
         y = 0;      // od -1 do 1

        bool up = Input.GetButton("Up");
        bool down = Input.GetButton("Down");
        bool left = Input.GetButton("Left");
        bool right = Input.GetButton("Right");
        bool space = Input.GetButton("Run");
        bool putDown = Input.GetButton("PutDown");



        bool[] inputList = { up, down, left, right,};       //listy do przeslania booli do animatora
        string[] inputNames = { "Up", "Down", "Left", "Right"};
        SendBoolInput(inputList, inputNames);



        if (up == true && down == false)
        {
            y = 1;
            Facing = "N";
        }
        else if (down == true && up == false)
        {
            y = -1;
            Facing = "S";
        }

        if (left == true && right == false)
        {
            x = -1;
            Facing = "W";
        }
        else if (right == true && left == false)
        {
            x = 1;
            Facing = "E";
        }

        if (space) 
        {
            fastness = default_player_speed * 2;
            animator.speed = default_animation_speed * 2;           
        }
        else
        {
            fastness = default_player_speed;            // domyslne parametry gracza dotyczace jego predkosci i jego animacji
            animator.speed = default_animation_speed;
        }

        if (Collided == false)
        {
            Vector3 movement = new Vector3(speed.x * x * fastness, speed.y * y * fastness, 0);

            movement *= Time.fixedDeltaTime;
            transform.Translate(movement);
        }

        if (Holding)
        {
            Pickup(HeldItem);
        }
        if (Holding && putDown && x == 0 && y == 0)
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
            Object.transform.Translate(rangeH * -1f, 0f -head, 0f);  //rzut w lewo
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




    public void SendBoolInput(bool[] inputList, string[] inputNames)
    {
        int i = 0;
        foreach (string inputName in inputNames)
        {
            animator.SetBool(inputName, inputList[i]);  //wysyła stan wartosci do animatora unity (mapowanie kontrolek,animacji)
            i += 1;
        }
    }

}
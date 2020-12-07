using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Vector2 speed = new Vector2(50, 50);
    public float fastness;
    public bool Collides=false;

 
    public int default_animation_speed = 2;
    public float default_player_speed = 0.1f;


    void FixedUpdate()
    {
  
        int x = 0;      // od -1 do 1
        int y = 0;      // od -1 do 1

        bool up = Input.GetButton("Up");
        bool down = Input.GetButton("Down");
        bool left = Input.GetButton("Left");
        bool right = Input.GetButton("Right");
        bool space = Input.GetButton("Run");
        

        bool[] inputList = { up, down, left, right, space };
        string[] inputNames = { "Up", "Down", "Left", "Right"};
        SendBoolInput(inputList, inputNames);



        if (up == true && down == false)
        {
            y = 1;
        }
        else if (down == true && up == false)
        {
            y = -1;
        }

        if (left == true && right == false)
        {
            x = -1;
        }
        else if (right == true && left == false)
        {
            x = 1;
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

        if (Collides == false)
        {
            Vector3 movement = new Vector3(speed.x * x * fastness, speed.y * y * fastness, 0);

            movement *= Time.fixedDeltaTime;
            transform.Translate(movement);
        }
        Collides = false;

    }
    void OnCollisionEnter2D(Collision2D Object)
    {
        Collides = true;
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
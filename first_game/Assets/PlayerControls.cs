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


    public int FPS_LIMIT = 30;
    public int default_animation_speed = 2;
    public float default_player_speed = 0.1f;


    void Update()
    {

        int x = 0;      // od -1 do 1
        int y = 0;      // od -1 do 1

        bool up = Input.GetButton("Fire1");
        bool down = Input.GetButton("Fire2");
        bool left = Input.GetButton("Fire3");
        bool right = Input.GetButton("Jump");
        bool space = Input.GetButton("Vertical");
        bool SwitchAnim = up || down || left || right; //bool determinujacy kiedy zmienic animacje chodzenia

        bool[] inputList = { up, down, left, right, space, SwitchAnim };
        string[] inputNames = { "Up", "Down", "Left", "Right", "SwitchAnim" };
        SendBoolInput(inputList, inputNames);

        Debug.Log("Test");

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

        if (x == 0 && y == 0)
        {
            animator.speed = 0; // Zatrzymaj animacje jesli gracz stoi w miejscu
        }


        Vector3 movement = new Vector3(speed.x * x * fastness, speed.y * y * fastness, 0);

        movement *= Time.deltaTime;
        transform.Translate(movement);
    }

    void Awake()
    {
        Application.targetFrameRate = FPS_LIMIT;  // Limit klatek !!!WRZUCICĆ TO DO OSOBNEGO PLIKU!!!
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
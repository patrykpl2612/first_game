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

    void Update()
    {

        int x = 0;
        int y = 0;


        bool up = Input.GetButton("Fire1");
        bool down = Input.GetButton("Fire2");
        bool left = Input.GetButton("Fire3");
        bool right = Input.GetButton("Jump");
        bool spaceUp = Input.GetButton("Vertical");

        animator.SetBool("Right", right);
        animator.SetBool("Left", left);
        animator.SetBool("Down", down);
        animator.SetBool("Up", up);
        animator.SetBool("SwitchAnim", SwitchAnim(up, down, left, right));

        if (up == true && down == false)
        {
            y = 1;
        }
        if (down == true && up == false)
        {
            y = -1;
        }

        if (left == true && right == false)
        {
            x = -1;
        }
        if (right == true && left == false)
        {
            x = 1;
        }
        if (x == 0 && y == 0)
        {
            animator.speed = 0;
        }

        if (spaceUp == true)
        {
            fastness = 0.2f;
            animator.speed = 2;
        }
        else
        {
            fastness = 0.1f;
            animator.speed = 1;
        }


        Vector3 movement = new Vector3(speed.x * x * fastness, speed.y * y * fastness, 0);

        movement *= Time.deltaTime;
        transform.Translate(movement);
    }

    void Awake()
    {
        Application.targetFrameRate = 30;
    }
    public bool SwitchAnim(bool a, bool b, bool c, bool d)
    {
        return a || b || c || d;
    }
}
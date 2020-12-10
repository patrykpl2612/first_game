using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private Collider2D m_ObjectCollider;
    public bool Pressed;                            // jesli chcemy zeby jakis obiekt dzialal na plytko to trzeba mu dac rigidbody2D
    public Animator PressurePlateAnimator;


    void Start()
    {
        Collider2D m_ObjectCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        PressurePlateAnimator.SetBool("Pressed",Pressed);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Pressed = true;
        Debug.Log("PRESSED!");
    }
    void OnTriggerExit2D(Collider2D col)
    {
        Pressed = false;
        Debug.Log("RELESED!");
    }
}


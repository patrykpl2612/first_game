using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private int ObjectsOnPlate = 0;
    private Collider2D m_ObjectCollider;
    public bool Pressed;                            // jesli chcemy zeby jakis obiekt dzialal na plytko to trzeba mu dac rigidbody2D
    public Animator PressurePlateAnimator;
    public AudioSource audiosource;


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

        ObjectsOnPlate += 1;
        if (ObjectsOnPlate == 1)
        {
            Pressed = true;
            audiosource.Play();
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        ObjectsOnPlate -= 1;
        if (ObjectsOnPlate == 0)
        {
            Pressed = false;
            audiosource.Play();
        }
    }
}


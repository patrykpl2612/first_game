using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAble : MonoBehaviour
{
    public float PushDistance = 10f;
    public GameObject Player;



    void Start()
    {
        Player = GameObject.Find("/Player");
    }


    public void Push(string direction)
    {

        Vector3 movement = new Vector3(0,0,0);
    
        if (direction == "N")
        {
            movement = new Vector3(0, PushDistance , 0);
        }

        if (direction == "E")
        {
            movement = new Vector3(PushDistance, 0, 0);
        }

        if (direction == "S")
        {
            movement = new Vector3(0, PushDistance * -1f, 0);
        }

        if (direction == "W")
        {
            movement = new Vector3(PushDistance * -1f, 0, 0);
        }
           

        movement *= Time.fixedDeltaTime;
        transform.Translate(movement);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAble : MonoBehaviour 
{ 
    public bool PickedUp = false;
    public float PlayerX;
    public float PlayerY;


   void OnCollisionEnter2D(Collision2D Object)
   {
        var obiekt = Object.collider;
        var playerPosition = GameObject.Find("Player").transform.position;
        PlayerX = playerPosition[0];
        PlayerY = playerPosition[1];

        Debug.Log(PlayerX);
        Debug.Log(PlayerY);

        if (obiekt.transform.position == GameObject.Find("Player").transform.position) 
        {
        PickedUp = true;
        }

        
    }
    void FixedUpdate()
    {
        if (PickedUp)
        {
            gameObject.transform.Translate(PlayerX, PlayerY,0f);
        }
        PickedUp = false;
    }
}

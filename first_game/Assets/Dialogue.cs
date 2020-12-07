using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject Player;
    public GameObject canvas;
    public Behaviour behavior;

    void Start()
    {
        Player = GameObject.Find("/Player");
        canvas = GameObject.Find("/Canvas");
        behavior = canvas.GetComponent<Behaviour>();
    }
    
    void FixedUpdate()
    {
        if( Vector3.Distance(transform.position, Player.transform.position) < 4f && Input.GetButton("Talk"))
        {
            behavior.enabled = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicRock : MonoBehaviour
{
    public GameObject Player;
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("/Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent == Player.transform) 
        {
            animator.SetBool("IsPicked", true);        
        }
        else 
        {
            animator.SetBool("IsPicked", false);
        }
    }
}

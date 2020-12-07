using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAble : MonoBehaviour
{
    public bool PickedUp = false;
    public Transform RockChild;
    public Transform PlayerParent;
    public GameObject Player;
    public Animator PlayerAnimator;
    public PlayerControls move;

    void Start()
    {
        RockChild = transform.Find("/InterRock");
        PlayerParent = transform.Find("/Player");
        Player = GameObject.Find("/Player");
        move = Player.GetComponent<PlayerControls>();
        PlayerAnimator = Player.GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D Object)
    {
        var obiekt = Object.collider;

        Vector3 playerPosition = GameObject.Find("Player").transform.position;

        Vector3 rockPosition = playerPosition;
        if (obiekt.transform.position == GameObject.Find("/Player").transform.position)
        {
            PickedUp = true;
            rockPosition[1] += 2.1f;
            RockChild.transform.SetParent(PlayerParent, true);
            RockChild.transform.Translate(rockPosition - RockChild.position);
        }


    }
    void FixedUpdate()
    {

        bool rzut = Input.GetButton("PutDown");
        int x = move.x;
        int y = move.y;
       // Vector3 changedscale = this.transform.localScale;
        //changedscale[0] *= -1;

      /*  if (x == 1)
        {
            this.transform.localScale = changedscale;
        }
        if(x==-1)
        {
            this.transform.localScale = changedscale;
        }*/

        if (PickedUp && rzut && y == 0 && x == 0)
        {


            Vector3 playerPosition = GameObject.Find("Player").transform.position;
            Vector3 rockPosition = playerPosition;
            if (PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("PlayerStandingLeft"))
            {
                rockPosition[0] -= 3f;
            }
            if (PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("PlayerStandingRight"))
            {

                rockPosition[0] += 3f;
            }
            if (PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("PlayerStandingUp"))
            {
                rockPosition[1] += 3f;
            }
            if (PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("PlayerStandingDown"))
            {
                rockPosition[1] -= 3f;
            }
           
            RockChild.transform.Translate(rockPosition - RockChild.position);
            RockChild.transform.SetParent(null);
            PickedUp = false;
            
        }

    }
}
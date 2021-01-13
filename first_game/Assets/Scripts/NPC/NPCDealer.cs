using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDealer : MonoBehaviour
{
    public float smooth = 1f;

    private Quaternion targetRotation;

    public int x = 1;
    // Start is called before the first frame update


    public GameObject ThePlayer;
    public float Radius;


    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        x *= -1;
      
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(ThePlayer.transform.position, transform.position);
        
        if (dist > Radius)
        {
            Vector3 movement = new Vector3(x, 0, 0);
            movement *= Time.fixedDeltaTime;
            transform.Translate(movement);
        }


        
        

    }
}

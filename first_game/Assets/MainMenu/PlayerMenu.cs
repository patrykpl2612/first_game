using UnityEngine;
using System.Collections;

public class PlayerMenu : MonoBehaviour
{

    public GameObject menu; // Assign in inspector
    private bool isShowing;

    public bool paused;

    void Start()
    {
        menu.SetActive(false);
        paused = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = togglePause();
            isShowing = !isShowing;
            menu.SetActive(isShowing);
        }
    }
    
     bool togglePause()
    {
         if (Time.timeScale == 0f)
         {
             Time.timeScale = 1f;
             Debug.Log("Unpaused");
             return (false);
         }
         else
         {
             Time.timeScale = 0f;
             Debug.Log("Paused");
             return (true);
         }
    }
    
    public bool IsShowed()
    {
        return isShowing;
    }
}
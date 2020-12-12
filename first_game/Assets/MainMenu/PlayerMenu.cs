using UnityEngine;
using System.Collections;

public class PlayerMenu : MonoBehaviour
{

    public GameObject menu;
    private bool isShowing;

    

    void Start()
    {
        menu.SetActive(false);
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            ShowMenu();
        }
    }
    
    public void togglePause()//nie wiem czy dziala co to Time.timeScale?
    {
         if (Time.timeScale == 0f)
         {
             Time.timeScale = 1f;
         }
         else
         {
             Time.timeScale = 0f; 
         }
    }
    
    public bool IsShowed()// zwraca czy gra jest zatrzymana(okienko menu jest wlaczone)
    {
        return isShowing;
    }

    public void ShowMenu()//wlacza/wylacza okienko menu
    {
        isShowing = !isShowing;
        menu.SetActive(isShowing);
        togglePause();
    }
}